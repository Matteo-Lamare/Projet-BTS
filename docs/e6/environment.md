# Environnement technologique E6 SLAM — exigences rectorat 2026

## Avertissement

La conformité porte sur **l'environnement technologique présenté au centre d'examen**, pas uniquement sur le dépôt EduGest. Les lignes ci-dessous doivent donc être confirmées avec l'établissement et testées avant l'épreuve.

## Environnement commun aux deux options

| Exigence 2026 | Réponse EduGest / centre envisagée | Situation actuelle |
| --- | --- | --- |
| Service d'authentification | ASP.NET Core Identity + JWT | Socle préparé |
| SGBD | PostgreSQL | Prévu/socle préparé |
| Accès sécurisé à Internet | réseau du centre + HTTPS | À vérifier |
| Environnement de travail collaboratif | GitHub/Git | Utilisé |
| Deux serveurs, éventuellement virtualisés, sur des OS différents, dont un open source | à identifier dans l'infrastructure du centre | **À confirmer avec le centre** |
| Solution de sauvegarde | sauvegarde PostgreSQL + stockage prévu par le centre | À mettre en œuvre et tester |
| Ressources sécurisées et soumises à habilitation | rôles/permissions EduGest + ressources du centre | Partiellement préparé |
| Deux types de terminaux dont un mobile | poste fixe + smartphone/tablette | À démontrer |

## Outils de sécurité à mobiliser dans l'environnement

Le document rectorat demande des outils pour :
- gestion des incidents ;
- détection et prévention des intrusions ;
- chiffrement ;
- analyse de trafic.

Ces éléments ne doivent pas être attribués à EduGest sans preuve. Ils doivent être identifiés avec les outils réellement disponibles dans le centre d'examen.

## Exigences spécifiques SLAM

| Exigence | Réponse visée | État |
| --- | --- | --- |
| Un ou deux environnements de développement avec outils de tests, framework et au moins deux langages | .NET/ASP.NET Core/WPF + C# ; Web + JavaScript | À démontrer sur l'environnement réel |
| Bibliothèque de composants logiciels | NuGet/.NET et composants du projet | À documenter |
| SGBD avec langage de programmation associé | PostgreSQL ; modalités exactes à préciser avec le centre | À confirmer |
| Gestion de versions et suivi de problèmes logiciels | GitHub | Versionnement utilisé ; suivi de problèmes à organiser si retenu |
| Solution pour tester les comportements anormaux | xUnit/tests API et outils du centre | Commencé |
| Code sur accès fixe de type client lourd | R1 WPF | À réaliser |
| Code dans un navigateur Web | R2 | Squelette seulement |
| Code sur OS d'une solution d'accès mobile | à distinguer du simple responsive Web | **À clarifier avec l'établissement** |
| Code sur OS d'un serveur | API ASP.NET Core | Socle préparé |

## Solutions applicatives du contexte

Le document 2026 indique que les activités de l'organisation cliente s'appuient sur **au moins deux solutions applicatives opérationnelles** offrant un accès sécurisé à des données hébergées sur un site distant. Leur architecture doit exploiter des appels à des services applicatifs distants et répondre aux situations prévues par l'annexe.

Conséquence : R1 et R2 EduGest peuvent contribuer au contexte, mais il ne faut pas conclure automatiquement qu'elles suffisent à elles seules à rendre l'environnement conforme. Ce point doit être validé avec l'équipe pédagogique.

Les solutions présentes doivent être opérationnelles et leur code source accessible dans un environnement de développement opérationnel au moment de l'épreuve.

## Checklist avant E6

- faire remplir/valider cette correspondance par l'équipe pédagogique ;
- identifier précisément les deux serveurs et leurs OS ;
- identifier les outils sécurité réellement disponibles ;
- vérifier l'accès au dépôt et au code sans dépendre d'un service de communication avec un tiers ;
- disposer de R1 et R2 opérationnelles ;
- vérifier poste fixe + terminal mobile ;
- tester l'accès aux données distantes ;
- exécuter une sauvegarde et une restauration PostgreSQL ;
- tester l'environnement complet dans les conditions du centre.
