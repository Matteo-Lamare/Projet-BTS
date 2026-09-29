# Découpage des applications

## Principe

Les deux applications utilisent la même API et la même base de données. L'API est la seule frontière d'accès aux données et applique toutes les règles métier et d'autorisation.

## R1 — application lourde WPF

Public principal : administration et professeurs.

| Fonctionnalité | Administration | Professeur |
| --- | --- | --- |
| Connexion / profil | Oui | Oui |
| Élèves | CRUD | Consultation des classes affectées |
| Professeurs | CRUD | Consultation |
| Classes / matières | CRUD | Consultation |
| Affectations d'enseignement | Gestion | Consultation |
| Évaluations | Gestion | Gestion de ses affectations |
| Notes | Gestion | Gestion de ses affectations |
| Absences / retards | Gestion | Gestion de ses classes |
| Documents | Gestion | Dépôt et gestion selon périmètre |
| Statistiques | Consultation | Selon droits |
| Journal d'actions | Consultation selon droits | Non par défaut |

## R2 — application web responsive

Public principal : élèves et parents ; accès également prévu pour les professeurs.

| Fonctionnalité | Élève | Parent | Professeur |
| --- | --- | --- | --- |
| Connexion / profil | Oui | Oui | Oui |
| Notes | Ses notes | Notes de ses enfants | Ses classes |
| Absences / retards | Ses données | Données de ses enfants | Ses classes |
| Documents | Documents autorisés | Documents autorisés de ses enfants | Dépôt / consultation autorisés |
| Emploi du temps | Oui | Oui, pour les enfants | Oui |
| Messagerie | Avec professeurs / classe | Hors périmètre initial | Avec élèves / classe |
| Tableau de bord | Oui | Oui | Oui |

## Règles de messagerie

- Les élèves peuvent échanger avec les professeurs.
- Un élève peut écrire à un groupe/classe.
- La messagerie privée élève ↔ élève est hors périmètre.
- Les parents n'utilisent pas la messagerie dans la première version.
- SignalR sert uniquement à la diffusion temps réel des nouveaux messages ; REST reste utilisé pour l'historique.

## Responsabilités partagées

| Élément | Règle |
| --- | --- |
| API | Données, règles métier, authentification et autorisation |
| Base de données | Source unique des données |
| Authentification | ASP.NET Core Identity + JWT + refresh token |
| Fichiers | Stockage serveur/volume, métadonnées en base |
| Audit | Journal distinct des logs techniques |
