# Couverture E6 — BTS SIO SLAM 2026

## Objectif

Ce document relie le projet EduGest aux indicateurs de l'épreuve E6. Il sert de guide de préparation : une compétence n'est considérée comme démontrée que si la réalisation, le code, les tests et les preuves correspondantes existent réellement au moment de l'épreuve.

## Deux réalisations professionnelles

- **R1 — EduGest Desktop** : application WPF destinée principalement à l'administration et aux professeurs.
- **R2 — EduGest Web** : application web responsive destinée principalement aux élèves et parents, avec accès professeur prévu.

Les deux clients utilisent la même API ASP.NET Core et la même base PostgreSQL, mais constituent deux contextes d'utilisation et deux interfaces distinctes.

## Matrice de couverture visée

| Compétence E6 | R1 | R2 | Preuves prévues |
| --- | --- | --- | --- |
| Analyser un besoin et son contexte | Oui | Oui | besoin, périmètre, règles métier, séparation R1/R2 |
| Concevoir l'architecture | Oui | Oui | architecture.md, décisions techniques, schémas |
| Modéliser une solution | Oui | Oui | MCD, MLD, modèle EF Core |
| Exploiter un framework | Oui | Oui | .NET, ASP.NET Core, EF Core, Identity ; WPF pour R1 |
| Développer/adopter des composants | Oui | Oui | services, contrôleurs, clients et composants réutilisables |
| Technologies Web et échanges | API | Oui | API REST, JWT, SignalR pour la messagerie |
| Accès aux données | Oui | Oui | EF Core + PostgreSQL |
| Intégration continue | Oui | Oui | GitHub Actions, historique Git |
| Tests | Oui | Oui | xUnit, tests d'intégration API, scénarios de validation |
| Documentation technique/utilisateur | Oui | Oui | docs techniques + guides utilisateurs à produire |
| Environnement de développement/tests | Oui | Oui | .NET, Git, CI, environnements DEV/TEST/PROD |
| Maintenance corrective/évolutive | Oui | Oui | commits de correction, tests de non-régression, documentation des changements |
| Gérer les données | Oui | Oui | modèle relationnel, habilitations, sauvegarde/restauration |

## Preuves à conserver pendant le développement

Pour chaque fonctionnalité réellement développée par le candidat :
- besoin ou règle métier concernée ;
- capture ou démonstration de l'interface ;
- extrait de code qu'il sait expliquer ;
- test associé et résultat ;
- commit Git correspondant ;
- éventuel problème rencontré et correction ;
- mise à jour de la documentation.

## Points à finaliser avant l'épreuve

- interfaces R1 et R2 réellement opérationnelles ;
- documentation utilisateur adaptée aux publics R1 et R2 ;
- rapport de tests ;
- procédure de déploiement reproductible ;
- sauvegarde PostgreSQL et preuve d'un test de restauration ;
- journal des évolutions/corrections ;
- éléments de démonstration anonymisés ;
- vérification finale de l'environnement technologique.
