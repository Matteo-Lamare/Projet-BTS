# Roadmap de développement

## Phase 0 — Socle du projet

Objectif : obtenir une solution compilable et reproductible avant toute fonctionnalité métier.

- solution .NET ;
- projets Domain / Application / Infrastructure / API / Desktop ;
- projet Web ;
- projets de tests ;
- configuration commune ;
- Git ignore et gestion des secrets ;
- premier pipeline CI.

## Phase 1 — Persistance

- DbContext EF Core ;
- entités et configurations ;
- migrations ;
- connexion PostgreSQL ;
- seed minimal de rôles et permissions ;
- tests de persistance essentiels.

## Phase 2 — Authentification

- ASP.NET Core Identity ;
- login ;
- JWT ;
- refresh token ;
- logout ;
- utilisateur courant ;
- rôles ;
- permissions ;
- autorisation ;
- tests des accès autorisés et refusés.

## Phase 3 — R1 : référentiels

- élèves ; professeurs ; classes ; matières ; années scolaires ; inscriptions ; affectations d'enseignement.

## Phase 4 — R1 : scolarité

- évaluations ; notes ; absences ; retards ; séances ; statistiques ; journal d'actions.

## Phase 5 — R2 : consultation web

- connexion ; tableau de bord ; notes ; absences/retards ; documents ; emploi du temps.

## Phase 6 — R2 : messagerie

- conversations ; participants ; messages ; SignalR ; temps réel ; contrôle des droits.

## Phase 7 — Industrialisation

- Docker Compose ; Nginx/HTTPS ; CI/CD ; sauvegardes ; restauration testée ; logs ; documentation utilisateur et technique.

## Règle de progression

Chaque phase doit rester compilable et testable. Une fonctionnalité n'est terminée que lorsque son API, ses validations, ses tests et sa documentation minimale sont cohérents.