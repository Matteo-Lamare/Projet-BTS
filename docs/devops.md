# DevOps et déploiement

## Git

GitHub est la source de vérité. main représente la version stable. Les fonctionnalités sont développées dans feature/*. Les commits utilisent les préfixes feat, fix, test, docs, refactor et chore.

## CI

GitHub Actions doit restaurer les dépendances, compiler, exécuter les tests unitaires et les tests d'intégration lorsque l'environnement requis est disponible.

## CD

Cible : serveur Ubuntu. Architecture : Internet → Nginx HTTPS → ASP.NET Core API → PostgreSQL. Docker Compose orchestre les services et volumes persistants.

## Environnements

DEV pour le développement local, TEST pour la validation et PROD pour la livraison/démonstration. Les configurations sont séparées.

## Secrets

Aucun mot de passe, secret JWT, chaîne de connexion ou clé privée ne doit être commité. User Secrets en DEV, secrets GitHub en CI/CD et secrets d'environnement en PROD.

## HTTPS

Nginx termine TLS et reverse-proxy vers l'API. Les clés privées restent hors dépôt.

## PostgreSQL

Volume persistant, sauvegardes planifiées, stockage externe, rétention et test documenté de restauration.

## Logs

Logs techniques pour API, authentification, erreurs et reverse proxy. Audit métier séparé en base.

## Fichiers

Volume dédié et contrôles API sur autorisation, taille, extension, MIME et nom physique généré.

## Hors périmètre

Pas de Kubernetes, microservices, ELK, Kafka, Redis ou infrastructure cloud complexe sans besoin démontré.