# EduGest Web — Vue 3

Application web légère en **Vue 3 + TypeScript + Vite**, destinée aux élèves et professeurs. L'application lourde reste en Avalonia / C#.

## Lancer l'application

Dans `src/EduGest.Web` :

```bash
npm install
npm run dev
```

Démarrer séparément l'API ASP.NET Core. Le proxy Vite transmet les requêtes `/api/*` vers `http://localhost:5000` par défaut. Pour un autre port :

```bash
VITE_API_PROXY_TARGET=http://localhost:5100 npm run dev
```

Sous PowerShell : `$env:VITE_API_PROXY_TARGET="http://localhost:5100"; npm run dev`.

## API existante

L'écran de connexion utilise `POST /api/auth/login` puis `GET /api/auth/me`. Les identifiants doivent exister côté API. Le jeton d'accès n'est pas persisté dans le navigateur. Le refresh token est renvoyé par le serveur mais n'est **pas encore géré** ; la déconnexion actuelle est locale et **ne révoque pas** le refresh token côté serveur. Une gestion complète des sessions doit être implémentée avant une mise en production.

`npm run build` valide les types Vue puis compile les fichiers statiques. La base PostgreSQL n'est jamais appelée directement depuis le navigateur.
