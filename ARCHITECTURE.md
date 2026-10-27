# Architecture technique

## Décision validée

| Élément | Choix |
| --- | --- |
| API | ASP.NET Core 10 avec C# |
| Accès aux données | Entity Framework Core 10 avec le fournisseur Npgsql |
| Base de données | PostgreSQL |
| Application légère | Vue 3, TypeScript et Vite |
| Application lourde | Avalonia UI avec C# et le patron MVVM |
| Communication temps réel | SignalR |
| Contrôle des accès | ASP.NET Core Identity, JWT et permissions applicatives |
| Documentation d'API | OpenAPI |
| Tests | xUnit, Testcontainers et Playwright |
| Exécution locale | Docker Compose |

## Organisation retenue

```text
Projet-BTS/
├── backend/             API ASP.NET Core
├── frontend-light/      Application Vue des élèves et professeurs
├── desktop-admin/       Application Avalonia de l'administration
├── database/             Schéma SQL et futures migrations
├── docs/                 Documentation technique complémentaire
├── MCD.md                Modèle conceptuel de données
├── MLD.md                Modèle logique de données
└── E6.md                 Dossier de réalisation
```

## Responsabilités

### API

L'API centralise les règles métier, l'authentification, les permissions, la validation des données et l'accès à PostgreSQL. Les deux clients y accèdent exclusivement par HTTPS.

Les modules prévus sont :

- comptes, rôles et permissions ;
- élèves, professeurs, classes, matières et années scolaires ;
- notes et évaluations ;
- absences et retards ;
- documents ;
- messagerie et notifications ;
- emploi du temps ;
- journalisation et statistiques.

### Application légère

L'application Vue est une interface web responsive destinée aux élèves et professeurs. Elle ne contient aucune règle de sécurité critique : les autorisations sont toujours vérifiées par l'API.

### Application lourde

L'application Avalonia est installable et destinée à l'administration. Le patron MVVM sépare l'interface, la logique de présentation et l'accès à l'API.

## Dépendances recommandées

| Projet | Dépendances principales |
| --- | --- |
| API | ASP.NET Core Identity, Entity Framework Core, Npgsql, SignalR, FluentValidation, OpenAPI, Serilog |
| Web | Vue Router (si nécessaire), Pinia (si nécessaire), Vite |
| Bureau | Avalonia, CommunityToolkit.Mvvm, Microsoft.Extensions.DependencyInjection, Refit ou HttpClientFactory |
| Tests | xUnit, FluentAssertions, Testcontainers, Playwright |
| Infrastructure | PostgreSQL, Docker Compose, GitHub Actions |

Chaque dépendance doit être ajoutée seulement lorsqu'elle répond à un besoin identifié. Les versions exactes seront figées dans les fichiers de dépendances au moment de l'initialisation de chaque projet.

## Sécurité

- Les mots de passe sont hachés par le serveur.
- Les jetons d'accès ne sont jamais inscrits dans le dépôt.
- Les secrets de développement utilisent un fichier local non versionné.
- Toutes les actions sensibles sont journalisées.
- Les fichiers déposés sont contrôlés par type, taille et autorisation d'accès.
