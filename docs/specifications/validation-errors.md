# Validations et gestion des erreurs

## Validations générales
Champs obligatoires, longueurs, références, dates, doublons et périmètres sont vérifiés côté serveur avant persistance. Une valeur reçue du client n'est jamais une preuve d'autorisation.

## Règles retenues
- année scolaire : début avant fin ;
- classe : année existante, nom unique dans l'année ;
- matière : code unique ;
- inscription : élève/classe existants, lien non dupliqué ;
- affectation : professeur/classe/matière existants, triplet non dupliqué ;
- évaluation : affectation existante et barème positif ;
- note : élève inscrit, score entre 0 et barème, une note par évaluation/élève ;
- séance : affectation existante, salle valide si fournie, début avant fin ;
- assiduité : type ABSENCE ou LATE ; élève inscrit dans la classe si une séance est fournie.

## Convention HTTP
200 lecture/modification ; 201 création ; 204 succès sans contenu ; 400 saisie invalide ; 401 non authentifié ; 403 interdit ; 404 absent/non accessible ; 409 conflit ; 500 erreur interne sans détail sensible.

Cette convention reste à confronter endpoint par endpoint à l'implémentation finale.

## Interface
R1/R2 traduisent les erreurs en messages compréhensibles sans afficher stack trace, requête SQL, secret ou détail interne.
