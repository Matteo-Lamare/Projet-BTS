# EduGest Web

Client web responsive de la réalisation R2.

## Stack

- HTML sémantique ;
- Tailwind CSS ;
- JavaScript vanilla ;
- API REST EduGest pour les données ;
- SignalR uniquement pour la messagerie temps réel.

Tailwind est installé avec npm et compilé avec le CLI officiel. Le Play CDN n'est pas utilisé en production.

## Commandes prévues

`npm install` installe les dépendances du front.

`npm run dev` surveille les sources et régénère le CSS pendant le développement.

`npm run build` génère le CSS minifié destiné au déploiement.
