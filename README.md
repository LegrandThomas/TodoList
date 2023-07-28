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
10. [requetages bdd / postman](#requetages-bdd--postman--swagger)
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


<img width="392" alt="use_case_new" src="https://github.com/LegrandThomas/TodoList/assets/103045194/a0433656-b141-4608-93ae-232eb2a3b4ed">



### Diagramme de classe:

<img width="778" alt="diagramme_classe" src="https://github.com/LegrandThomas/TodoList/assets/103045194/b8782ea6-a88f-4db0-be22-073d7f17a5ea">

### régles de cardinalités

- un user posséde **0 à plusieurs** tâches
- une tâche appartient à **1 et 1** seul user

- un status est attribué à **0 à plusieurs** tâches
- une tâche a **1 et 1** seul status

### régles métiers et contraintes divers

-si le status d'une tâche est 'terminée' et/ou si il passe "à terminée", la tâche doit avoir une date de résolution.
-pour une tâche, la date de résolution ne peut pas être antérieure à la date de création

### Controller/services/data

### requetages bdd / postman / swagger

 ===> lien githubpage pour carrousel d'images <===

https://legrandthomas.github.io/carrousel/

<details>

<summary>Collection Postman</summary>

 ![collection_postman](https://github.com/LegrandThomas/TodoList/assets/103045194/419e54f5-b016-4165-9a68-db70cbe95d8b)
 
</details>

<details>
<summary>User</summary>

![postman_GetAllUsers](https://github.com/LegrandThomas/TodoList/assets/103045194/ea8eb0b5-6787-46b5-95cd-8741fd3795ff)

![postman_CreateUser](https://github.com/LegrandThomas/TodoList/assets/103045194/02cbae84-f565-416b-9160-fc7e6820e716)

![postman_deleteUserById](https://github.com/LegrandThomas/TodoList/assets/103045194/88e87b5e-f7f0-475c-b4f0-bbd20e7553a2)

![postman_GETUserById](https://github.com/LegrandThomas/TodoList/assets/103045194/a6041543-f2c0-4c11-8470-1e40062b53e1)

</details>

<details>
<summary>Status</summary>

![postman_GetallStatus](https://github.com/LegrandThomas/TodoList/assets/103045194/226b0a61-36bf-43c1-a573-606bf40410a6)

![postman_getStatusById](https://github.com/LegrandThomas/TodoList/assets/103045194/d6336617-c39b-4ac1-af3a-82d49b1b9cf4)

</details>


<details>
<summary>Tasks</summary>

![postman_GetAllTasks](https://github.com/LegrandThomas/TodoList/assets/103045194/c8fb835d-7474-4c49-842f-4faea96e4dae)

![postman_createTask](https://github.com/LegrandThomas/TodoList/assets/103045194/a0aa94ec-579f-4e1a-9e2f-cededd62a08a)

![postman_deteleTaskById](https://github.com/LegrandThomas/TodoList/assets/103045194/25330d35-be1d-4545-84cb-36dd19913cd9)

![postman_GetTaksByUserId](https://github.com/LegrandThomas/TodoList/assets/103045194/4c2beb4f-1eb3-4466-9b88-c173b4e1faf8)

![postman_getTaskById](https://github.com/LegrandThomas/TodoList/assets/103045194/8373af3f-1724-4fd9-8474-35d27ad98ec9)

![postman_GetTasksByStatus](https://github.com/LegrandThomas/TodoList/assets/103045194/a85f89db-3179-4f45-a323-1d8d45ab4382)

</details>


<details>
<summary>Swagger</summary>

![swagger1](https://github.com/LegrandThomas/TodoList/assets/103045194/1467ca23-1c83-481d-a203-5f79a64747d3)

![swagger2](https://github.com/LegrandThomas/TodoList/assets/103045194/dd72f443-e25e-4dbe-8680-4c201c6a244f)

</details>

### IOC/DI:

### Commentaires divers:

### Installation/Mise en route:

