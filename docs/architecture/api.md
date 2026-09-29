# Contrat API

Base : /api. Les routes protégées nécessitent un JWT valide. Les permissions et le périmètre métier sont vérifiés côté serveur.

## Authentification

POST /api/auth/login — connexion.
POST /api/auth/refresh — renouvellement.
POST /api/auth/logout — déconnexion.
GET /api/auth/me — utilisateur courant.

## Référentiels

GET/POST /api/students et GET/PUT/DELETE /api/students/{id}.
GET/POST /api/teachers et GET/PUT/DELETE /api/teachers/{id}.
GET/POST /api/classes et GET/PUT/DELETE /api/classes/{id}.
GET/POST /api/subjects et GET/PUT/DELETE /api/subjects/{id}.
GET/POST /api/teaching-assignments.
GET /api/academic-years.

## Notes

GET/POST /api/assessments.
GET/PUT/DELETE /api/assessments/{id}.
GET /api/assessments/{id}/grades.
PUT /api/assessments/{id}/grades/{studentId}.
GET /api/students/{id}/grades.

## Assiduité

GET /api/attendance.
POST /api/attendance.
PUT /api/attendance/{id}.
DELETE /api/attendance/{id}.

## Documents

GET /api/documents.
POST /api/documents.
GET /api/documents/{id}.
GET /api/documents/{id}/download.
DELETE /api/documents/{id}.

## Emploi du temps

GET /api/sessions.
POST /api/sessions.
PUT/DELETE /api/sessions/{id}.

## Messagerie

GET /api/conversations.
POST /api/conversations.
GET /api/conversations/{id}/messages.
POST /api/conversations/{id}/messages.

Hub SignalR : /hubs/chat. Événements : MessageReceived, JoinConversation, LeaveConversation, MarkAsRead.

## Administration

GET /api/statistics.
GET /api/audit-logs.
GET/POST/PUT /api/users.
GET/PUT /api/roles/{id}.
GET/PUT /api/permissions/{id}.

## Réponses HTTP

200 lecture/modification, 201 création, 204 sans contenu, 400 requête invalide, 401 non authentifié, 403 non autorisé, 404 ressource absente/non accessible, 409 conflit, 422 règle métier refusée, 500 erreur interne sans détail sensible.

Les DTO détaillés seront figés avant l'implémentation des contrôleurs.