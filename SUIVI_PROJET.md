# Suivi du projet

Ce document est le journal lisible du projet. Il explique les décisions, les changements réalisés, leur raison et leur niveau de vérification.

## Règle de mise à jour

Chaque étape importante est ajoutée ici avec :

- la date ;
- ce qui a été modifié ;
- pourquoi ce choix a été fait ;
- ce qui a été vérifié ;
- la prochaine action utile.

## 16 octobre 2026 — Migration des clients

### Décision

Le projet adopte les technologies suivantes :

- API et logique métier : C# avec ASP.NET Core 8 existant ;
- base de données : PostgreSQL avec Entity Framework Core ;
- application légère : React, TypeScript et Vite ;
- application lourde : Avalonia UI, C# et MVVM ;
- communication temps réel : SignalR, à intégrer lorsque la messagerie sera développée.

### Pourquoi

Le client léger doit être accessible dans un navigateur pour les élèves et les professeurs. React structure mieux l'interface, les formulaires et les appels à l'API qu'une page JavaScript isolée.

Le client lourd doit être installable pour l'administration. Avalonia permet de conserver C# et XAML tout en évitant de limiter le projet à Windows, contrairement à WPF.

### Modifications réalisées

- remplacement du projet desktop WPF par le socle Avalonia ;
- ajout d'une fenêtre de démarrage Avalonia et d'un premier ViewModel MVVM ;
- remplacement du socle web JavaScript vanilla par une application React avec TypeScript et Vite ;
- ajout de React Router et TanStack Query pour préparer la navigation et les futurs appels API ;
- création de ce journal de suivi ;
- conservation de l'API et de la base de données existantes : elles ne sont pas modifiées par cette migration.

### Vérification

- les fichiers de configuration des deux clients ont été vérifiés de manière statique ;
- les clients n'ont pas encore été compilés ou exécutés : l'installation des dépendances et le premier lancement restent à faire ;
- aucune fonction métier existante n'a été supprimée, car les deux clients ne contenaient encore aucun écran métier.

### Prochaine étape

Installer les dépendances, vérifier que l'API, le client React et le client Avalonia démarrent, puis mettre en place l'authentification de bout en bout.

### Note technique

Le projet desktop inclut aussi les polices et les diagnostics Avalonia nécessaires au démarrage prévu par le modèle Avalonia.
