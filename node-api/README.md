# Migration expérimentale de l'API EduGest vers Node.js

**État : socle initial, non prêt pour la production.** L'API ASP.NET Core actuelle reste la source de vérité et ne doit pas être arrêtée.

## Stack cible
- Node.js 22+ et Express 5
- PostgreSQL conservé
- **Prisma ORM** en remplacement d'Entity Framework Core
- JWT HS256 avec issuer/audience/signature alignés sur l'API .NET

## Installation
```bash
cd node-api
npm install
cp .env.example .env
# Renseigner la véritable connexion PostgreSQL et les paramètres JWT
npm run db:introspect
npm run db:generate
npm test
npm run dev
```

Le schéma SQL historique `database/schema.sql` est **documentaire et obsolète** : seule la base construite par les migrations EF Core peut être introspectée. Ne pas utiliser `prisma migrate dev` ou `prisma db push` contre les données existantes.

## Compatibilité et limitations
- `GET /health` fonctionne.
- `/api/*` exige un JWT valide mais retourne volontairement 501 pour les routes métier.
- La validation JWT ne remplace pas les politiques de permissions EF/Identity.
- Aucun CRUD, login, refresh, logout, bootstrap administrateur ou migration des mots de passe ASP.NET Identity n'est encore porté.
- Les migrations Prisma ne sont pas activées avant validation du modèle introspecté.

## Étapes nécessaires avant bascule
1. Introspecter les tables Identity, utilisateurs, rôles, permissions et entités scolaires réelles.
2. Porter la vérification des mots de passe PBKDF2 ASP.NET Identity ou imposer une migration contrôlée des identifiants.
3. Porter RBAC et politiques de permissions en requêtant les tables adéquates côté serveur.
4. Reproduire et tester les contrats des 13 contrôleurs métiers, JWT/refresh token rotation et codes HTTP.
5. Valider les tests d'intégration, migrations, Docker/CI, sauvegarde et déploiement canari.

Ne pas connecter les clients R1/R2 à ce socle tant que la parité fonctionnelle et de sécurité n'est pas atteinte.
