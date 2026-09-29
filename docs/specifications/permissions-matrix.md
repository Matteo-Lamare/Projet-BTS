# Matrice des rôles et permissions

## Principe

Les permissions techniques sont contrôlées côté API. Cette matrice reprend le seed actuellement préparé. Les restrictions de **périmètre métier** restent nécessaires en plus de la permission : par exemple, un professeur possédant `grades.write` ne doit modifier que les notes liées à ses affectations.

| Permission | Élève | Parent | Professeur | Administration | Administrateur |
| --- | :---: | :---: | :---: | :---: | :---: |
| students.read |  |  | ✓ | ✓ | ✓ |
| students.write |  |  |  | ✓ | ✓ |
| teachers.read |  |  |  | ✓ | ✓ |
| teachers.write |  |  |  | ✓ | ✓ |
| classes.read |  |  | ✓ | ✓ | ✓ |
| classes.write |  |  |  | ✓ | ✓ |
| subjects.read |  |  | ✓ | ✓ | ✓ |
| subjects.write |  |  |  | ✓ | ✓ |
| grades.read | ✓ | ✓ | ✓ | ✓ | ✓ |
| grades.write |  |  | ✓ |  | ✓ |
| attendance.read | ✓ | ✓ | ✓ | ✓ | ✓ |
| attendance.write |  |  | ✓ |  | ✓ |
| documents.read | ✓ | ✓ | ✓ | ✓ | ✓ |
| documents.write |  |  | ✓ | ✓ | ✓ |
| messages.read | ✓ |  | ✓ |  | ✓ |
| messages.write | ✓ |  | ✓ |  | ✓ |
| timetable.read | ✓ | ✓ | ✓ | ✓ | ✓ |
| statistics.read |  |  |  | ✓ | ✓ |
| users.manage |  |  |  |  | ✓ |
| roles.manage |  |  |  |  | ✓ |
| audit.read |  |  |  |  | ✓ |
| settings.manage |  |  |  |  | ✓ |

## Périmètres métier à ajouter/garantir

- Élève : uniquement ses propres notes, absences, documents et emploi du temps.
- Parent : uniquement les enfants liés par `ParentStudent`.
- Professeur : uniquement les classes/matières correspondant à ses affectations.
- Administration : droits fonctionnels du tableau ; pas de gestion des rôles/comptes sensibles par défaut.
- Administrateur : toutes les permissions.

La présence d'une permission n'autorise jamais à contourner ces règles de périmètre.
