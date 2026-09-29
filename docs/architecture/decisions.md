# Décisions d'architecture

## ADR-11 — Entités de domaine et EF Core

Les premières entités C# reprennent les objets structurants du MCD validé : utilisateurs, rôles, permissions, élèves, professeurs, parents, classes, matières et années scolaires.

EF Core mappe ces entités dans `EduGestDbContext`. Les contraintes simples (unicité, longueurs, clés étrangères) sont déclarées dans le mapping ; les règles métier impliquant plusieurs objets restent dans la couche Application.

La génération de migration doit être réalisée par l'outil EF Core à partir de ce modèle, et non écrite manuellement.