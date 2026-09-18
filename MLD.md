# Modèle logique de données

## Convention

Ce modèle traduit le [modèle conceptuel de données](MCD.md) en tables relationnelles.

Le fichier [database/schema.sql](database/schema.sql) propose une implémentation PostgreSQL. PostgreSQL est une cible provisoire : le modèle reste transposable vers un autre système de gestion de base de données si le choix technique change.

## Tables

### Accès et autorisations

| Table | Clé primaire | Clés étrangères | Rôle |
| --- | --- | --- | --- |
| users | id | - | Comptes de connexion |
| roles | id | - | Rôles fonctionnels |
| permissions | id | - | Permissions unitaires |
| user_roles | user_id, role_id | users, roles | Attribution des rôles |
| role_permissions | role_id, permission_id | roles, permissions | Permissions accordées à un rôle |
| students | id | user_id | Profil d'élève |
| teachers | id | user_id | Profil de professeur |

### Organisation scolaire

| Table | Clé primaire | Clés étrangères | Rôle |
| --- | --- | --- | --- |
| academic_years | id | - | Années scolaires |
| classes | id | - | Classes |
| subjects | id | - | Matières |
| enrollments | id | student_id, class_id, academic_year_id | Inscription d'un élève dans une classe |
| teaching_assignments | id | teacher_id, class_id, subject_id, academic_year_id | Attribution d'un enseignement |

### Scolarité

| Table | Clé primaire | Clés étrangères | Rôle |
| --- | --- | --- | --- |
| assessments | id | teaching_assignment_id | Évaluations |
| grades | id | assessment_id, student_id | Notes |
| attendance_events | id | student_id, justified_by | Absences et retards |
| rooms | id | - | Salles |
| sessions | id | teaching_assignment_id, room_id | Séances d'emploi du temps |

### Documents, messagerie et traçabilité

| Table | Clé primaire | Clés étrangères | Rôle |
| --- | --- | --- | --- |
| documents | id | uploaded_by | Métadonnées des documents déposés |
| document_classes | document_id, class_id | documents, classes | Ciblage d'un document par classe |
| document_students | document_id, student_id | documents, students | Ciblage d'un document par élève |
| conversations | id | - | Conversations |
| conversation_participants | conversation_id, user_id | conversations, users | Participants d'une conversation |
| messages | id | conversation_id, author_id | Messages |
| audit_logs | id | user_id | Journal des actions sensibles |

## Contraintes principales

- L'adresse électronique d'un compte est unique.
- Un élève et un professeur sont chacun associés à un seul compte.
- Un élève ne peut posséder qu'une inscription par année scolaire.
- Un professeur ne peut avoir deux fois la même affectation pour une classe, une matière et une année scolaire.
- Un élève ne peut recevoir qu'une note par évaluation.
- Une séance est liée à une affectation d'enseignement, ce qui garantit la cohérence entre professeur, classe et matière.
- Les dates de fin doivent être postérieures aux dates de début.
- Une note doit être comprise entre zéro et le barème de l'évaluation ; cette règle nécessite une validation applicative ou un déclencheur SQL, car elle compare deux tables.
- Les fichiers sont stockés hors de la base de données ; seule leur référence de stockage est conservée dans la table documents.
- Les suppressions d'éléments sensibles doivent être journalisées.

## Passage au développement

Avant de lancer l'API, il faudra :

1. valider PostgreSQL ou remplacer le schéma par l'équivalent du SGBD retenu ;
2. compléter la liste des permissions initiales ;
3. créer les migrations à partir de `database/schema.sql` ;
4. ajouter des données de démonstration anonymisées ;
5. tester les contraintes et autorisations les plus sensibles.
