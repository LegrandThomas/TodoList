using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json.Linq;

namespace TodoList.FrontCLI
{
    internal class Program
    {
        //option menu principal
        private const string MenuOptionAddTask = "1";
        private const string MenuOptionShowTasks = "2";
        private const string MenuOptionModifyTask = "3";
        private const string MenuOptionDeleteTask = "4";
        private const string MenuOptionFilterTasksByStatus = "5";
        private const string MenuOptionQuit = "6";

        //color
        private const ConsoleColor BgColorBlue = ConsoleColor.DarkBlue;
        private const ConsoleColor BgColorBlack = ConsoleColor.Black;
        private const ConsoleColor BgColorWhite = ConsoleColor.White;
        private const ConsoleColor BgColorGreen = ConsoleColor.Green;
        private const ConsoleColor BgColorRed = ConsoleColor.Red;

        private static JObject _user = null;
        private static int _userId;
        private static int _statusId;

        private static async Task Main() // Modifiez la méthode Main pour qu'elle soit asynchrone
        {
            using HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(" https://localhost:7063/");

            await ShowWelcomeScreenAsync(client); // Attend la fin de l'exécution de ShowWelcomeScreenAsync
        }

        /// <summary>
        /// Affiche le Welcome Menu
        /// </summary>
        private static async Task ShowWelcomeScreenAsync(HttpClient client)
        {
            bool isLogged = false;
            while (!isLogged)
            {
                Console.BackgroundColor = BgColorBlue;
                Console.ForegroundColor = BgColorWhite;
                Console.WriteLine("                 --------------------                 ");
                Console.WriteLine("                 |    To Do List     |                ");
                Console.WriteLine("                 --------------------                 ");
                Console.BackgroundColor = BgColorBlack;

                // Console.WriteLine("Tapez votre adresse e-mail et votre mot de passe pour continuer...");
                Console.WriteLine("Tapez votre adresse e-mail pour continuer...");

                string? input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Tapez votre adresse e-mail...");
                    continue;
                }

                if (!input.Contains("@"))
                {
                    Console.WriteLine("Tapez une adresse e-mail valide...");
                    continue;
                }

                HttpResponseMessage response = await client.GetAsync($"api/User/Email/{input}");
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Adresse e-mail inexistante...");
                    continue;
                }

                string jsonContent = await response.Content.ReadAsStringAsync();
                _user = JObject.Parse(jsonContent);

                if (_user == null)
                {
                    Console.WriteLine("Une erreur inconnue est survenue lors de l'identification...");
                    continue;
                }

                isLogged = true;
                _userId = (int)_user["idUser"];
                await ShowMenuAsync(client);
            }
        }

        /// <summary>
        /// affiche le Menu principal
        /// </summary>
        private static async Task ShowMenuAsync(HttpClient client)
        {
            Console.BackgroundColor = BgColorBlue;
            Console.ForegroundColor = BgColorWhite;

            Console.WriteLine("              -------------------------------------------               ");
            Console.WriteLine("             |             === Menu ===                  |              ");
            Console.WriteLine("             |    1. Ajouter une tâche                   |              ");
            Console.WriteLine("             |    2. Afficher la liste des tâches        |              ");
            Console.WriteLine("             |    3. Modifier une tâche                  |              ");
            Console.WriteLine("             |    4. Supprimer une tâche                 |              ");
            Console.WriteLine("             |    5. Filtrer les tâches par status       |              ");
            Console.WriteLine("             |    6. Quitter                             |              ");
            Console.WriteLine("              -------------------------------------------               ");
            Console.WriteLine("\n");
            Console.BackgroundColor = BgColorBlack;
            Console.Write("Sélectionnez une option : ");
            string? input = Console.ReadLine();
            Console.WriteLine(input);
            switch (input)
            {
                case MenuOptionAddTask:
                    // Appeler la méthode correspondant à l'option 1
                    await AddTask(client);

                    break;
                case MenuOptionShowTasks:

                    HttpResponseMessage response = await client.GetAsync($"api/Task/User/{_userId}");

                    string tasks = await VerifyContentAndReturnJsonContent(response);

                    if(!string.IsNullOrEmpty(tasks))
                    {
                        ShowTasksByUser(tasks);
                    }
                    break;

                case MenuOptionModifyTask:
                    // Appeler la méthode correspondant à l'option 3
                    Console.ForegroundColor = BgColorGreen;
                    Console.WriteLine("Option 3 sélectionnée.");
                    Console.ForegroundColor = BgColorWhite;
                    await ModifyTaskAsync(client);
                    break;
                case MenuOptionDeleteTask:
                    // Appeler la méthode correspondant à l'option 4
                    await DeleteTaskAsync(client);
                    Console.ForegroundColor = BgColorGreen;
                    Console.WriteLine("Option 4 sélectionnée.");
                    Console.ForegroundColor = BgColorWhite;
                    break;
                case MenuOptionFilterTasksByStatus:
                    // Appeler la méthode correspondant à l'option 5

                    string choixStatus = ShowTaskByStatus();

                    if(!int.TryParse(choixStatus, out _statusId))
                    {
                        Console.WriteLine("Veuillez sélectionner un statut valide");
                        break; 
                    }

                    HttpResponseMessage tasksByStatus = await client.GetAsync($"api/Task/User/{_userId}/Status/{_statusId}");

                    string? allTasksByStatus= await VerifyContentAndReturnJsonContent(tasksByStatus);

                    if(allTasksByStatus != null) 
                    {
                        ShowTasksByUser(allTasksByStatus);
                    }
                   
                    break;
                case MenuOptionQuit:
                    Console.WriteLine("Le programme va se terminer. Appuyez sur une touche pour quitter...");
                    Console.ReadKey();
                    Environment.Exit(0);
                    break;
                default:
                    Console.ForegroundColor = BgColorRed;
                    Console.WriteLine("Option invalide. Veuillez réessayer.\n");
                    Console.ForegroundColor = BgColorWhite;

                    break;
            }

            await ShowMenuAsync(client);
        }

        /// <summary>
        /// permet à l'utilisateur de s'ajouter une tâche
        /// </summary>
        private static async Task AddTask(HttpClient client)
        {
            bool addAnotherTask = true;
            do
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("***   Vous avez choisi d'ajouter une tâche.   ***\n");
                Console.ForegroundColor = ConsoleColor.White;

                // Récupération de l'id du user voir pour current_user_id en cas de logging
                int newTaskIdUser = _userId;

                Console.WriteLine("Veuillez indiquer le nom de la tâche : ");
                string? newTaskName = Console.ReadLine();

                Console.WriteLine("Veuillez indiquer la description de la tâche : ");
                string? newTaskDesc = Console.ReadLine();

                var task = new
                {
                    Name = newTaskName,
                    Description = newTaskDesc,
                    IdStatus = 1,
                    DateCreated = DateTime.Now,
                    // DateDue est utilisée seulement si la tâche est "Terminée"
                    IdUser = newTaskIdUser
                };

                // Envoi des données à l'API en utilisant HttpClient
                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync("api/Task", task);

                    if(response.IsSuccessStatusCode)
                    { 
                        // La tâche a été ajoutée avec succès
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Tâche ajoutée avec succès !");
                        Console.ForegroundColor = ConsoleColor.White;

                        // Affichage des informations saisies
                        Console.WriteLine("Voici les informations que vous avez saisies :\n");

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("Votre id: ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine(newTaskIdUser);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("Nom de la tâche: ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine(newTaskName);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("Description de la tâche: ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine(newTaskDesc);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("Statut de la tâche: ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("à faire");

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("Date de création de la tâche: ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine(DateTime.Now);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("Date de clôture de la tâche : ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Aucune");

                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(" Action annulée, une tâche porte déjà ce nom");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                catch (Exception ex)
                {
                    // Gérer les erreurs en cas d'échec de l'ajout de la tâche
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Erreur lors de l'ajout de la tâche : " + ex.ToString());
                    Console.ForegroundColor = ConsoleColor.White;

                }

                string choix;
                do
                {
                    Console.WriteLine("\nSouhaitez-vous retourner au menu ?\n");
                    Console.WriteLine(" 1. Retour au Menu");
                    Console.WriteLine(" 2. Quitter");

                    choix = Console.ReadLine();

                    switch (choix)
                    {
                        case "1":
                            addAnotherTask = false;
                            Console.Clear();
                            await ShowMenuAsync(client);
                            break;
                        case "2":
                            addAnotherTask = false;
                            Console.WriteLine("Le programme va se terminer. Appuyez sur une touche pour quitter...");
                            Console.ReadKey();
                            Environment.Exit(0);
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Option invalide. Veuillez réessayer.\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                    }
                } while (choix != "1" && choix != "2");
            } while (addAnotherTask);
        }

        private static async Task<string> VerifyContentAndReturnJsonContent(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Une erreur est survenue lors de la récupération des tâches...");
                return "";
            }

            string jsonContent = await response.Content.ReadAsStringAsync();

            return jsonContent; 
        }

        private static void ShowTasksByUser(string jsonContent)
        {
            JArray tasks = JArray.Parse(jsonContent);

            IEnumerable<string> taskNamesAndDescriptions = tasks.Select(taskObj =>
            {
                string statusText;
                int idStatus = (int)taskObj["idStatus"];

                switch (idStatus)
                {
                    case 1:
                        statusText = "À faire";
                        break;
                    case 2:
                        statusText = "En cours";
                        break;
                    case 3:
                        statusText = "Terminé";
                        break;
                    default:
                        statusText = "À faire";
                        break;
                }

                return $"- Nom: {taskObj["name"]}\n" +
                       $"- Description: {taskObj["description"]}\n" +
                       $"- Statut: {statusText}\n" +
                       $"- Date de création: {taskObj["dateCreated"]}\n" +
                       $"- Date d'échéance: {taskObj["dateDue"]}\n";
            });

            Console.WriteLine(string.Join(", ", taskNamesAndDescriptions));
        }

        private static string ShowTaskByStatus()
        {
            Console.WriteLine("Veuillez Sélectionner un statut : ");
            Console.WriteLine("1 - A Faire");
            Console.WriteLine("2 - En cours");
            Console.WriteLine("3 - Terminé");

            string choixStatut = Console.ReadLine().Trim();

            return choixStatut;
        }

        // todo in progress
        private static async Task ModifyTaskAsync(HttpClient client)
        {
            bool editAnotherTask = true;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("***   Vous avez choisi de modifier une tâche.   ***\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Veuillez indiquer le nom de la tâche ou l'id de la tâche : ");

            while (editAnotherTask)
            {
                string? taskNameOrId = null;
                while (string.IsNullOrEmpty(taskNameOrId))
                {
                    taskNameOrId = Console.ReadLine();
                    Console.WriteLine("Veuillez indiquer le nom de la tâche ou son id pour continuer ...");
                }

                JToken? taskToEdit = null;

                taskToEdit = int.TryParse(taskNameOrId, out var taskId) ?
                    _user["tasks"].FirstOrDefault(task => task["idTask"].Value<int>() == taskId) :
                    _user["tasks"].FirstOrDefault(task => task["name"].Value<string>() == taskNameOrId);

                if (taskToEdit == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"La tâche '{taskNameOrId}' n'a pas été trouvée.");
                    Console.ForegroundColor = ConsoleColor.White;
                    return;
                }

                Console.WriteLine("Choisir une option d'édition :");
                Console.WriteLine("- 1 : Modifier le nom");
                Console.WriteLine("- 2 : Modifier la description");
                Console.WriteLine("- 3 : Modifier le status");
                Console.WriteLine("- 4 : Modifier le status");
                Console.WriteLine("- 5 : Modifier la date de création");
                Console.WriteLine("- 6 : Modifier la date d'échéance");

                string? editionOption = null;
                while (editionOption is not "1" or "2" or "3" or "4" or "5" or "6")
                {
                    editionOption = Console.ReadLine();
                    Console.WriteLine("Veuillez sélectionner une option valide 1, 2, 3, 4, 5, 6 pour continuer ...");
                }

                switch (editionOption)
                {
                    case "1":
                        Console.WriteLine($"Ancien nom : {taskToEdit["name"]}");
                        Console.WriteLine("Entrez un nouveau nom :");
                        string? newName = null;
                        while (string.IsNullOrEmpty(newName))
                        {
                            newName = Console.ReadLine();
                            if (taskToEdit["name"].Value<string>() == newName)
                            {
                                newName = null;
                                Console.WriteLine("Le nouveau nom doit être différent de l'ancien ...");
                            }

                            Console.WriteLine("Entrez un nom pour continuer ...");
                        }

                        taskToEdit["name"] = newName;
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "4":
                        break;
                    case "5":
                        break;
                    case "6":
                        break;
                }

                HttpResponseMessage updateResponse = await client.PutAsJsonAsync("api/Task/", taskToEdit.ToObject<JObject>().ToString());
                if (!updateResponse.IsSuccessStatusCode)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Une erreur est survenue lors de la modification de la tâche.");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.WriteLine("Revenir au menu d'édition :");
                Console.WriteLine("- 1 : Oui");
                Console.WriteLine("- 2 : Non et revenir au menu principal");
                string? optionChoice = null;
                while (optionChoice is not "1" or "2")
                {
                    optionChoice = Console.ReadLine();
                    Console.WriteLine("Veuillez sélectionner une option valide 1, 2 pour continuer ...");
                }

                switch (optionChoice)
                {
                    case "2":
                        Console.Clear();
                        await ShowMenuAsync(client);
                        break;
                }
            }
        }

        private static async Task DeleteTaskAsync(HttpClient client)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("***   Vous avez choisi de supprimer une tâche.   ***\n");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Veuillez entrer le nom de la tâche que vous souhaitez supprimer :");
            string taskName = Console.ReadLine().ToLower();

            // Récupérer les tâches de l'utilisateur pour vérifier si la tâche existe
            HttpResponseMessage response = await client.GetAsync($"api/Task/User/{_userId}");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Une erreur est survenue lors de la récupération des tâches...");
                return;
            }

            string jsonContent = await response.Content.ReadAsStringAsync();
            JArray tasks = JArray.Parse(jsonContent);
            JToken? taskToDelete = tasks.FirstOrDefault(task => task["name"].ToString() == taskName);

            if (taskToDelete == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"La tâche '{taskName}' n'a pas été trouvée.");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Détails de la tâche à supprimer :");
            Console.WriteLine($"Nom : {taskToDelete["name"]}");
            Console.WriteLine($"Description : {taskToDelete["description"]}");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Voulez-vous vraiment supprimer cette tâche ? (O/N)");
            string confirmation = Console.ReadLine().ToLower();

            if (confirmation == "o")
            {
                int taskId = (int)taskToDelete["idTask"];

                HttpResponseMessage deleteResponse = await client.DeleteAsync($"api/Task/{taskId}");
                if (!deleteResponse.IsSuccessStatusCode)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Une erreur est survenue lors de la suppression de la tâche.");
                    Console.ForegroundColor = ConsoleColor.White;
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Tâche '{taskName}' supprimée avec succès !");
                Console.ForegroundColor = ConsoleColor.White;

                string choix;
                do
                {
                    Console.WriteLine("\nSouhaitez-vous retourner au menu ?\n");
                    Console.WriteLine(" 1. Retour au Menu");
                    Console.WriteLine(" 2. Quitter");

                    choix = Console.ReadLine();

                    switch (choix)
                    {
                        case "1":
                            Console.Clear();
                            await ShowMenuAsync(client);
                            break;
                        case "2":
                            Console.WriteLine("Le programme va se terminer. Appuyez sur une touche pour quitter...");
                            Console.ReadKey();
                            Environment.Exit(0);
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Option invalide. Veuillez réessayer.\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                    }
                } while (choix != "1" && choix != "2");

                return;
            }
            else
            {
                Console.WriteLine("Suppression annulée.");
                return;
            }
        }

    }
}