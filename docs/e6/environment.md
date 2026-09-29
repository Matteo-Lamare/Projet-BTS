# Environnement technologique E6

## Correspondance prévue pour EduGest

| Exigence | Réponse prévue dans le projet | État |
| --- | --- | --- |
| Service d'authentification | ASP.NET Core Identity, JWT et refresh tokens | Socle préparé |
| SGBD | PostgreSQL + Entity Framework Core | Socle préparé |
| Accès sécurisé à Internet | HTTPS via Nginx | À déployer |
| Travail collaboratif | GitHub, Git, historique de commits | Utilisé |
| Sauvegarde | pg_dump/outil PostgreSQL, stockage séparé, rétention | À mettre en œuvre et tester |
| Ressources avec habilitations | rôles + permissions contrôlés par l'API | Socle préparé |
| Deux types de terminaux dont mobile | poste fixe + R2 responsive sur smartphone | À démontrer |
| Environnement de développement avec tests/framework | .NET 8, ASP.NET Core, WPF, xUnit | Socle préparé |
| Au moins deux langages | C# et JavaScript ; HTML/CSS pour la présentation | Prévu |
| Bibliothèque de composants | .NET/NuGet et composants du projet | Prévu |
| Gestion de versions/suivi | GitHub | Utilisé |
| Test des comportements anormaux | tests de validation, authentification et autorisation | Commencé |
| Client lourd | R1 WPF | À réaliser |
| Code navigateur | R2 HTML/Tailwind/JavaScript | Squelette préparé |
| Code serveur | API ASP.NET Core | Socle préparé |

## Sécurité à documenter pendant la mise en œuvre

Le dossier final devra apporter des preuves concrètes pour les mécanismes réellement utilisés : chiffrement des échanges HTTPS, habilitations, gestion des incidents et outils de sécurité disponibles dans l'environnement d'examen. Ne pas déclarer comme réalisé un mécanisme uniquement prévu dans l'architecture.

## Règle de preuve

La conformité finale ne se déduit pas de cette liste. Chaque ligne doit être vérifiée sur l'environnement réellement présenté le jour de l'épreuve et accompagnée, lorsque pertinent, d'une capture, configuration, procédure ou démonstration.
