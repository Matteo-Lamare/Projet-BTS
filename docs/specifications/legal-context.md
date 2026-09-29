# Contexte juridique et données — cadrage du projet

## Portée

Ce document sert à identifier les enjeux juridiques du projet EduGest. Il ne constitue pas une attestation de conformité juridique.

## Données concernées

EduGest prévoit de traiter notamment :
- identité et compte utilisateur ;
- rattachement à une classe ou à un rôle ;
- notes et évaluations ;
- absences et retards ;
- relations parent-enfant ;
- documents scolaires ;
- messages lorsque la messagerie est réalisée ;
- traces d'actions administratives.

Ces informations sont liées à des personnes identifiables et doivent donc être traitées avec prudence. Le dépôt Git ne doit contenir que des données fictives/anonymisées pour le développement et la démonstration.

## Principes retenus dans la conception

- ne collecter que les informations nécessaires aux fonctionnalités retenues ;
- limiter l'accès selon le rôle et le périmètre métier ;
- protéger l'authentification et les échanges ;
- ne jamais versionner mots de passe, secrets ou données personnelles réelles ;
- limiter les informations sensibles présentes dans les logs ;
- prévoir sauvegarde et restauration contrôlées ;
- documenter les finalités des données utilisées dans la démonstration ;
- supprimer ou anonymiser les données de démonstration qui ne sont plus nécessaires.

## Points à faire valider

Les documents rectorat demandent d'analyser le besoin et son contexte juridique mais ne fixent pas, dans les documents étudiés, les durées de conservation, bases juridiques ou procédures exactes propres à un établissement scolaire réel.

Avant de présenter EduGest comme déployable dans un établissement réel, il faudrait donc faire préciser/valider notamment :
- responsable du traitement et finalités exactes ;
- base juridique applicable à chaque traitement ;
- durées de conservation ;
- information des personnes ;
- droits des personnes et procédure d'exercice ;
- éventuels sous-traitants/hébergeurs ;
- politique de sauvegarde et de suppression ;
- règles spécifiques aux données de mineurs.

Pour le projet BTS, les choix non confirmés sont présentés comme hypothèses de conception et non comme une conformité juridique acquise.
