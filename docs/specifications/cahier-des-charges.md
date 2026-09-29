# Cahier des charges — EduGest

## 1. Contexte

EduGest est une solution de gestion scolaire réalisée dans le cadre du BTS SIO option SLAM. Le besoin est de centraliser les informations utiles à la scolarité tout en proposant des interfaces adaptées aux différents profils.

Le projet comprend deux réalisations :
- R1 : application lourde WPF pour l'administration et les professeurs ;
- R2 : application Web responsive pour les élèves, parents et professeurs.

Les deux réalisations consomment une API commune et partagent les mêmes données.

## 2. Objectifs

- centraliser élèves, professeurs, classes, matières et affectations ;
- gérer évaluations, notes, absences et retards ;
- permettre une consultation adaptée aux habilitations ;
- proposer documents, emploi du temps et messagerie dans le périmètre prévu ;
- assurer traçabilité, sécurité, sauvegarde et possibilité de maintenance ;
- fournir deux applications opérationnelles et explicables par le candidat.

## 3. Acteurs

**Élève** : consulte ses données scolaires, documents et emploi du temps ; utilise la messagerie autorisée.

**Parent** : consulte les données autorisées de ses enfants. La messagerie parent est hors périmètre initial.

**Professeur** : consulte les référentiels nécessaires et gère les données relevant de ses affectations.

**Administration** : gère les référentiels scolaires et consulte les informations nécessaires au fonctionnement de l'établissement.

**Administrateur** : dispose des fonctions d'administration des comptes, rôles, permissions, paramètres et audit.

## 4. Périmètre fonctionnel

### R1
Authentification, élèves, professeurs, classes, matières, années scolaires, inscriptions, affectations, évaluations, notes, absences/retards, séances, documents selon droits, statistiques et audit selon habilitation.

### R2
Authentification/profil, tableau de bord, notes, absences/retards, documents, emploi du temps et messagerie. Les parents ne disposent pas de messagerie dans la première version.

## 5. Contraintes fonctionnelles

- un utilisateur doit être authentifié avant d'accéder aux données protégées ;
- les droits sont contrôlés par le serveur ;
- un professeur agit uniquement dans son périmètre autorisé ;
- un élève ne consulte que ses propres données ;
- un parent ne consulte que les données des enfants qui lui sont rattachés ;
- une note correspond à un élève et une évaluation ;
- une seule note existe pour un couple élève/évaluation ;
- une note reste comprise entre 0 et le barème de l'évaluation ;
- les suppressions susceptibles de casser l'historique sont refusées ou encadrées ;
- les fichiers et actions sensibles sont soumis aux contrôles d'autorisation.

## 6. Contraintes techniques retenues

- C#/.NET ;
- WPF pour R1 ;
- HTML, Tailwind CSS et JavaScript vanilla pour R2 ;
- ASP.NET Core Web API ;
- PostgreSQL et Entity Framework Core ;
- ASP.NET Core Identity, JWT et refresh tokens ;
- Git/GitHub ;
- xUnit ;
- déploiement cible Linux/Ubuntu avec HTTPS.

Ces technologies sont des choix du projet et non des obligations générales de l'épreuve E6.

## 7. Qualité attendue

- interfaces compréhensibles et cohérentes ;
- validation des saisies ;
- messages d'erreur exploitables sans information sensible ;
- tests des scénarios nominaux et anormaux ;
- documentation maintenue avec les évolutions ;
- secrets et données personnelles réelles absents du dépôt ;
- solution suffisamment simple pour être maintenue et expliquée pendant l'épreuve.

## 8. Hors périmètre initial

- paiement ;
- gestion comptable ;
- visioconférence ;
- messagerie privée élève à élève ;
- messagerie parent ;
- architecture microservices ;
- application mobile native dédiée, sauf évolution ultérieure validée.

## 9. Critères de recette

Une fonctionnalité est recevable lorsqu'elle répond au besoin documenté, respecte les habilitations, traite les erreurs principales, est testée, ne compromet pas les données existantes et peut être démontrée dans l'environnement prévu.
