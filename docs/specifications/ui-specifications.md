# Spécifications des interfaces R1 et R2

## Principes ergonomiques communs

- identité EduGest bleu/cyan avec thème clair et sombre ;
- navigation stable et prévisible ;
- intitulés français compréhensibles ;
- petites icônes fonctionnelles, sans surcharger les écrans ;
- information prioritaire visuellement dominante ;
- action principale clairement identifiable ;
- confirmation avant suppression importante ;
- messages explicites après succès/erreur ;
- validation proche du champ ;
- fonctions interdites masquées côté interface et toujours refusées côté API ;
- quantité d'informations limitée à ce qui est utile au rôle.

## Connexion commune

La connexion ne demande **jamais de choisir un rôle**. L'utilisateur fournit ses identifiants ; après authentification, ses rôles/permissions déterminent l'espace et les fonctions accessibles.

Le formulaire reste simple : identifiant/e-mail, mot de passe, bouton de connexion, erreur et chargement. Affichage/masquage du mot de passe possible. « Se souvenir de moi » et récupération de mot de passe ne seront conservés que si leur comportement est réellement implémenté.

## R1 — WPF

### Tableau de bord
Navigation adaptée au compte connecté. Pour le professeur, priorité au planning, prochain cours/appel et actions à traiter. Pour l'administration/administrateur, priorité aux informations de gestion utiles.

### Élèves et professeurs
Listes avec recherche, fiches et opérations autorisées. Les affectations professeur relient professeur, classe et matière.

### Classes, matières & affectations
Listes/formulaires simples. Une classe affiche son année scolaire. L'administration utilise le libellé « Matières & affectations » plutôt que « Notes » pour cette gestion structurelle.

### Évaluations et notes
Le professeur sélectionne son affectation/classe et une évaluation puis saisit les notes. Barème visible. L'administration n'est pas présentée comme l'acteur principal de saisie des notes.

### Absences/retards
Le professeur peut effectuer l'appel dans le périmètre de ses séances/classes. Type, date/heure et informations complémentaires selon modèle final.

## R2 — Web responsive

### Élève
Dashboard centré sur planning, prochain cours et informations personnelles utiles. Accès à ses notes, absences, documents, planning et messagerie autorisée.

### Parent
Consultation des enfants liés. Si plusieurs enfants : sélection de l'enfant. Accès lecture aux notes, absences, documents et planning. Pas de messagerie parent en V1.

### Professeur
Si les fonctions professeur sont exposées en R2, elles reprennent les mêmes restrictions de périmètre que R1 et une interface adaptée au navigateur.

### Notes
Liste par matière/évaluation avec note et barème. Le parent consulte uniquement l'enfant sélectionné et lié à son compte.

### Emploi du temps
Vue adaptée à la largeur disponible : date/heure, matière, professeur ou classe selon rôle, salle si disponible.

### Documents
Liste avec titre/type/contexte et téléchargement autorisé.

### Messagerie
Élève ↔ professeur et diffusion de groupe selon droits. Pas de conversation privée élève↔élève. Pas de messagerie parent en V1.

## Référence visuelle

Les décisions visuelles validées sont consignées dans `docs/design/mockups.md`. Les images servent de référence ergonomique ; l'application finale et ses captures constitueront la preuve du résultat réellement implémenté.
