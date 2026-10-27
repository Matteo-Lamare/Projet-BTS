import express from 'express';
import helmet from 'helmet';
import { jwtVerify } from 'jose';

export function createApp(config) {
  const app = express();
  app.disable('x-powered-by');
  app.use(helmet());
  app.use(express.json({ limit: '100kb' }));

  app.get('/health', (_req, res) => res.json({ status: 'ok', runtime: 'node' }));

  async function authenticate(req, res, next) {
    const match = /^Bearer (.+)$/i.exec(req.get('authorization') ?? '');
    if (!match) return res.sendStatus(401);
    try {
      const key = new TextEncoder().encode(config.jwtSigningKey);
      const { payload } = await jwtVerify(match[1], key, {
        issuer: config.jwtIssuer,
        audience: config.jwtAudience,
        algorithms: ['HS256'],
        clockTolerance: 30
      });
      req.auth = payload;
      next();
    } catch {
      return res.sendStatus(401);
    }
  }

  // Tant que les règles de permissions et le contrat de chaque contrôleur
  // ne sont pas migrés et testés, aucune route métier n'est exposée.
  app.use('/api', authenticate, (_req, res) =>
    res.status(501).json({ error: 'Migration Node.js en cours : route non disponible' })
  );
  app.use((_req, res) => res.sendStatus(404));
  return app;
}
