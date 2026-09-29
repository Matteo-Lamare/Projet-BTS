# Maquettes R1 / R2 — décisions validées

## Statut

La direction visuelle principale a été construite par itérations avec le candidat. Les maquettes servent de **référence de conception** et non de contrat pixel-perfect : les données fictives ou détails accidentels générés dans une image ne créent pas de nouvelle exigence fonctionnelle.

## Identité visuelle retenue

- logo EduGest : chapeau de diplômé ;
- palette principale : bleu, cyan/turquoise, avec accents violets ponctuels ;
- deux thèmes : clair et sombre ;
- interfaces aérées, cartes arrondies, bordures/ombres discrètes ;
- icônes petites et fonctionnelles ;
- hiérarchie visuelle : l'information prioritaire doit être plus visible que les blocs secondaires ;
- même langage graphique entre les différents rôles.

## Connexion commune

Une seule page de connexion est retenue.

Elle contient uniquement les éléments utiles à l'authentification : logo, identifiant/adresse e-mail, mot de passe, affichage/masquage du mot de passe, mémorisation si retenue, récupération de mot de passe si elle est effectivement implémentée, bouton de connexion et retour d'erreur.

**Aucun bouton « se connecter en tant que » et aucun sélecteur de rôle.** Après authentification, l'application détermine l'espace accessible à partir du compte, de ses rôles et de ses permissions.

Les blocs promotionnels/fonctionnels sous le formulaire ont été supprimés.

## Élève — R2

Navigation de référence :
- Tableau de bord ;
- Mon planning ;
- Mes notes ;
- Mes devoirs ;
- Mes documents ;
- Messages.

Dashboard : planning de la semaine prioritaire, prochain cours, devoirs/éléments à faire, dernières notes et messages récents autorisés.

Notes : lecture par matière et évaluation, avec barème.

Devoirs : présentation des travaux/échéances si cette fonctionnalité est effectivement reliée au modèle final. La maquette ne suffit pas à créer un module métier non spécifié.

Documents : liste filtrable et téléchargement des documents autorisés.

## Parent — R2

Le parent dispose d'un espace de consultation centré sur ses enfants liés.

Navigation de référence :
- Tableau de bord ;
- Mes enfants ;
- Planning ;
- Notes ;
- Absences ;
- Documents.

Lorsque plusieurs enfants sont liés au compte, un sélecteur permet de changer l'enfant consulté.

Le parent reste en lecture seule sur les données scolaires prévues. **Pas de messagerie parent dans la première version.**

## Professeur

Le dashboard professeur a été volontairement allégé.

Priorités :
1. planning ;
2. prochain cours et action d'appel ;
3. travail à faire (ex. notes à saisir / rendus à traiter selon fonctions réellement réalisées) ;
4. messages récents autorisés ;
5. activité concernant le professeur et ses classes.

L'activité ne doit pas afficher l'activité générale des autres professeurs.

Écrans de référence :
- Tableau de bord ;
- Mon planning ;
- Mes classes ;
- Saisie des notes ;
- Absences / appel ;
- Mes documents ;
- Messages.

Les anciens blocs « Mes matières » et « Prochaine séance » séparés ont été retirés du dashboard lorsqu'ils faisaient doublon avec le planning/prochain cours.

## Administrateur / administration

Écrans de référence :
- Tableau de bord ;
- Utilisateurs selon permission ;
- Classes ;
- Matières & affectations ;
- Emploi du temps ;
- Résultats en consultation selon permission ;
- Absences ;
- Documents ;
- fonctions système réservées selon permission.

Le libellé ambigu **« Notes » a été remplacé par « Matières & affectations »** pour la gestion de la structure pédagogique. Une affectation relie professeur, classe et matière. La saisie courante des notes appartient au professeur ; l'administration peut disposer d'une consultation des résultats selon ses droits.

## Thèmes

Les thèmes clair et sombre reprennent la même structure et les mêmes fonctions. Le changement de thème ne modifie jamais les permissions ni le contenu métier accessible.

## Responsive

R2 doit rester exploitable sur ordinateur et mobile. Les maquettes larges définissent la direction desktop ; les adaptations mobiles seront vérifiées pendant l'intégration plutôt que considérées comme déjà prouvées par les images.

## Évolution des maquettes

Une maquette peut encore évoluer pendant le développement si un test d'usage révèle un problème. Toute modification fonctionnelle significative doit rester cohérente avec les cas d'utilisation, permissions et spécifications UI.
