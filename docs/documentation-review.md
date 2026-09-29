# Revue de cohérence documentaire

Date de revue : 2026-09-29

## Références et hiérarchie

En cas d'écart pendant le développement :
1. exigences E6 : documents rectorat 2026 et docs/e6 ;
2. besoin EduGest : cahier des charges + cas d'utilisation + séparation R1/R2 ;
3. architecture : décisions + architecture + diagrammes ;
4. base réellement exécutée : migrations EF Core ;
5. contrat réellement exposé : code API puis documentation API mise à jour ;
6. maquettes : uniquement versions explicitement validées avec le candidat.

## Points vérifiés

### R1/R2
R1 reste WPF administration/professeurs. R2 reste Web responsive élèves/parents/professeurs. Même API, aucune connexion directe des clients à PostgreSQL.

### Authentification
Identity est la source des comptes/rôles techniques. JWT + refresh token pour les clients. Permissions métier séparées.

### Autorisations
La matrice correspond au seed actuel. Les permissions ne remplacent pas le contrôle de périmètre (élève lui-même, parent/enfant, professeur/affectation).

### Données
Le MCD conserve la cible complète. Le MLD et le dictionnaire distinguent maintenant le socle EF actuel des modules encore prévus. Les migrations EF sont la source technique de vérité. schema.sql est historique.

### Fonctionnalités
Coefficients/moyennes, justification d'absence, groupes, notifications et certains paramètres ont été reclassés en évolutions possibles lorsqu'ils ne sont pas suffisamment fixés dans le modèle actuel.

### E6
Les deux réalisations, maintenance, tests, documentation, données et environnement technologique sont couverts par des documents de préparation. Les preuves d'exécution restent volontairement non cochées/non remplies tant qu'elles n'existent pas.

### Maquettes
Aucune maquette visuelle n'est validée. Elles seront construites et testées avec le candidat après cette revue.

## Éléments qui ne peuvent pas être finalisés maintenant

- captures et maquettes finales ;
- guides utilisateurs illustrés ;
- rapport de tests exécutés ;
- incident/évolution réellement rencontré ;
- versions finales R1/R2 ;
- procédure de déploiement réellement testée ;
- sauvegarde/restauration réellement testée ;
- détails de l'environnement technologique fourni par le centre.

Ces éléments dépendent du développement ou de l'établissement et ne doivent pas être inventés.

## Conclusion

La documentation de cadrage est suffisamment cohérente pour passer à la conception visuelle des maquettes. Elle reste un ensemble vivant : toute décision prise pendant les essais de maquettes ou le développement doit mettre à jour les documents concernés.
