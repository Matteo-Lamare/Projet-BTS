# Standards de développement

## C#

- Nullable Reference Types activées.
- Async/await pour les opérations I/O.
- Pas d'accès PostgreSQL depuis les clients.
- Pas d'entités EF exposées directement par l'API.
- Validation côté serveur obligatoire.
- Exceptions métier distinctes des erreurs techniques.

## API

- Routes REST cohérentes.
- DTO séparés des entités.
- Codes HTTP explicites.
- Autorisation côté serveur.
- Aucun secret dans les réponses.
- Journalisation des opérations sensibles.

## Base de données

- Contraintes d'intégrité définies au niveau PostgreSQL lorsque possible.
- Règles dépendant de plusieurs tables contrôlées par l'application.
- Migrations EF Core versionnées.
- Index ajoutés lorsqu'un besoin de recherche est identifié.
- Données de démonstration anonymisées.

## Tests

Une fonctionnalité importante doit comporter au minimum : tests des règles métier principales, scénario nominal, refus d'autorisation lorsque pertinent et tests d'intégration des endpoints critiques.

## Git

Branches : feature/*, fix/*, docs/*.

Commits : feat: fonctionnalité ; fix: correction ; test: tests ; docs: documentation ; refactor: restructuration ; chore: maintenance.

Les commits doivent rester petits et explicites.

## Definition of Done

Une fonctionnalité est terminée lorsque :
1. elle compile ;
2. ses validations existent ;
3. les règles d'autorisation sont vérifiées ;
4. les tests pertinents passent ;
5. la documentation nécessaire est à jour ;
6. aucun secret ou donnée réelle n'est ajouté ;
7. le changement est poussé sur GitHub.