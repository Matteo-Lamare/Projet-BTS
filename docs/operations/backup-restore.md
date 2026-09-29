# Sauvegarde et restauration PostgreSQL

## Statut
Procédure préparatoire, non validée tant qu'elle n'a pas été réellement exécutée.

## Sauvegarde cible
Utiliser l'outillage PostgreSQL adapté (par exemple pg_dump) dans l'environnement autorisé. Sauvegarde datée, protégée et stockée séparément du volume principal. Aucun identifiant réel dans Git.

## Test de restauration
1. identifier sauvegarde et version ;
2. restaurer vers une base de TEST vide ;
3. utiliser l'outil PostgreSQL adapté au format ;
4. connecter l'API à la base restaurée ;
5. vérifier des données de référence et un parcours ;
6. noter résultat, durée et anomalies ;
7. conserver une preuve sans donnée personnelle réelle.

## Rapport à compléter
Date ; commit/version ; environnement ; sauvegarde ; commande/outillage ; résultat ; contrôles ; durée ; anomalie/correction ; preuve.

Une sauvegarde n'est considérée comme validée pour le dossier qu'après restauration effectivement testée et documentée.
