# Spécifications des interfaces R1 et R2

## Principes ergonomiques communs

- navigation stable et prévisible ;
- intitulés en français compréhensibles ;
- action principale clairement identifiable ;
- confirmation avant une suppression importante ;
- message explicite après succès ou erreur ;
- formulaires avec libellés visibles et validation proche du champ ;
- ne pas afficher une fonction interdite et toujours faire vérifier l'autorisation par l'API ;
- limiter la quantité d'informations affichée à ce qui est utile au rôle.

## R1 — WPF

### Écran Connexion
Champs : identifiant, mot de passe. Actions : connexion, affichage d'erreur. Aucun mot de passe ne doit être journalisé ou affiché en clair après saisie.

### Tableau de bord
Navigation vers Élèves, Professeurs, Classes, Matières, Scolarité et autres fonctions autorisées. Les éléments visibles dépendent du rôle.

### Élèves
Liste avec recherche simple ; action d'ajout ; sélection d'une fiche ; formulaire prénom, nom, date de naissance et informations de compte nécessaires. Accès aux inscriptions selon le périmètre.

### Professeurs
Liste/recherche, création/modification et consultation des affectations.

### Classes et matières
Écrans simples de liste + formulaire. Une classe affiche son année scolaire. Les affectations permettent de choisir professeur, classe et matière.

### Évaluations et notes
Sélection de l'affectation/classe, création d'une évaluation, puis tableau des élèves permettant la saisie des notes. Le barème doit être visible lors de la saisie.

### Absences/retards
Sélection d'une séance ou d'un élève selon le parcours retenu ; choix Absence/Retard ; date/heure ; motif facultatif.

## R2 — Web responsive

### Connexion
Formulaire centré et utilisable sur écran mobile. Identifiant, mot de passe, message d'erreur et état de chargement.

### Navigation
Desktop : navigation persistante si l'espace le permet. Mobile : navigation compacte. Seules les fonctions autorisées sont proposées.

### Tableau de bord
Résumé adapté au rôle : prochaines informations utiles, notes/assiduité et accès rapides. Ne pas surcharger la première version.

### Notes
Liste lisible par matière/évaluation avec note et barème. Le parent choisit l'enfant lorsqu'il en suit plusieurs.

### Absences et retards
Historique chronologique avec type, date et justification/motif si le périmètre final le prévoit.

### Emploi du temps
Vue adaptée à la largeur disponible ; informations essentielles : date/heure, matière, professeur/classe selon le rôle et salle lorsqu'elle existe.

### Documents
Liste avec titre/type/contexte et action de téléchargement autorisée.

### Messagerie
Liste des conversations autorisées, historique et zone d'envoi. Aucune création de conversation privée élève↔élève.

## Maquettes

Ce document fixe les contenus et comportements attendus, mais ne remplace pas les maquettes visuelles. Les maquettes finales doivent être créées ou capturées au fur et à mesure du développement et conservées comme preuve de conception/ergonomie.
