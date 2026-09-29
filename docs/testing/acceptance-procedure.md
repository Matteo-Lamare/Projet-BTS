# Procédure de recette — R1 et R2

## But

Vérifier qu'une version candidate répond aux besoins documentés avant de la considérer présentable.

## Préparation

Noter le commit/tag, l'environnement, les comptes fictifs utilisés et l'état initial de la base. Exécuter les tests automatisés disponibles.

## Recette commune

Vérifier :
- connexion valide et refus d'une connexion invalide ;
- navigation selon le rôle ;
- refus d'un accès non autorisé ;
- traitement d'une saisie invalide ;
- persistance après rechargement/redémarrage lorsque pertinent ;
- absence d'informations techniques sensibles dans les erreurs.

## R1

Tester au minimum les fonctions réellement retenues parmi référentiels, inscriptions/affectations, notes, assiduité, statistiques et administration. La liste finale dépendra de la version réellement développée.

## R2

Tester sur écran de bureau et terminal mobile les fonctions réellement retenues : tableau de bord, notes, assiduité, documents, emploi du temps et messagerie si réalisée.

## Résultat

Chaque scénario est reporté dans le rapport de tests avec attendu, obtenu et statut. Toute anomalie bloquante est corrigée puis retestée avant validation de la version.
