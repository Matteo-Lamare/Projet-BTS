# Installation et lancement — document de préparation

## Statut

Cette procédure sera finalisée lorsque R1, R2 et le déploiement seront stabilisés. Elle distingue le socle actuel de la future installation de démonstration.

## Prérequis de développement prévus

- Git ;
- SDK .NET 8 ;
- PostgreSQL pour l'environnement réel de développement ;
- Node.js/npm uniquement pour construire les ressources Tailwind de R2 ;
- IDE compatible .NET/WPF.

Les versions exactes utilisées pour l'épreuve devront être consignées avant la livraison.

## Récupération

Cloner le dépôt puis se placer sur le commit/tag prévu. Ne jamais recopier dans le dépôt des secrets ou identifiants réels.

## Configuration locale

Renseigner la connexion PostgreSQL et le secret JWT par User Secrets ou variables d'environnement. Les valeurs CHANGE_ME des fichiers versionnés ne sont pas destinées à un déploiement réel.

L'initialisation du premier compte administrateur doit également utiliser une configuration non secrète dans Git.

## Base de données

Les migrations EF Core sont la source de vérité. La commande exacte et le scénario d'initialisation seront figés après validation de l'environnement final. Le fichier database/schema.sql est historique et ne doit pas servir à créer la base actuelle.

## API

La procédure finale devra préciser : commande de lancement, URL/port, environnement, vérification de santé ou requête de contrôle et compte de démonstration.

## R1 WPF

À compléter lorsque l'interface aura été développée : build, lancement, configuration de l'URL API et prérequis du poste.

## R2 Web

À compléter après les essais de maquettes et l'implémentation : installation npm, compilation Tailwind, fichiers servis et URL d'accès.

## Critère de fin

Avant E6, un poste propre doit pouvoir suivre cette procédure sans dépendre d'informations présentes uniquement dans une conversation.
