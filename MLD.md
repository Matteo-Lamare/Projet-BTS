# Modèle logique de données

## Référence

Le modèle logique traduit le MCD en tables relationnelles PostgreSQL. Les migrations EF Core deviendront la source de vérité pour l'évolution de la base pendant le développement ; `schema.sql` reste la représentation documentaire et de démonstration.

## Tables

### Accès

| Table | Clé primaire | Rôle |
| --- | --- | --- |
| users | id | Comptes |
| roles | id | Rôles |
| permissions | id | Permissions |
| user_roles | user_id, role_id | Attribution des rôles |
| role_permissions | role_id, permission_id | Attribution des permissions |
| students | id | Profils élèves |
| teachers | id | Profils professeurs |
| parents | id | Profils parents |
| parent_students | parent_id, student_id | Liens parent-enfant |

### Organisation scolaire

| Table | Clé primaire | Rôle |
| --- | --- | --- |
| academic_years | id | Années scolaires |
| classes | id | Classes |
| subjects | id | Matières |
| enrollments | id | Inscriptions annuelles |
| teaching_assignments | id | Affectations d'enseignement |

### Scolarité

| Table | Clé primaire | Rôle |
| --- | --- | --- |
| assessments | id | Évaluations |
| grades | id | Notes |
| attendance_events | id | Absences / retards |
| rooms | id | Salles |
| sessions | id | Séances d'emploi du temps |

### Documents

| Table | Clé primaire | Rôle |
| --- | --- | --- |
| documents | id | Métadonnées des fichiers |
| document_classes | document_id, class_id | Ciblage classe |
| document_students | document_id, student_id | Ciblage élève |
| document_subjects | document_id, subject_id | Ciblage matière |

### Messagerie et audit

| Table | Clé primaire | Rôle |
| --- | --- | --- |
| conversations | id | Conversations |
| conversation_participants | conversation_id, user_id | Participants |
| messages | id | Messages |
| audit_logs | id | Journal des actions |

## Contraintes principales

- Email utilisateur unique.
- Un profil élève, professeur ou parent correspond à un seul compte.
- Un élève possède au plus une inscription par année scolaire.
- Une affectation d'enseignement est unique pour professeur + classe + matière + année.
- Une note est unique pour évaluation + élève.
- Une séance est rattachée à une affectation d'enseignement.
- Une absence peut être rattachée à une séance.
- Les dates de fin sont postérieures aux dates de début.
- Le score d'une note est contrôlé par l'application par rapport au barème de son évaluation.
- Les fichiers sont stockés hors PostgreSQL ; la base conserve leur référence.
- Les associations de documents sont limitées par les autorisations métier.
