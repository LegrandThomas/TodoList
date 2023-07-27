# TodoList

![todolist-to](https://github.com/LegrandThomas/TodoList/assets/103045194/9564e11a-321b-4e6c-93f0-1239d773351b)

---

## Table des matières

1. [Le Projet](#le-projet)
2. [Contexte du projet](#contexte-du-projet)
3. [Equipe pour ce brief](#equipe-pour-ce-brief)
4. [use case](#use-case)
5. [Diagramme de classe](#diagramme-de-classe)
6. [régles de cardinalités](#régles-de-cardinalités)
7. [régles métiers et contraintes divers](#régles-métiers-et-contraintes-divers)
9. [Controller/services/data](#Controllerservicesdata)
10. [requetages bdd / postman](#requetages-bdd--postman)
11. [IOC/DI](#iocdi)
12. [Commentaires divers](#Commentaires-divers)
12. [Installation/Mise en route](#installationmise-en-route)





## Le Projet:

* Réalisation de la partie backEnd d'une application de gestion de tâches un "To Do List"

 ### Contexte du projet:
  
  <details>
      <summary>contexte</summary>
      Vous allez concevoir les classes et les liens pour une application de gestion de tâches, également appelée ToDoList. L'objectif de l'application est de permettre aux utilisateurs d'ajouter, afficher, modifier et supprimer des tâches à réaliser et d'indiquer le status de la tâche ( à faire, en cours, terminée).

​
Fonctionnalités requises :

    Ajouter une tâche : L'utilisateur doit pouvoir ajouter une nouvelle tâche à la ToDoList. Chaque tâche aura un titre, une description, une date de création, une date d'échéance et un statut initial (à faire).
    Afficher la liste des tâches : L'utilisateur doit pouvoir voir la liste complète des tâches qu'il a ajoutées à la ToDoList. La liste devrait afficher le titre, la date d'échéance de chaque tâche et le statut.
    Modifier une tâche : L'utilisateur doit pouvoir modifier le titre, la description, la date d'échéance et le statut d'une tâche existante dans la ToDoList.
    Supprimer une tâche : L'utilisateur doit pouvoir supprimer une tâche de la ToDoList s'il n'a plus besoin de la réaliser.
    Filtrage des tâches par statut : Permettre aux utilisateurs de filtrer les tâches par statut (à faire, en cours, terminée).


Fonctionnalités avancées (Optionnelle) :

Pour ceux qui ont bien avancé et finit les fonctionnalités de base, vous pouvez intégrer la gestion des utilisteurs du ToDoList avec un système de connexion à l'application.

Contraintes :

    Concevez les classes pour représenter les entités de votre application : Task (tâche) , User (utilisateur) et Statut.
    Chaque classe doit avoir des propriétés pour représenter les attributs de l'entité. Par exemple, la classe Task pourrait avoir les propriétés suivantes : Title (titre), Description (description), CreatedDate      (date de création) et DueDate (date d'échéance) - User pourrait avoir : FirstName (Prénom), Name (Nom) et Email (Adresse e-mail) - Statut : Value (Valeur) .
    Définissez les liens entre les classes lorsque cela est nécessaire. Par exemple, une tâche est associée à un utilisateur qui l'a créée. Vous pouvez donc créer une relation entre la classe Task , la classe         User et le statut de la tâche.
    Assurez-vous d'utiliser les principes de l'encapsulation, de l'abstraction et de l'héritage pour concevoir vos classes de manière cohérente et modulaire.

N'hésitez pas à utiliser des diagrammes de classes pour visualiser les liens entre vos classes et mieux comprendre la structure de votre application.

L'objectif de ce sujet est de vous familiariser avec les concepts de base de la conception des classes pour une application de gestion de tâches. Bonne conception !


  </details>
  
  
<details>

<summary>Gitflow</summary>

  </details>




   ### Equipe pour ce brief:

<details>
<summary>Team</summary>
 
![femme](https://github.com/LegrandThomas/TodoList/assets/103045194/904ebd31-2c43-459b-9fde-86fd1d12b274)                      Florence

![avatar-homme(2)](https://github.com/LegrandThomas/TodoList/assets/103045194/09641e12-6955-41e5-93af-75fd5a598f32)            JB

![avatar-homme(3)](https://github.com/LegrandThomas/TodoList/assets/103045194/96a8c1ae-94e5-4a14-8b5e-90deb1229a6f)            Monir

![avatar-homme(4)](https://github.com/LegrandThomas/TodoList/assets/103045194/4e0ba620-9066-4a3e-b6e5-990bc296057d)            Pascal

![avatar-homme(5)](https://github.com/LegrandThomas/TodoList/assets/103045194/8d51767b-a971-4209-9e00-430a62fe73d0)            Thomas
   
  </details>

   ### use case


<img width="603" alt="use_case" src="https://github.com/LegrandThomas/TodoList/assets/103045194/881ff5a2-fa49-42ba-a9ba-cb9fc0315a24">



### Diagramme de classe:

<img width="778" alt="diagramme_classe" src="https://github.com/LegrandThomas/TodoList/assets/103045194/b8782ea6-a88f-4db0-be22-073d7f17a5ea">

### régles de cardinalités

### régles métiers et contraintes divers

### Controller/services/data

### requetages bdd / postman


### IOC/DI:

### Commentaires divers:

### Installation/Mise en route:

