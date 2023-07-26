using Api.TodoList.Data.Context;
using Api.TodoList.Data.Context.Contract;
using Api.TodoList.Data.Entity.Model;
using Api.TodoList.Data.Repository;
using Api.TodoList.Data.Repository.Contract;
using Api.TodoList.Service;
using Api.TodoList.Service.Contract;
using Api.TodoList.Service.DTO;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Api.TodoList.Application
{
    public class Program

    {
        //option menu principal
        private const string MenuOptionAddTask = "1";
        private const string MenuOptionShowTasks = "2";
        private const string MenuOptionModifyTask = "3";
        private const string MenuOptionDeleteTask = "4";
        private const string MenuOptionFilterTasksByStatus = "5";
        private const string MenuOptionQuit = "6";

        //color
        private const System.ConsoleColor bgcolorBleu = ConsoleColor.DarkBlue;
        private const System.ConsoleColor bgcolorBlack = ConsoleColor.Black;
        private const System.ConsoleColor bgcolorWhite = ConsoleColor.White;
        private const System.ConsoleColor bgcolorGreen = ConsoleColor.Green;
        private const System.ConsoleColor bgcolorRed = ConsoleColor.Red;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var httpClient = new HttpClient();

            // Configurez la base URL de votre API ici
            httpClient.BaseAddress = new Uri(" https://localhost:7063/");

            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");

            builder.Services.AddDbContext<ITodoListDbContext, TodoListDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))

                    .EnableDetailedErrors());

            builder.Services.AddScoped<IRepository<User>, UserRepository>();
            builder.Services.AddScoped<IRepository<Data.Entity.Model.Task>, TaskRepository>();
            // builder.Services.AddScoped<IRepository<Status>, StatusRepository>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITaskService, TaskService>();
            builder.Services.AddScoped<IStatusService, StatusService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            System.Threading.Tasks.Task.Run(async () =>
            {
                await ShowWelcomeScreenAsync();

            });

            app.Run();
        }

        /// <summary>
        /// Affiche le Welcome Menu
        /// </summary>
        private static async System.Threading.Tasks.Task ShowWelcomeScreenAsync()
        {
            Console.BackgroundColor = bgcolorBleu;
            Console.ForegroundColor = bgcolorWhite;
            Console.WriteLine("                 --------------------                 ");
            Console.WriteLine("                 |    To Do List     |                ");
            Console.WriteLine("                 --------------------                 ");
            Console.BackgroundColor = bgcolorBlack;

            Console.WriteLine(" Appuyez sur une touche pour continuer...");

            Console.ReadKey();
            Console.Clear();
            await ShowMenuAsync();
        }

        /// <summary>
        /// affiche le Menu principal
        /// </summary>
        private static async System.Threading.Tasks.Task ShowMenuAsync()
        {
            var httpClient = new HttpClient();

            // Configurez la base URL de votre API ici
            httpClient.BaseAddress = new Uri(" https://localhost:7063/");

            Console.BackgroundColor = bgcolorBleu;
            Console.ForegroundColor = bgcolorWhite;

            Console.WriteLine("              -------------------------------------------               ");
            Console.WriteLine("             |             === Menu ===                  |              ");
            Console.WriteLine("             |    1. Ajouter une t�che                   |              ");
            Console.WriteLine("             |    2. Afficher la liste des t�ches        |              ");
            Console.WriteLine("             |    3. Modifier une t�che                  |              ");
            Console.WriteLine("             |    4 .Supprimer une t�che                 |              ");
            Console.WriteLine("             |    5. Filtrer les t�ches par status       |              ");
            Console.WriteLine("             |    6. Quitter                             |              ");
            Console.WriteLine("              -------------------------------------------               ");
            Console.WriteLine("\n");
            Console.BackgroundColor = bgcolorBlack;
            Console.Write("S�lectionnez une option : ");
            string? input = Console.ReadLine();
            Console.WriteLine(input);
            switch (input)
            {
                case MenuOptionAddTask:
                    // Appeler la m�thode correspondant � l'option 1
                    await AddTask(httpClient);

                    break;
                case MenuOptionShowTasks:
                    await DisplayTasksWithUsersAsync(httpClient);
                    break;


                case MenuOptionModifyTask:
                    // Appeler la m�thode correspondant � l'option 3
                    Console.ForegroundColor = bgcolorGreen;
                    Console.WriteLine("Option 3 s�lectionn�e.");
                    Console.ForegroundColor = bgcolorWhite;
                    break;
                case MenuOptionDeleteTask:
                    // Appeler la m�thode correspondant � l'option 4
                    Console.ForegroundColor = bgcolorGreen;
                    Console.WriteLine("Option 4 s�lectionn�e.");
                    Console.ForegroundColor = bgcolorWhite;
                    break;
                case MenuOptionFilterTasksByStatus:
                    // Appeler la m�thode correspondant � l'option 5
                    Console.ForegroundColor = bgcolorGreen;
                    Console.WriteLine("Option 5 s�lectionn�e.");
                    Console.ForegroundColor = bgcolorWhite;
                    break;
                case MenuOptionQuit:
                    Console.WriteLine("Le programme va se terminer. Appuyez sur une touche pour quitter...");
                    Console.ReadKey();
                    Environment.Exit(0);
                    break;
                default:
                    Console.ForegroundColor = bgcolorRed;
                    Console.WriteLine("Option invalide. Veuillez r�essayer.\n");
                    Console.ForegroundColor = bgcolorWhite;

                    await ShowMenuAsync();
                    break;
            }
        }
        /// <summary>
        /// permet � l'utilisateur de s'ajouter une t�che
        /// </summary>
        private static async Task<bool> AddTask(HttpClient httpClient)
        {
            bool addAnotherTask = true;
            do
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("***   Vous avez choisi d'ajouter une t�che.   ***\n");
                Console.ForegroundColor = ConsoleColor.White;

                // R�cup�ration de l'id du user voir pour current_user_id en cas de logging
                var NewTaskIdUser = 1;

                Console.WriteLine("Veuillez indiquer le nom de la t�che : ");
                var NewTaskName = Console.ReadLine();

                Console.WriteLine("Veuillez indiquer la description de la t�che : ");
                var NewTaskDesc = Console.ReadLine();

                Console.WriteLine("Veuillez indiquer la date de cr�ation de la t�che, au format DD/MM/YYYY, merci : ");
                var NewTaskDateCreation = Console.ReadLine();

                string tmpdate;
                do
                {
                    Console.WriteLine("Veuillez indiquer le statut de cette t�che :");
                    Console.WriteLine("   - 1 : � faire");
                    Console.WriteLine("   - 2 : En cours");
                    Console.WriteLine("   - 3 : Termin�e");

                    tmpdate = Console.ReadLine();
                } while (tmpdate != "1" && tmpdate != "2" && tmpdate != "3");

                int NewTaskStatus;
                string NewTaskStatusString;
                string NewTaskDateCloture = "Nulle";
                string TaskStatusAFaire = "� faire";
                string TaskStatusEnCours = "en cours";
                string TaskStatusTerminee = "termin�e";
                string TaskStatusDefault = "non d�finie";

                switch (tmpdate)
                {
                    case "1":
                        NewTaskStatus = 1;
                        NewTaskStatusString = TaskStatusAFaire;
                        break;
                    case "2":
                        NewTaskStatus = 2;
                        NewTaskStatusString = TaskStatusEnCours;
                        break;
                    case "3":
                        NewTaskStatus = 3;
                        NewTaskStatusString = TaskStatusTerminee;
                        Console.WriteLine("Veuillez indiquer la date de r�solution de la t�che, au format DD/MM/YYYY, merci : ");
                        NewTaskDateCloture = Console.ReadLine();
                        break;
                    default:
                        NewTaskStatus = 0;
                        NewTaskStatusString = TaskStatusDefault;
                        break;
                }

                // Cr�ez un nouvel objet CreateTaskDTO pour encapsuler les informations de la nouvelle t�che et les envoyer � l'API
                var taskDTO = new CreateTaskDTO
                {
                    Name = NewTaskName,
                    Description = NewTaskDesc,
                    IdStatus = NewTaskStatus,
                    // DateCreated = DateTime.Parse(NewTaskDateCreation),
                    // DateDue est utilis�e seulement si la t�che est "Termin�e"
                    DateDue = NewTaskDateCloture,
                    IdUser = NewTaskIdUser
                };

                // Envoi des donn�es � l'API en utilisant HttpClient
                try
                {
                    HttpResponseMessage response = await httpClient.PostAsJsonAsync("api/Task", taskDTO);
                    // La t�che a �t� ajout�e avec succ�s
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("T�che ajout�e avec succ�s !");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                catch (Exception ex)
                {
                    // G�rer les erreurs en cas d'�chec de l'ajout de la t�che
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Erreur lors de l'ajout de la t�che : " + ex.ToString());
                    Console.ForegroundColor = ConsoleColor.White;

                }


                // Affichage des informations saisies
                Console.WriteLine("Voici les informations que vous avez saisies :\n");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Votre id: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(NewTaskIdUser);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Nom de la t�che: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(NewTaskName);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Description de la t�che: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(NewTaskDesc);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Statut de la t�che: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(NewTaskStatusString);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Date de cr�ation de la t�che: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(NewTaskDateCreation);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Date de cl�ture de la t�che : ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(NewTaskDateCloture);

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
                            addAnotherTask = false;
                            Console.Clear();
                            await ShowMenuAsync();
                            break;
                        case "2":
                            addAnotherTask = false;
                            Console.WriteLine("Le programme va se terminer. Appuyez sur une touche pour quitter...");
                            Console.ReadKey();
                            Environment.Exit(0);
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Option invalide. Veuillez r�essayer.\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                    }
                } while (choix != "1" && choix != "2");
            } while (addAnotherTask);

            return true;
        }

        private static async System.Threading.Tasks.Task DisplayTasksWithUsersAsync(HttpClient httpClient)
        {
            Console.ForegroundColor = bgcolorGreen;
            Console.WriteLine("Vous avez choisi d'afficher la liste des taches.");
            Console.ForegroundColor = bgcolorWhite;

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync("api/User");
                if (response.IsSuccessStatusCode)
                {
                    var usersWithTasks = await response.Content.ReadFromJsonAsync<List<UserWithTasksDTO>>();
                    Console.ForegroundColor = bgcolorGreen;
                    Console.WriteLine("Liste des t�ches :\n");
                    Console.ForegroundColor = bgcolorWhite;

                    foreach (var user in usersWithTasks)
                    {
                        Console.WriteLine($"Utilisateur : {user.FirstName} {user.LastName} (ID : {user.IdUser})");
                        foreach (var task in user.Tasks)
                        {
                            DisplayTaskDetails(task);
                        }
                    }
                }
                else
                {
                    Console.ForegroundColor = bgcolorRed;
                    Console.WriteLine("Erreur lors de la r�cup�ration de la liste des t�ches : " + response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = bgcolorRed;
                Console.WriteLine("Erreur lors de la r�cup�ration de la liste des t�ches : " + ex.ToString());
            }
            Console.ForegroundColor = bgcolorWhite;
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
                        await ShowMenuAsync();
                        break;
                    case "2":
                        Console.WriteLine("Le programme va se terminer. Appuyez sur une touche pour quitter...");
                        Console.ReadKey();
                        Environment.Exit(0);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Option invalide. Veuillez r�essayer.\n");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }
            } while (choix != "1" && choix != "2");
        }

        private static void DisplayTaskDetails(Data.Entity.Model.Task task)
        {
            Console.WriteLine($"ID de la t�che : {task.IdTask}");
            Console.WriteLine($"ID de l'utilisateur : {task.IdUser}");
            Console.WriteLine($"ID du statut : {task.IdStatus}");
            Console.WriteLine($"Nom de la t�che : {task.Name}");
            Console.WriteLine($"Description de la t�che : {task.Description}");
            Console.WriteLine($"Date de cr�ation de la t�che : {task.DateCreated}");
            Console.WriteLine($"Date de cl�ture de la t�che : {task.DateDue ?? DateTime.MinValue}");
            Console.WriteLine("-------------------------------------------");
        }

        public class UserWithTasksDTO
        {
            public int? IdUser { get; set; }
            public List<Data.Entity.Model.Task>? Tasks { get; set; }
            public string? LastName { get; set; }
            public string? FirstName { get; set; }
            public string? Email { get; set; }
        }

    }
}