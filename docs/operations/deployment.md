# Déploiement — cible EduGest

## Statut
Architecture cible à adapter à l'environnement réel.

## Flux
Client WPF ou navigateur -> HTTPS/Nginx -> ASP.NET Core API -> PostgreSQL.

## Environnements
DEV : développement ; TEST : validation ; PROD : livraison/démonstration. Configurations et secrets séparés.

## Livraison à documenter
Identifier commit/tag ; vérifier build/tests ; sauvegarder si nécessaire ; déployer ; appliquer les migrations contrôlées ; vérifier HTTPS ; recette minimale ; documenter résultat ; prévoir retour arrière.

## À valider
Docker Compose, Nginx, certificats, volumes, adresse, deuxième serveur/OS et outils de sécurité doivent être confrontés à l'environnement réel avant d'en faire une procédure finale.
