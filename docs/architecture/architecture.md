# Architecture technique

## Vue générale

Avalonia (R1) et Web (R2) consomment la même API ASP.NET Core, qui est la seule porte d'accès à PostgreSQL. L'API gère aussi le stockage des fichiers et SignalR.

## Solution .NET

- EduGest.Domain : entités, énumérations et règles métier.
- EduGest.Application : cas d'utilisation, services, DTO, validations et interfaces.
- EduGest.Infrastructure : EF Core, PostgreSQL, Identity, fichiers et journalisation.
- EduGest.Api : contrôleurs REST, authentification, autorisation, middleware et SignalR.
- EduGest.Desktop : client Avalonia en MVVM.
- EduGest.Web : client React + TypeScript + Vite + Tailwind CSS. React est compilé par Vite ; les dépendances sont regroupées lors du build de production.
- Tests : xUnit unitaires et intégration API.

Flux : Client → Controller → Application Service → Infrastructure/EF Core → PostgreSQL.

Cible d'architecture : les échanges utilisent des DTO séparés des entités EF Core. Le socle préparatoire expose encore certaines entités directement ; ce point sera refactoré progressivement par le candidat lors du développement.

## Authentification et autorisation

ASP.NET Core Identity gère les comptes. L'API utilise JWT et refresh tokens. L'autorisation combine rôle, permission et périmètre métier. Un professeur ne peut modifier que les données relevant de ses affectations.

## Fichiers

Les métadonnées sont en PostgreSQL et les fichiers sur un volume serveur. L'API contrôle taille, extension, type MIME et autorisation avant accès.

## Messagerie

REST assure la persistance et l'historique. SignalR diffuse uniquement les nouveaux messages en temps réel.

## Simplicité

Le projet n'utilise pas de microservices, CQRS, event sourcing, Kafka, Redis, Kubernetes ou Elasticsearch : leur coût de complexité n'est pas justifié par le besoin BTS.