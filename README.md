# stage_final
analyse de sentiment
Description du Projet
1.	Conception UI/UX
 Frontend : Développé en ReactJS, favorisant une interface utilisateur réactive et dynamique.
2.	Architecture et Structure Backend
Le projet intègre deux serveurs backend distincts, avec une architecture modulaire et évolutive :
Backend A (ASP.NET Core):
Microservices : 
•	Service Utilisateur : Gère l'authentification et les opérations CRUD (Create, Read, Update, Delete) sur les utilisateurs.

•	Service de Publication : Gère la publication des avis des utilisateurs ainsi que les opérations CRUD associées.
 Architecture N-Layers : Comprend les level API, Business Logic, et Data Access, tout en respectant les principes SOLID. Cette approche assure la réutilisabilité, la modularité et l'évolutivité du backend. Les méthodes sont asynchrones pour une meilleure performance.

Backend B (Flask) :
- Utilise le Framework Flask en Python pour gérer les routes d'intelligence artificielle (IA).
Gateway API (ASP.NET Core):
Sert de passerelle pour rediriger les requêtes venant du frontend vers le backend approprié, tout en sécurisant les routes.

3.	Fonctionnalités Clés
-Analyse de Sentiment : Utilise le traitement du langage naturel (NLP) avec l'apprentissage profond et les réseaux de neurones convolutifs (CNN) pour l'analyse de sentiment.
- Chatbot : Intégration d'un chatbot utilisant des modèles de langage à grande échelle (LLM).
- API Google : Bien que plusieurs APIs soient disponibles pour l'analyse de sentiment (Azure, Amazon, Google), ce projet utilise spécifiquement l'API Google.
4.	Utilisation de l'Application
Les utilisateurs peuvent se connecter ou créer un compte, tester les APIs, et laisser des commentaires.

5.	Technologies Utilisées
ReactJS, NLP, LLM, API Google, C# (ASP.NET Core), Python (Flask), IA (Deep Learning, CNN etc.)

6.	Explication : Plateforme d'Analyse de Sentiment
Une plateforme d'analyse de sentiment est un système informatique capable d'identifier, d'extraire, de quantifier et d'étudier les états affectifs et les informations subjectives à partir de sources textuelles. Elle utilise des techniques de traitement du langage naturel (NLP), d'analyse textuelle et d'apprentissage automatique pour interpréter l'émotion exprimée dans les données textuelles - par exemple, déterminer si un commentaire est positif, négatif. Ces plateformes sont largement utilisées pour surveiller et analyser les avis et les réactions des consommateurs sur les réseaux sociaux, les forums, les sites d'avis, etc., permettant aux entreprises d'obtenir des insights précieux sur le sentiment du public à l'égard de leurs produits ou services.
