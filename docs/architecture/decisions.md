# Décisions techniques

## Backend

ASP.NET Core Web API est retenu pour rester dans l'écosystème C# et intégrer naturellement REST, authentification, autorisation et SignalR.

## Données

PostgreSQL + Entity Framework Core sont retenus pour le modèle relationnel scolaire, les contraintes d'intégrité, les transactions et les migrations. Les migrations EF Core deviendront la source de vérité pendant le développement ; schema.sql reste documentaire.

## Architecture

Domain / Application / Infrastructure / API : séparation claire, testabilité et complexité maîtrisée.

## Clients

R1 utilise WPF + MVVM pour une interface desktop riche. R2 utilise HTML/CSS/JavaScript vanilla afin de maîtriser directement HTTP, JSON et JavaScript sans ajouter un framework front-end.

## Sécurité

ASP.NET Core Identity + JWT + refresh token. Les autorisations sont contrôlées côté serveur. Les secrets ne sont jamais versionnés.

## Temps réel

SignalR est limité à la messagerie. L'historique reste en REST.

## Documents

Fichiers sur volume serveur, métadonnées en PostgreSQL. Les noms physiques sont générés indépendamment du nom utilisateur.

## Déploiement

Ubuntu + Docker Compose, avec API, PostgreSQL et Nginx/reverse proxy HTTPS. Pas de Kubernetes ni d'infrastructure distribuée disproportionnée.

## Git et tests

GitHub Flow : main stable et branches feature/*. Commits avec préfixes feat, fix, test, docs, refactor et chore. xUnit couvre les tests unitaires et d'intégration.

## Environnements

DEV → TEST → PROD. User Secrets en développement, secrets GitHub pour CI/CD et secrets d'environnement en production.

## Logs et audit

Les logs techniques sont séparés du journal métier des actions sensibles. Serilog pourra structurer les logs sans déployer une stack ELK.

## Sauvegardes

Sauvegardes PostgreSQL planifiées, stockage externe, rétention et test réel de restauration.

## Parent

Parent est un rôle métier réel. La relation parent-enfant est une association N:N.

## Soft delete

Le soft delete est limité aux comptes, profils principaux et documents lorsque la conservation historique est utile.

## ADR-12 — Identity comme source des comptes et rôles techniques

Le projet utilise ASP.NET Core Identity pour les comptes d'authentification et les rôles techniques. Les profils métier `Student`, `Teacher` et `Parent` restent des entités du domaine et référencent l'identifiant du compte Identity. Les permissions fonctionnelles restent dans le modèle métier afin de pouvoir exprimer des droits plus fins que les seuls rôles.

Conséquence : le `DbContext` d'Infrastructure sera basé sur `IdentityDbContext`, et les tables Identity seront la source de vérité pour les comptes, mots de passe et rôles. Les tables métier ne dupliquent pas ces données d'authentification.
