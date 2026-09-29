# Revue de cohérence documentaire

Date de revue initiale : 2026-09-29  
Dernière mise à jour : 2026-09-29, après validation de la direction des maquettes.

## Références et hiérarchie

En cas d'écart pendant le développement :
1. exigences E6 : documents rectorat 2026 et docs/e6 ;
2. besoin EduGest : cahier des charges + cas d'utilisation + séparation R1/R2 ;
3. architecture : décisions + architecture + diagrammes ;
4. base réellement exécutée : migrations EF Core ;
5. contrat réellement exposé : code API puis documentation API mise à jour ;
6. maquettes : décisions validées dans docs/design/mockups.md, puis application réellement implémentée.

## Points vérifiés

### R1/R2
R1 reste WPF administration/professeurs. R2 reste Web responsive élèves/parents/professeurs. Même API, aucune connexion directe des clients à PostgreSQL.

### Authentification
Identity est la source des comptes/rôles techniques. JWT + refresh token pour les clients. Permissions métier séparées. Connexion commune : aucun choix manuel de rôle.

### Autorisations
La matrice correspond au seed actuel. Les permissions ne remplacent pas le contrôle de périmètre (élève lui-même, parent/enfant, professeur/affectation).

### Données
Le MCD conserve la cible complète. Le MLD et le dictionnaire distinguent le socle EF actuel des modules encore prévus. Les migrations EF sont la source technique de vérité. schema.sql est historique.

### Fonctionnalités
Coefficients/moyennes, justification d'absence, groupes, notifications et certains paramètres restent des évolutions possibles lorsqu'ils ne sont pas suffisamment fixés.

### Maquettes
La direction de conception est validée : identité EduGest bleu/cyan, thèmes clair/sombre, connexion commune, espaces Élève/Parent/Professeur/Administrateur et principes de navigation. Les images sont des références ergonomiques, pas un contrat pixel-perfect. Les captures de l'application finale restent à produire.

### E6
Les documents de préparation couvrent les deux réalisations, maintenance, tests, données et environnement technologique. Les preuves d'exécution restent non cochées tant qu'elles n'existent pas.

## Éléments volontairement non finalisés avant développement

- captures finales de l'application ;
- guides utilisateurs illustrés ;
- rapport de tests exécutés ;
- incident/évolution réellement rencontré ;
- versions finales R1/R2 ;
- déploiement réellement testé ;
- sauvegarde/restauration réellement testée ;
- détails de l'environnement technologique fourni par le centre.

Ces éléments dépendent du développement ou de l'établissement et ne doivent pas être inventés.

## État avant développement

Le besoin, le périmètre, l'architecture, les règles métier principales, les autorisations, les données et la direction UI sont suffisamment cadrés pour reprendre le développement progressivement.

La documentation reste vivante : une décision fonctionnelle prise pendant le code doit mettre à jour le document concerné.
