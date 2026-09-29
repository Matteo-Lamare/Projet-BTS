# Plan de tests et preuves

## Objectif

Conserver des preuves compréhensibles des validations réalisées, sans remplacer le travail de développement du candidat.

## Catégories

### Authentification et habilitations
- connexion valide et invalide ;
- accès sans jeton : 401 ;
- accès authentifié sans permission : 403 ;
- accès avec permission : succès ;
- renouvellement et révocation du refresh token.

### Données métier
Pour chaque fonctionnalité développée : scénario nominal, données invalides, doublon lorsque pertinent, ressource inexistante, règle métier et autorisation.

### Intégration
Tester les parcours entre API, authentification et persistance. Les tests doivent être rejouables et versionnés.

### Non-régression
Lors d'une correction, ajouter ou conserver un test reproduisant le problème lorsque cela est pertinent.

### Base de données
- migrations reproductibles ;
- contraintes vérifiées ;
- sauvegarde ;
- restauration réellement testée avant l'épreuve.

## Rapport de test final

Le rapport final indiquera : version/commit testé, environnement, scénario, prérequis, résultat attendu, résultat obtenu et statut. Les captures ou journaux utiles pourront être annexés.
