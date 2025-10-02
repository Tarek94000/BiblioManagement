
# BiblioManagement

BiblioManagement est un système de gestion de bibliothèque construit avec ASP.NET Core et Entity Framework Core. Ce projet permet de gérer les livres, les utilisateurs, et les tâches administratives au sein d'une bibliothèque.

## Fonctionnalités

- Ajouter, modifier et supprimer des livres.
- Gérer les utilisateurs et leurs emprunts.
- Consulter la liste des livres disponibles.
- Système d'authentification pour les utilisateurs et les administrateurs.

## Prérequis

Avant de pouvoir exécuter ce projet, assurez-vous que vous avez les éléments suivants installés sur votre machine :

- [Node.js](https://nodejs.org/) (si vous utilisez un frontend JavaScript)
- [.NET SDK](https://dotnet.microsoft.com/download) (pour exécuter les projets .NET)
- [SQL Server](https://www.microsoft.com/en-us/sql-server) ou une base de données compatible avec Entity Framework Core

## Installation

### 1. Cloner ce repository

```bash
git clone https://github.com/ton-utilisateur/BiblioManagement.git
cd BiblioManagement
```

### 2. Installer les dépendances

#### Backend (ASP.NET Core) :

- Ouvrez le projet avec Visual Studio ou VSCode.
- Exécutez la commande suivante pour restaurer les dépendances :

```bash
dotnet restore
```

- Ensuite, pour démarrer l'application, utilisez :

```bash
dotnet run
```

#### Frontend (si applicable) :

Si tu as un frontend en JavaScript, installe les dépendances avec :

```bash
npm install
```

Puis démarre le serveur avec :

```bash
npm start
```

### 3. Configurer la base de données

- Modifiez le fichier `appsettings.json` pour y inclure les informations de connexion à votre base de données.
- Exécutez les migrations Entity Framework pour créer les tables nécessaires dans la base de données :

```bash
dotnet ef database update
```

## Contribuer

1. Fork ce repository.
2. Créez une branche (`git checkout -b feature-xyz`).
3. Commitez vos modifications (`git commit -am 'Ajout de la fonctionnalité XYZ'`).
4. Poussez la branche (`git push origin feature-xyz`).
5. Ouvrez une pull request.

## Licence

Ce projet est sous la licence MIT - voir le fichier [LICENSE](LICENSE) pour plus de détails.
