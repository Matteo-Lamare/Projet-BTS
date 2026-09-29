# Contrat API

Base : `/api`. Les routes protégées nécessitent un JWT valide. Les permissions et le périmètre métier sont vérifiés côté serveur.

Ce document distingue les routes **déjà présentes dans le socle** des routes **prévues**. Les contrats pourront évoluer lorsque le candidat développera les interfaces R1/R2.

## Authentification — socle présent

- POST `/api/auth/login`
- POST `/api/auth/refresh`
- POST `/api/auth/logout`
- GET `/api/auth/me`

## Référentiels — socle présent

- GET/POST `/api/academic-years` ; GET/PUT/DELETE `/api/academic-years/{id}`
- GET/POST `/api/classes` ; GET/PUT/DELETE `/api/classes/{id}`
- GET/POST `/api/subjects` ; GET/PUT/DELETE `/api/subjects/{id}`
- GET/POST `/api/students` ; GET/PUT/DELETE `/api/students/{id}`
- GET/POST `/api/teachers` ; GET/PUT/DELETE `/api/teachers/{id}`
- GET/POST `/api/enrollments` ; DELETE `/api/enrollments/{id}`
- GET/POST `/api/teaching-assignments` ; DELETE `/api/teaching-assignments/{id}`

## Évaluations et notes — socle présent

- GET/POST `/api/assessments`
- GET/PUT/DELETE `/api/assessments/{id}`
- GET/POST `/api/grades`
- GET/PUT/DELETE `/api/grades/{id}`

## Séances et assiduité — socle présent

- GET/POST `/api/sessions`
- GET/PUT/DELETE `/api/sessions/{id}`
- GET/POST `/api/attendance`
- GET/PUT/DELETE `/api/attendance/{id}`

## Routes prévues, non considérées comme réalisées

Documents, statistiques, audit, gestion avancée des utilisateurs/permissions et messagerie/SignalR seront documentés précisément lorsqu'ils seront effectivement développés.

## Codes HTTP

Le socle utilise notamment : 200 lecture, 201 création, 204 succès sans contenu, 400 données invalides, 401 non authentifié, 403 non autorisé, 404 ressource absente et 409 conflit.

## Règle pour la suite

Les DTO et routes définitifs doivent rester compréhensibles par le candidat. Toute modification du contrat API est documentée avec la fonctionnalité correspondante.
