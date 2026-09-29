# Modèle logique de données

## Statut et source de vérité

Ce document sépare le **socle actuellement matérialisé par EF Core** de la **cible fonctionnelle encore à développer**. Les migrations EF Core sont la source technique de vérité. `database/schema.sql` est un ancien schéma documentaire conservé pour historique et ne doit pas être exécuté.

ASP.NET Core Identity fournit les tables techniques de comptes et de rôles ; elles ne sont donc pas redéfinies ici comme des tables métier `users` / `roles`.

## Socle actuel

### Identité et profils
- tables ASP.NET Core Identity : comptes, rôles et associations techniques ;
- permissions ;
- role_permissions ;
- students ;
- teachers ;
- parents ;
- parent_students ;
- refresh_tokens.

### Organisation scolaire
- academic_years ;
- classes ;
- subjects ;
- enrollments ;
- teaching_assignments.

### Scolarité
- assessments ;
- grades ;
- rooms ;
- sessions ;
- attendance_events.

Les identifiants du domaine sont des `Guid` dans le modèle actuel.

## Cible fonctionnelle non encore matérialisée dans le DbContext

### Documents
- documents ;
- document_classes ;
- document_students ;
- document_subjects.

### Messagerie
- conversations ;
- conversation_participants ;
- messages.

### Audit
- audit_logs.

Ces tables restent dans le MCD cible parce qu'elles appartiennent au périmètre prévu, mais leur structure définitive sera arrêtée au moment de leur développement.

## Contraintes actuellement retenues

- un profil Student/Teacher/Parent référence un compte Identity ;
- un profil d'un même type possède un UserId unique ;
- une classe appartient à une année scolaire ;
- couple classe/année unique selon le nom ;
- code matière unique ;
- inscription Student/Class unique ;
- affectation Teacher/Class/Subject unique ;
- note Assessment/Student unique ;
- séance rattachée à une affectation, salle facultative ;
- contrôles métier complémentaires dans l'application : dates, barème, inscription de l'élève et périmètre d'autorisation.

## Règle d'évolution

Toute évolution de la base doit rester cohérente entre entités/configurations EF, migration, tests, dictionnaire de données, MCD/MLD et contrat API.
