# Périmètre fonctionnel

Ce document décrit la cible fonctionnelle. Les éléments explicitement marqués « évolution possible » ne font pas partie du périmètre acquis tant qu'ils n'ont pas été décidés puis documentés.

## Comptes et accès
- Connexion et déconnexion.
- Gestion du profil.
- Gestion des utilisateurs, rôles et permissions.
- Réinitialisation sécurisée de l'accès : modalité à décider si cette fonction est retenue.

## Élèves
- Créer, modifier et supprimer un élève selon les dépendances.
- Consulter sa fiche.
- L'inscrire dans une classe.
- Consulter les données autorisées.

## Professeurs
- Créer, modifier et supprimer selon les dépendances.
- Consulter la fiche.
- Affecter matières/classes.
- Consulter les élèves du périmètre autorisé.

## Classes et matières
- CRUD selon règles métier.
- Inscriptions et affectations d'enseignement.
- Les groupes de classe sont une évolution possible.

## Notes
- Créer une évaluation.
- Saisir/modifier les notes.
- Consulter les résultats selon les droits.
- Coefficients et calcul automatique de moyennes : **évolutions possibles**, à décider avant implémentation.

## Absences et retards
- Déclarer, modifier/supprimer selon droits et consulter l'historique.
- Justification d'absence : **évolution possible**, à décider avant implémentation.

## Documents
- Déposer, télécharger, classer et supprimer selon droits.
- Ciblage classe/élève/matière.
- Contrôle des accès.

## Messagerie
- Élève ↔ professeur.
- Diffusion groupe/classe.
- Historique.
- Temps réel via SignalR si la fonctionnalité est réalisée comme prévu.
- Notifications supplémentaires : évolution possible.

## Emploi du temps
- Séances associées à une affectation et éventuellement une salle.
- Consultation selon rôle.
- L'édition complète de planning par interface sera précisée lors du développement.

## Tableau de bord
Afficher des informations utiles au rôle connecté. Le contenu exact sera décidé avec les maquettes afin de ne pas imposer prématurément moyenne, messages ou indicateurs non encore réalisés.

## Administration et suivi
- Gestion des comptes/rôles/permissions selon périmètre.
- Années scolaires.
- Statistiques prévues pour R1.
- Journal des actions prévu.
- Paramètres d'établissement : contenu exact à définir avant implémentation.
