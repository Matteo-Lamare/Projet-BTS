# Dictionnaire de données — modèle cible et socle actuel

## Source de vérité

Pendant le développement, **les migrations Entity Framework Core constituent la source technique de vérité**. Le MCD/MLD décrivent la cible fonctionnelle. Certaines entités prévues (documents, messagerie, audit) ne sont pas encore implémentées dans le DbContext actuel.

## Identité et sécurité

Les comptes et rôles techniques sont fournis par ASP.NET Core Identity avec identifiants `Guid`. Les tables Identity ne sont pas redéfinies manuellement dans ce document.

| Entité métier actuelle | Champs principaux | Contraintes principales |
| --- | --- | --- |
| Permission | Id, Name | Name requis, max 150, unique |
| RolePermission | RoleId, PermissionId | clé composée |
| RefreshToken | Id, UserId, TokenHash, ExpiresAt, CreatedAt, RevokedAt | hash requis/unique ; lié à Identity User |
| Student | Id, UserId, FirstName, LastName, BirthDate | UserId unique ; noms max 100 |
| Teacher | Id, UserId, FirstName, LastName | UserId unique ; noms max 100 |
| Parent | Id, UserId, FirstName, LastName | UserId unique ; noms max 100 |
| ParentStudent | ParentId, StudentId | clé composée |

## Organisation scolaire

| Entité | Champs principaux | Contraintes |
| --- | --- | --- |
| AcademicYear | Id, Label, StartDate, EndDate | Label requis max 20 ; cohérence des dates contrôlée par l'application |
| Class | Id, Name, AcademicYearId | Name max 100 ; couple Name/AcademicYearId unique |
| Subject | Id, Name, Code | Name max 150 ; Code max 30 et unique |
| Enrollment | Id, StudentId, ClassId, EnrolledAt | couple StudentId/ClassId unique |
| TeachingAssignment | Id, TeacherId, ClassId, SubjectId | triplet unique |

## Scolarité

| Entité | Champs principaux | Contraintes |
| --- | --- | --- |
| Assessment | Id, TeachingAssignmentId, Title, AssessmentDate, MaxScore | titre max 200 ; score max précision 5,2 |
| Grade | Id, AssessmentId, StudentId, Score, Comment | une note par évaluation/élève ; commentaire max 1000 |
| Room | Id, Name | nom max 100, unique |
| Session | Id, TeachingAssignmentId, RoomId, StartsAt, EndsAt | salle facultative ; fin après début contrôlée par l'application |
| AttendanceEvent | Id, StudentId, SessionId, OccurredAt, Type, Reason | type requis max 30 ; motif max 1000 ; séance facultative |

## Cible non encore implémentée dans le DbContext

Documents et associations de ciblage, conversations/participants/messages et journal d'audit restent dans le MCD/MLD cible et seront précisés lorsqu'ils seront développés.

## Règle d'évolution

Toute modification du modèle doit mettre à jour, selon le cas : entité, configuration EF, migration, tests, MCD/MLD et ce dictionnaire.
