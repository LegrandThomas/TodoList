# TodoList

## Table des matières

1. [Le Projet](#Le-Projet)
2. [Contexte du projet](#Contexte-du-projet)
3. [acteurs et fonctionnalités](#acteurs-et-fonctionnalités)
4. [use case](#use-case)
5. [MCD / MLD](#mcd--mld)
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


#### Les branches



   ### acteurs et fonctionnalités:


   ### use case

<details>
   <summary>use case  </summary>



</details>



### MCD / MLD:



### régles de cardinalités

### régles métiers et contraintes divers

### Controller/services/data

### requetages bdd / postman


### IOC/DI:

### Commentaires divers:

### Installation/Mise en route:

