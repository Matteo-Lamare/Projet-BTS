# Découpage des applications

## Principe d'architecture

Les deux applications utilisent une API et une base de données communes. Les règles d'autorisation sont appliquées par l'API ; une restriction d'interface ne remplace jamais un contrôle côté serveur.

## Application légère

Destinée aux élèves et aux professeurs.

| Fonctionnalité | Élève | Professeur |
| --- | --- | --- |
| Connexion et profil | Consulter et modifier son profil | Consulter et modifier son profil |
| Notes | Consulter ses résultats | Créer et modifier les notes de ses classes |
| Absences et retards | Consulter ses informations | Déclarer et modifier les informations de ses classes |
| Documents | Consulter et télécharger | Déposer et gérer les documents de ses classes |
| Messagerie | Envoyer et recevoir | Envoyer et recevoir |
| Emploi du temps | Consulter | Consulter |
| Tableau de bord | Consulter | Consulter |

## Application lourde

Destinée au personnel administratif et aux administrateurs.

| Fonctionnalité | Administration | Administrateur |
| --- | --- | --- |
| Élèves | Gérer | Gérer |
| Professeurs | Gérer | Gérer |
| Classes et matières | Gérer | Gérer |
| Documents | Gérer | Gérer |
| Utilisateurs et rôles | Consulter selon besoin | Gérer |
| Paramètres | Non | Gérer |
| Statistiques | Consulter | Consulter |
| Journal des actions | Consulter selon besoin | Consulter |

## Responsabilités partagées

| Élément | Règle |
| --- | --- |
| API | Expose les données et applique les règles métier et les autorisations |
| Base de données | Source unique des données de l'établissement |
| Authentification | Gérée de façon centralisée pour les deux applications |
| Journalisation | Conserve les actions sensibles d'administration |
| Fichiers | Stockés et servis selon des droits vérifiés par l'API |

## Hors périmètre initial

Les notifications en temps réel, les statistiques avancées et les intégrations externes sont reportées après la mise en place du socle fonctionnel et de sécurité.
