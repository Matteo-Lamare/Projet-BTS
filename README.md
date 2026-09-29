# Projet BTS SIO - Gestion d'établissement

Ce dépôt contient la documentation, puis le code source, d'une application de gestion d'établissement scolaire conçue comme réalisation professionnelle pour l'épreuve E6 du BTS SIO, option SLAM.

## Objectif

Proposer deux interfaces complémentaires s'appuyant sur une API et une base de données communes :

- **R1 — application lourde WPF** : administration et enseignants, avec gestion des référentiels, notes et absences ;
- **R2 — application web responsive** : élèves, parents et enseignants, avec consultation, documents, emploi du temps et messagerie temps réel.

## Architecture retenue

- WPF / C# / MVVM pour R1 ;
- HTML / Tailwind CSS / JavaScript vanilla pour R2 ;
- ASP.NET Core Web API pour le backend ;
- SignalR pour la messagerie temps réel ;
- PostgreSQL + Entity Framework Core pour les données ;
- ASP.NET Core Identity + JWT + refresh tokens pour l'authentification ;
- Ubuntu + Docker Compose pour l'hébergement ;
- Nginx comme reverse proxy HTTPS ;
- GitHub + GitHub Actions pour le versionnement et la CI/CD ;
- xUnit pour les tests unitaires et d'intégration.

Les applications clientes ne communiquent jamais directement avec PostgreSQL : toutes les règles métier et autorisations passent par l'API.

## Documentation

- [Dossier E6](E6.md)
- [Périmètre fonctionnel](Fonctionnalitée.md)
- [Découpage R1/R2](Separation.md)
- [MCD](MCD.md)
- [MLD](MLD.md)
- [Architecture technique](docs/architecture/architecture.md)
- [Décisions techniques](docs/architecture/decisions.md)
- [Contrat API](docs/architecture/api.md)
- [DevOps et déploiement](docs/devops.md)
- [Couverture E6](docs/e6/coverage.md)
- [Environnement technologique E6](docs/e6/environment.md)
- [Plan de tests](docs/e6/test-plan.md)
- [Déroulement E6 2026](docs/e6/exam-process.md)
- [Analyse des écarts E6 2026](docs/e6/gap-analysis-2026.md)
- [Cahier des charges](docs/specifications/cahier-des-charges.md)
- [Cas d'utilisation et règles métier](docs/specifications/use-cases.md)
- [Spécifications des interfaces](docs/specifications/ui-specifications.md)
- [Contexte juridique](docs/specifications/legal-context.md)
- [Journal des versions](docs/versions.md)
- [Matrice rôles/permissions](docs/specifications/permissions-matrix.md)
- [Dictionnaire de données](docs/database/data-dictionary.md)
- [Sécurité](docs/security/security.md)
- [Sauvegarde/restauration](docs/operations/backup-restore.md)
- [Déploiement](docs/operations/deployment.md)
- [Checklist finale E6](docs/e6/final-checklist.md)
- [Diagrammes de conception](docs/architecture/diagrams.md)
- [Installation et lancement](docs/operations/installation-run.md)
- [Procédure de recette](docs/testing/acceptance-procedure.md)
- [Maquettes - méthode et statut](docs/design/mockups.md)
- [Gabarit guide R1](docs/user-guides/r1-guide-template.md)
- [Gabarit guide R2](docs/user-guides/r2-guide-template.md)
- [Revue de cohérence documentaire](docs/documentation-review.md)

## Structure cible

```text
src/
├── EduGest.Domain/
├── EduGest.Application/
├── EduGest.Infrastructure/
├── EduGest.Api/
├── EduGest.Desktop/
└── EduGest.Web/
tests/
├── EduGest.UnitTests/
└── EduGest.IntegrationTests/
database/
├── schema.sql
└── seed.sql
docs/
├── architecture/
└── e6/
infrastructure/
├── docker/
└── scripts/
.github/
└── workflows/
```

## État du projet

Le cadrage fonctionnel et l'architecture sont documentés. Un socle API, authentification/autorisation, persistance et tests d'intégration est préparé. Les interfaces R1/R2 et les fonctionnalités finales restent à développer et à maîtriser par le candidat.

## Règles de dépôt

Toute décision importante doit être documentée avant ou avec son implémentation. Les secrets, mots de passe et données personnelles réelles ne doivent jamais être commités.
