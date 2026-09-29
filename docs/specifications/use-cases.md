# Cas d'utilisation et règles métier

## Authentification

**UC-01 — Se connecter**  
Acteurs : tous les utilisateurs.  
Précondition : compte actif existant.  
Déroulement : l'utilisateur fournit ses identifiants sur une page commune. Aucun rôle n'est choisi manuellement. Après authentification, les rôles/permissions du compte déterminent les fonctions accessibles.  
Résultat : accès à l'espace autorisé.

**UC-02 — Se déconnecter**  
Résultat : la session côté client est terminée et le mécanisme de renouvellement n'est plus utilisable selon l'implémentation retenue.

## Administration scolaire

**UC-03 — Gérer les élèves**  
Acteur principal : Administration/Administrateur. Création, consultation, modification ; suppression seulement lorsque les dépendances le permettent.

**UC-04 — Gérer les professeurs**  
Même principe, avec association à un compte professeur.

**UC-05 — Gérer années, classes et matières**  
Les doublons incompatibles avec le modèle sont refusés. Une classe appartient à une année scolaire.

**UC-06 — Inscrire un élève dans une classe**  
Élève et classe existants ; association non dupliquée.

**UC-07 — Affecter un professeur**  
Une affectation relie professeur, classe et matière. Les trois ressources existent.

## Notes

**UC-08 — Créer une évaluation**  
L'évaluation appartient à une affectation et possède titre, date et barème positif.

**UC-09 — Saisir une note**  
Acteur métier principal : professeur autorisé sur l'affectation. L'élève appartient à la classe ; score entre 0 et barème ; une note par élève/évaluation.

**UC-10 — Consulter les notes**  
Selon rôle/périmètre : élève lui-même, enfant lié pour parent, classes affectées pour professeur, consultation administrative selon permissions.

## Assiduité

**UC-11 — Planifier une séance**  
Séance liée à une affectation ; fin postérieure au début.

**UC-12 — Faire l'appel / déclarer absence ou retard**  
Le professeur agit dans son périmètre autorisé. Si une séance est indiquée, l'élève appartient à sa classe.

## Documents

**UC-13 — Publier/consulter un document**  
Métadonnées et périmètre autorisé. Taille, type et accès contrôlés avant stockage/téléchargement.

## Messagerie

**UC-14 — Échanger élève/professeur**  
Échange dans le périmètre autorisé.

**UC-15 — Diffuser à une classe/groupe**  
Selon droits. Conversation privée élève↔élève exclue. Parent exclu de la messagerie en V1.

SignalR, s'il est utilisé, assure le temps réel ; l'historique reste persistant et accessible via l'API.

## Règles transversales

- API = frontière de sécurité ;
- masquer un bouton ne remplace pas l'autorisation serveur ;
- identifiant fourni par le client ≠ preuve d'autorisation ;
- rôle non choisi par l'utilisateur à la connexion ;
- données invalides refusées avant persistance ;
- erreurs techniques sans secrets ;
- changement métier => tests et documentation mis à jour.
