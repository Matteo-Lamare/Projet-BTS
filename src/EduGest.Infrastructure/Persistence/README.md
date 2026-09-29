# Persistance

EF Core est la source d'évolution du schéma de développement.

- PostgreSQL est le SGBD cible.
- `EduGestDbContext` centralise l'accès aux données.
- Les migrations EF Core seront versionnées dans le dépôt.
- `database/schema.sql` reste une représentation documentaire/démonstrative.
- Aucun client (WPF ou Web) n'accède directement à PostgreSQL.
