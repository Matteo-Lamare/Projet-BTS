import 'dotenv/config';
import { createApp } from './app.js';

const config = {
  jwtSigningKey: process.env.JWT_SIGNING_KEY,
  jwtIssuer: process.env.JWT_ISSUER,
  jwtAudience: process.env.JWT_AUDIENCE
};
if (!config.jwtSigningKey || new TextEncoder().encode(config.jwtSigningKey).length < 32 ||
    !config.jwtIssuer || !config.jwtAudience || !process.env.DATABASE_URL) {
  throw new Error('DATABASE_URL, JWT_SIGNING_KEY (>=32 octets), JWT_ISSUER et JWT_AUDIENCE sont obligatoires.');
}
const port = Number(process.env.PORT ?? 3000);
if (!Number.isInteger(port) || port < 1 || port > 65535) throw new Error('PORT invalide');
createApp(config).listen(port, () => console.log(`API Node.js disponible sur le port ${port}`));
