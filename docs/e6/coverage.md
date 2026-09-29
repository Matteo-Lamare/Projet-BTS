# Couverture E6 SLAM — confrontation aux documents rectorat 2026

## Principe

Cette matrice reprend les intitulés de sous-compétences de la grille d'aide à l'évaluation 2026. Elle ne constitue pas une auto-validation : une compétence n'est couverte que par une réalisation réellement opérationnelle, maîtrisée par le candidat et accompagnée de preuves.

## Réalisations support

- **R1 — EduGest Desktop** : client lourd WPF, orienté administration/professeurs.
- **R2 — EduGest Web** : application Web responsive, orientée élèves/parents/professeurs.

Les deux réalisations doivent, ensemble, couvrir toutes les compétences du bloc 2. Une réalisation absente entraîne 10 points de pénalité ; l'absence des deux entraîne 20 points. Un environnement technologique non conforme peut entraîner jusqu'à 15 points de pénalité.

## Concevoir et développer une solution applicative

| Sous-compétence officielle | Couverture EduGest visée | Preuves à produire |
| --- | --- | --- |
| Analyser un besoin exprimé et son contexte juridique | R1 + R2 | cahier des charges, acteurs, données personnelles, règles d'accès |
| Participer à la conception de l'architecture d'une solution applicative | R1 + R2 | architecture, décisions techniques, flux clients/API/BDD |
| Modéliser une solution applicative | R1 + R2 | MCD, MLD, modèle EF, diagrammes utiles |
| Exploiter les ressources du cadre applicatif (framework) | R1 + R2 | .NET/ASP.NET Core/EF Core ; WPF pour R1 |
| Identifier, développer, utiliser ou adapter des composants logiciels | R1 + R2 | composants/services réellement utilisés et expliqués |
| Exploiter les technologies Web pour mettre en œuvre les échanges entre applications, y compris de mobilité | R1 + R2 | API REST, JWT ; R2 responsive ; SignalR si réalisé |
| Utiliser des composants d'accès aux données | R1 + R2 | EF Core/PostgreSQL, requêtes et migrations |
| Intégrer en continu les versions d'une solution applicative | R1 + R2 | GitHub, commits, GitHub Actions, documentation des versions |
| Réaliser les tests nécessaires à la validation ou à la mise en production d'éléments adaptés ou développés | R1 + R2 | tests unitaires/fonctionnels/intégration, résultats |
| Rédiger des documentations technique et d'utilisation d'une solution applicative | R1 + R2 | documentation technique + guides utilisateurs |
| Exploiter les fonctionnalités d'un environnement de développement et de tests | R1 + R2 | IDE, débogage, tests, Git, environnement reproductible |

## Assurer la maintenance corrective ou évolutive

| Sous-compétence officielle | Stratégie EduGest | Preuves à produire |
| --- | --- | --- |
| Recueillir, analyser et mettre à jour les informations sur une version | prévoir une évolution identifiée de R1 ou R2 | issue/besoin, version concernée, commit |
| Évaluer la qualité d'une solution applicative | revue d'une version avant/après évolution | tests, ergonomie, conformité au besoin |
| Analyser et corriger un dysfonctionnement | conserver au moins un cas réel de correction | incident/bug, diagnostic, correction, commit |
| Mettre à jour des documentations technique et d'utilisation | intégrer la documentation au workflow Git | diff documentaire lié à l'évolution |
| Élaborer et réaliser les tests des éléments mis à jour | test de correction + non-régression | test automatisé ou protocole reproductible |

**Écart actuel important :** cette compétence ne doit pas être couverte artificiellement par le seul développement initial. Il faut conserver de vraies traces d'une correction ou évolution réalisée et maîtrisée par le candidat.

## Gérer les données

| Sous-compétence officielle | Couverture EduGest visée | Preuves à produire |
| --- | --- | --- |
| Exploiter des données à l'aide d'un langage de requêtes | R1 + R2 | LINQ/EF Core et, si pertinent, SQL PostgreSQL |
| Développer des fonctionnalités applicatives au sein d'un SGBD | à démontrer selon les fonctionnalités réellement retenues | contraintes/traitements côté BDD si réalisés |
| Concevoir ou adapter une base de données | Oui | MCD/MLD, migrations, contraintes |
| Administrer et déployer une base de données | Oui, à finaliser | PostgreSQL opérationnel, déploiement, sauvegarde/restauration testée |

## Critères transversaux à ne pas oublier

La grille 2026 cite explicitement :
- adéquation au cahier des charges et prise en compte du contexte juridique ;
- maquettes et contraintes ergonomiques ;
- tests unitaires, fonctionnels, d'intégration et de non-régression selon le contexte ;
- service Web et accès aux données persistantes ;
- outil collaboratif de gestion des versions et des itérations ;
- documentation des versions pour l'intégration continue ;
- composants documentés pour être réutilisés ;
- documentation technique et utilisateur à jour ;
- solution opérationnelle et stable ;
- habilitations sur les données ;
- sauvegarde planifiée et tests de restauration.

## État honnête du projet

Le dépôt contient actuellement une documentation de cadrage et un socle/template backend. Cela ne suffit pas à valider les compétences. Les preuves finales seront constituées à partir du code, des interfaces, des tests, des corrections et du déploiement réellement réalisés et expliqués par le candidat.
