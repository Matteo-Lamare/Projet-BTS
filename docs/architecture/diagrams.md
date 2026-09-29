# Diagrammes de conception — EduGest

Ces diagrammes décrivent la cible actuelle. Ils devront évoluer si l'implémentation réellement retenue change.

## Architecture générale

```mermaid
flowchart LR
    WPF[R1 - Client WPF] -->|HTTPS / JSON| API[ASP.NET Core Web API]
    WEB[R2 - Navigateur Web] -->|HTTPS / JSON| API
    API --> APP[Application / logique]
    APP --> INFRA[Infrastructure]
    INFRA --> DB[(PostgreSQL)]
    API --> ID[Identity / JWT]
    API -. temps réel si réalisé .-> SIG[SignalR]
```

Principe : aucun client n'accède directement à PostgreSQL.

## Authentification

```mermaid
sequenceDiagram
    actor U as Utilisateur
    participant C as Client R1/R2
    participant A as API
    participant I as Identity
    participant D as PostgreSQL
    U->>C: saisit ses identifiants
    C->>A: POST /api/auth/login
    A->>I: vérifie le compte
    I->>D: lit les données nécessaires
    D-->>I: résultat
    I-->>A: identité/rôles
    A-->>C: jeton d'accès + mécanisme de renouvellement
    C->>A: requête protégée + jeton
    A-->>C: réponse selon autorisation
```

## Saisie d'une note — logique cible

```mermaid
sequenceDiagram
    actor P as Professeur
    participant C as Client
    participant A as API
    participant D as Base
    P->>C: saisit la note
    C->>A: demande d'enregistrement
    A->>A: vérifie authentification et permission
    A->>D: vérifie affectation, évaluation et inscription
    D-->>A: données métier
    A->>A: valide score et périmètre
    A->>D: enregistre
    D-->>A: succès
    A-->>C: confirmation
```

Le contrôle « professeur réellement affecté » est une règle cible à garantir dans l'implémentation finale ; le diagramme ne constitue pas une preuve qu'elle est déjà codée.

## Parent consultant un enfant

```mermaid
flowchart TD
    P[Parent authentifié] --> R{Enfant demandé rattaché au parent ?}
    R -- Non --> X[Refus]
    R -- Oui --> D{Permission de lecture ?}
    D -- Non --> X
    D -- Oui --> V[Données autorisées de l'enfant]
```

## Déploiement cible

```mermaid
flowchart LR
    FIXE[Poste fixe - R1] --> RP[Reverse proxy / HTTPS]
    MOB[Terminal / navigateur - R2] --> RP
    RP --> API[API ASP.NET Core]
    API --> PG[(PostgreSQL)]
    API --> VOL[(Volume fichiers - si Documents réalisé)]
```

Le nombre de serveurs, leurs OS et les outils de sécurité de l'environnement E6 restent à valider avec l'établissement.
