# 🎮 **PokemonMonster**  
**Un projet WPF en C# inspiré du système de combat de Pokémon**

## 📜 **Description du Projet**  
**PokemonMonster** est une application développée en **C#** avec la technologie **WPF** (Windows Presentation Foundation). L'objectif est de recréer, dans une version simplifiée, le **système de combat Pokémon**, avec quelques fonctionnalités clés pour rendre l'expérience interactive et agréable.  

---

### 🚪 **Fonctionnalités Principales**  

1. **Authentification**  
   - Le projet démarre sur une **page de connexion** permettant de s'authentifier ou de s'inscrire.  

2. **Catalogue de Pokémon**  
   - Accédez à une liste des **151 premiers Pokémon**.  
   - Chaque Pokémon est jouable !  
   - Découvrez leurs statistiques et les sorts associés.  

3. **Catalogue de Sorts**  
   - Consultez la liste des **attaques (ou sorts)** disponibles.  
   - Visualisez les détails de chaque sort ainsi que les Pokémon associés.  

4. **Système de Combat**  
   - Choisissez un Pokémon en cliquant sur **"Jouer"**.  
   - Affrontez des adversaires sélectionnés **aléatoirement**.  
   - **Progression dynamique** : Après trois victoires, les adversaires obtiennent un **bonus de dégâts ou de points de vie**, rendant le jeu plus challengeant.  

5. **Navigation Simple et Intuitive**  
   - Une **barre de navigation** en haut de l'écran vous permet de basculer facilement entre les pages :  
     - Liste des Pokémon  
     - Liste des sorts  
     - Page de combat  

---

### 🧩 **Architecture et Modèle MVVM**  
Le projet suit le modèle **MVVM (Model-View-ViewModel)** dans la majorité des cas. Cependant, certaines exceptions, telles que la **logique de navigation**, sont directement intégrées dans les vues pour des raisons pratiques.  

---

## 🚀 **Installation et Configuration**  

### **Prérequis**  
- **Git** (pour cloner le repository)  
- **C#**  
- **SQL Server** (pour la base de données)  
- Un **IDE compatible avec C#**, tel que **Visual Studio**  

---

### **Étapes d'Installation**  

1. **Créer la Base de Données**  
   - Créez une base de données locale nommée **`ExerciceMonster`**.  
   - Les **tables et données** seront automatiquement créées au lancement du projet, il n'est donc pas nécessaire de la remplir manuellement.  

2. **Cloner le Repository**  
   - Clonez le projet sur votre machine via un terminal :  
     ```bash
     git clone https://github.com/fl-hugo/PokemonMonster
     ```

3. **Configurer la Connexion à la Base de Données**  
   - Ouvrez le projet dans **Visual Studio** ou un IDE équivalent.  
   - Modifiez le fichier `RepositoryBase.cs` situé dans le dossier **Repositories**.  
   - Ajustez la valeur de **`_connectionString`** pour correspondre à votre base de données locale. Exemple pour **SQLEXPRESS** :  
     ```csharp
     _connectionString = "Server=localhost\\SQLEXPRESS; Database=ExerciceMonster; Trusted_Connection=True; TrustServerCertificate=True;";
     ```

4. **Lancer le Projet**  
   - Compilez et lancez le projet depuis votre IDE.  
   - Patientez jusqu'à ce que la **page d'accueil** apparaisse. Vous êtes prêt à jouer ! 🎉  

---

### **Note**  
J'ai tenté d'implémenter une page de configuration dynamique (`DatabaseConnectionView.xaml`) pour faciliter la connexion à votre base de données sans modification du code source. Cependant, cette fonctionnalité n'est pas encore opérationnelle dans cette version du projet.  

---

## 🛠️ **Technologies Utilisées**  
- **Langage** : C#  
- **Framework** : WPF  
- **Base de Données** : SQL Server  
- **Architecture** : MVVM  

---

## 🎯 **Améliorations Futures**  
- Finaliser la configuration dynamique de la connexion à la base de données.  
- Intégrer un système de sauvegarde de progression.  
- Optimiser l'interface utilisateur pour une meilleure expérience.
- Ajouter de la complexité au système de combat (effets de sorts comme brûlure, types de sorts et de pokemon, statistiques de monstres)
- Améliorer le design de l'application, notamment en y ajoutant de la fluidité et des animations

---

## 🧑‍💻 **Auteur**  
Développé par **Flandrin Hugo** dans le cadre d'un projet d'apprentissage à **Ynov Lyon**.  

---

### 🚀 **Bon Jeu !**  
