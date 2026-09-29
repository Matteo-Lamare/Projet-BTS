# Analyse des écarts — EduGest vs documents rectorat E6 2026

## Synthèse

### Points déjà bien cadrés
- deux réalisations distinctes R1 WPF et R2 Web ;
- API/service Web partagé ;
- PostgreSQL/EF Core ;
- authentification et habilitations ;
- Git/GitHub et CI ;
- tests d'intégration déjà présents dans le socle ;
- MCD/MLD et documentation d'architecture ;
- séparation client/API/base de données.

### Écarts prioritaires

**P0 — conformité de l'environnement du centre**
- deux serveurs sur OS différents dont un open source non identifiés ;
- outils de gestion d'incidents, intrusion, chiffrement et analyse de trafic non identifiés ;
- exigence de code exécuté sur une solution mobile à clarifier ;
- deux solutions applicatives opérationnelles du contexte à valider avec l'établissement.

**P0 — deux réalisations opérationnelles**
R1 WPF et R2 Web ne doivent pas rester des descriptions/squelettes. Les deux réalisations sont nécessaires à l'épreuve.

**P0 — maintenance corrective/évolutive**
Il faut conserver une vraie évolution ou correction : besoin/version initiale, diagnostic ou analyse, code modifié, tests, non-régression et documentation mise à jour.

**P0 — sauvegarde/restauration**
La stratégie écrite ne suffit pas. Il faut une sauvegarde réellement exécutée et au moins un test de restauration documenté.

**P1 — contexte juridique**
Le cahier des charges doit expliciter les données personnelles scolaires, les droits d'accès et les principes de minimisation/conservation réellement retenus. Ne pas inventer de conformité juridique sans analyse.

**P1 — ergonomie et maquettes**
La grille mentionne les maquettes et contraintes ergonomiques. Il faut documenter les principaux écrans R1/R2 avant ou pendant leur développement.

**P1 — documentation utilisateur**
Prévoir au minimum un guide court R1 et un guide R2, mis à jour avec les versions finales.

**P1 — documentation des versions**
Git existe, mais il faut rendre lisibles les versions/évolutions : changelog ou journal de versions relié aux commits/releases.

**P1 — rapport de tests**
Les tests automatisés doivent être accompagnés d'un rapport compréhensible : version, environnement, scénario, attendu, obtenu, statut.

## Incohérences internes corrigées ou à surveiller

- ne plus écrire « développement à venir » alors qu'un socle existe ;
- ne pas présenter les routes prévues comme déjà implémentées ;
- ne pas présenter le socle/template backend comme une réalisation personnelle terminée ;
- la cible DTO séparée des entités doit rester une cible tant que le code n'est pas refactoré ;
- Tailwind concerne uniquement le CSS du client R2 ;
- responsive Web ne doit pas être présenté sans validation comme équivalent à l'exigence de code exécuté sur l'OS d'une solution mobile.

## Ordre recommandé avant développement intensif

1. finaliser cahier des charges et contexte juridique ;
2. préparer cas d'utilisation/règles métier et maquettes R1/R2 ;
3. faire valider l'environnement technologique avec l'établissement ;
4. préparer les deux fiches descriptives comme documents vivants ;
5. définir les preuves à conserver pendant que le candidat code ;
6. seulement ensuite reprendre le développement fonctionnel.
