# Stratégie de sécurité EduGest

## Authentification
Socle : ASP.NET Core Identity, JWT et refresh token. Mots de passe gérés par Identity. Secrets et chaînes réelles hors Git.

## Autorisation
Deux contrôles sont nécessaires : permission fonctionnelle puis périmètre métier. Exemple : posséder grades.write ne suffit pas ; un professeur doit être affecté à la classe concernée. Ce contrôle de périmètre reste à garantir partout lors du développement.

## Communications
Cible : HTTPS via Nginx. Il ne sera déclaré opérationnel qu'après déploiement et vérification.

## Données et fichiers
PostgreSQL n'est pas directement accessible aux clients. Migrations versionnées. Les futurs fichiers seront contrôlés en autorisation, taille et type avant stockage/accès.

## Logs
Séparer logs techniques et audit métier. Ne pas journaliser mots de passe, jetons complets ou données sensibles inutiles.

## Tests de sécurité fonctionnelle
Tester : accès anonyme, permission insuffisante, données d'un autre utilisateur/enfant/classe, entrées invalides, token expiré/révoqué et absence de détails sensibles dans les erreurs.

## Environnement E6
Les outils d'incident, intrusion, chiffrement et analyse de trafic demandés par le rectorat relèvent aussi de l'environnement du centre et doivent être identifiés avec l'établissement.
