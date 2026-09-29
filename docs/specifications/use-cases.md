# Cas d'utilisation et règles métier

## Authentification

**UC-01 — Se connecter**  
Acteurs : tous les utilisateurs.  
Précondition : compte actif existant.  
Résultat : accès aux fonctions correspondant aux droits du compte.

**UC-02 — Se déconnecter**  
Résultat : la session côté client est terminée et le mécanisme de renouvellement n'est plus utilisable selon l'implémentation retenue.

## Administration scolaire

**UC-03 — Gérer les élèves**  
Acteur principal : Administration/Administrateur.  
Actions : créer, consulter, modifier ; suppression seulement lorsque les dépendances métier le permettent.

**UC-04 — Gérer les professeurs**  
Même principe que pour les élèves, avec association à un compte professeur.

**UC-05 — Gérer années, classes et matières**  
Les doublons incompatibles avec le modèle sont refusés. Une classe appartient à une année scolaire.

**UC-06 — Inscrire un élève dans une classe**  
L'élève et la classe doivent exister. Le même lien élève/classe ne doit pas être créé deux fois.

**UC-07 — Affecter un professeur**  
Une affectation relie professeur, classe et matière. Les trois ressources doivent exister.

## Notes

**UC-08 — Créer une évaluation**  
L'évaluation appartient à une affectation d'enseignement et possède un titre, une date et un barème strictement positif.

**UC-09 — Saisir une note**  
L'élève doit appartenir à la classe de l'évaluation. La note est comprise entre 0 et le barème. Un élève ne reçoit qu'une note pour une même évaluation.

**UC-10 — Consulter les notes**  
L'accès dépend du rôle et du périmètre : données propres de l'élève, enfants rattachés pour le parent, classes autorisées pour le professeur.

## Assiduité

**UC-11 — Planifier une séance**  
La séance référence une affectation ; sa date/heure de fin est postérieure au début.

**UC-12 — Déclarer une absence ou un retard**  
Le type appartient aux valeurs autorisées. Si une séance est indiquée, l'élève doit appartenir à sa classe.

## Documents

**UC-13 — Publier/consulter un document**  
Le fichier est associé à des métadonnées et à un périmètre autorisé. Taille, type et accès doivent être contrôlés avant stockage/téléchargement.

## Messagerie

**UC-14 — Échanger élève/professeur**  
Un élève peut échanger avec un professeur dans le périmètre autorisé.

**UC-15 — Diffuser à une classe/groupe**  
La diffusion de groupe est permise selon les droits. La conversation privée élève↔élève est exclue.

SignalR, s'il est utilisé, assure le temps réel ; les messages restent persistés et l'historique passe par l'API.

## Règles transversales

- l'API constitue la frontière de sécurité ;
- masquer un bouton dans l'interface ne remplace jamais l'autorisation serveur ;
- les identifiants reçus du client ne sont jamais considérés comme une preuve d'autorisation ;
- les données invalides sont refusées avant persistance ;
- les erreurs techniques ne doivent pas exposer de secrets ;
- les évolutions des règles métier doivent entraîner la mise à jour des tests et de la documentation.
