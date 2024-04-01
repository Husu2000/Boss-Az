using Newtonsoft.Json;
using System.Net.Mail;
using System.Net;
using ConsoleApp2.Models;



namespace BossAz
{
	public class Program
	{


		static UserData userData = new UserData()
		{
			Workers = new List<Worker>(),
			Employers = new List<Employer>()
		};


		static int selectedIndex = 0;
		static int totalOptions = 3;


	// Email Send

		public static bool SendCodeToEmail(string code, string addressEmail)
		{
			string fromAddress = "huseyn200825@gmail.com";
			string password = "imdqayxqfneunmbf";

			MailMessage mailMessage = new MailMessage(fromAddress, addressEmail)
			{
				IsBodyHtml = true,
				Subject = "Boss.az Email Verify Code",

				Body = @$"<!DOCTYPE html>
			<html lang=""en"">
			<head>
				<meta charset=""UTF-8"">
				<meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
				<title>Document</title>
				<style>
					@import url('https://fonts.googleapis.com/css2?         family=Poppins:ital,wght@0,100;0,200;0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,100;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&disp   lay=   swap ');
					.box{{
						text-align: center;
						position: relative;
						top: 150px;
					}}     
					.confirmation-code{{
						font-size: 90px;
						font-weight: bold;
					}}
				</style>
			</head>
			<body>
				<div class=""box"">
					<p>Your Email Verify Code : </p>
					<h2 class=""confirmation-code"">" + @$"{code}</h2>
				</div>
			</body>
			</html>"
			};

			SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
			{
				Port = 587,
				Credentials = new NetworkCredential(fromAddress, password),
				EnableSsl = true,
			};

			try
			{
				smtpClient.Send(mailMessage);
				return true;
			}
			catch { return false; }
		}

	//------------------------------------------------------------------------------------------------------




		static void RegisterWorker()
		{
			Console.Clear();
			Console.WriteLine($@"
									 
 
██████╗ ███████╗ ██████╗ ██╗███████╗████████╗███████╗██████╗ 
██╔══██╗██╔════╝██╔════╝ ██║██╔════╝╚══██╔══╝██╔════╝██╔══██╗
██████╔╝█████╗  ██║  ███╗██║███████╗   ██║   █████╗  ██████╔╝
██╔══██╗██╔══╝  ██║   ██║██║╚════██║   ██║   ██╔══╝  ██╔══██╗
██║  ██║███████╗╚██████╔╝██║███████║   ██║   ███████╗██║  ██║
╚═╝  ╚═╝╚══════╝ ╚═════╝ ╚═╝╚══════╝   ╚═╝   ╚══════╝╚═╝  ╚═╝
                                                             

																 
																 
																 

 ");
			Console.WriteLine("\t\t\t\tWorker Register");

            Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("\t\t\t\tName: ");
			string WorkerName = Console.ReadLine();
			
			Console.ForegroundColor= ConsoleColor.Green;
			Console.Write("\t\t\t\tSurname: ");
            string WorkerSurname = Console.ReadLine();
			

			Console.ForegroundColor= ConsoleColor.Green;
			Console.Write("\t\t\t\tTelephone Number: ");
            string WorkerPhone = Console.ReadLine();

			Console.ForegroundColor= ConsoleColor.Green;
			Console.Write("\t\t\t\tAge: ");
            int WorkerAge = int.Parse(Console.ReadLine());
			
			Console.ForegroundColor=ConsoleColor.Green;
			Console.Write("\t\t\t\tUsername: ");
            string WorkerUserName = Console.ReadLine();
			
			Console.ForegroundColor= ConsoleColor.Green;
			Console.Write("\t\t\t\tEmail: ");
            string WorkerEmail = Console.ReadLine();
			Console.ForegroundColor = ConsoleColor.White;


//-----------------------------------------------------------------------------------------------


            Random random = new Random();

            while (true)
            {
                var code = random.Next(1000, 9999);

                if (SendCodeToEmail(code.ToString(), WorkerEmail))
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("\n\t\t\t\tVerify Code sended to your email.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("\n\n\t\tVerify Code not sended. Something went wrong. Please try again.");
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\n\t\t\t\tIf there is any Problem , write --> pause\n\t\t\t\tEnter the Verify Code or (pause) : ");

                
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Cyan;
                string verifyCode = Console.ReadLine();

                if (verifyCode == code.ToString()) break;
                else if (verifyCode == "pause")
                { Console.WriteLine("\t\t\t\t---Pause---."); Thread.Sleep(2500); break; ; }

                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine(
                    "\n\n\t\t\tVerify Code resended to your email.\n\t\t\tPlease enter a correct Verify Code !\n\n");
                Thread.Sleep(2500);
                Console.Clear();
                Console.WriteLine("\n\n\n\n");
            }

			
            //------------------------------------------------------------
			Console.Clear() ;
			Console.ForegroundColor= ConsoleColor.DarkGray;
            Console.Write("Write Password ==> ");
			string WorkerPassword = Console.ReadLine();

            List<CV> WorkerCVs = new List<CV>();

            Worker worker = new Worker(12,WorkerName,WorkerSurname,WorkerPhone,WorkerAge,WorkerUserName,WorkerEmail,WorkerPassword);

            userData.Workers.Add(worker);
			JsonDataManager.SaveUserData(userData);

			string logMessage = $"[{DateTime.Now}] Worker {worker.WorkerName} {worker.WorkerSurname} registered.";
			JsonDataManager.AddLogEntry(logMessage);

			Console.WriteLine("Worker Registered Successfully");
			Console.ForegroundColor=ConsoleColor.White;
			System.Threading.Thread.Sleep(1000);
		}

        static void RegisterEmployer()
        {
            Console.Clear();
            Console.WriteLine($@"
                                     
 
██████╗ ███████╗ ██████╗ ██╗███████╗████████╗███████╗██████╗ 
██╔══██╗██╔════╝██╔════╝ ██║██╔════╝╚══██╔══╝██╔════╝██╔══██╗
██████╔╝█████╗  ██║  ███╗██║███████╗   ██║   █████╗  ██████╔╝
██╔══██╗██╔══╝  ██║   ██║██║╚════██║   ██║   ██╔══╝  ██╔══██╗
██║  ██║███████╗╚██████╔╝██║███████║   ██║   ███████╗██║  ██║
╚═╝  ╚═╝╚══════╝ ╚═════╝ ╚═╝╚══════╝   ╚═╝   ╚══════╝╚═╝  ╚═╝
                                                             

                                                                 
                                                                 
                                                                 


 ");
            Console.WriteLine("\t\t\t\tRegister Employer");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\t\t\t\tName: ");
            string EmployerName = Console.ReadLine();

            Console.Write("\t\t\t\tSurname: ");
            string EmployerSurname = Console.ReadLine();

            Console.Write("\t\t\t\tTelephone Number: ");
            string EmployerPhone = Console.ReadLine();

            Console.Write("\t\t\t\tAge: ");
            int EmployerAge = int.Parse(Console.ReadLine());

            Console.Write("\t\t\t\tUsername: ");
            string EmployerUserName = Console.ReadLine();

            Console.Write("\t\t\t\tEmail: ");
            string EmployerEmail = Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.White;

            Random random = new Random();

            while (true)
            {
                var code = random.Next(1000, 9999);

                if (SendCodeToEmail(code.ToString(), EmployerEmail))
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("\n\t\t\t\tVerify Code sended to your email.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("\n\n\t\tVerify Code not sended. Something went wrong. Please try again.");
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\n\t\t\t\tIf there is any Problem , write --> pause\n\t\t\t\tEnter the Verify Code or (pause) : ");
             
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Cyan;
                string verifyCode = Console.ReadLine();

                if (verifyCode == code.ToString()) break;
                else if (verifyCode == "pause")
                {
                    Console.WriteLine("\t\t\t\t---Pause---.");
                    Thread.Sleep(2500);
                    break;
                }

                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine(
                    "\n\n\t\t\tVerify Code resended to your email.\n\t\t\tPlease enter a correct Verify Code !\n\n");
                Thread.Sleep(2500);
                Console.Clear();
                Console.WriteLine("\n\n\n\n");
            }
           
        
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Write Password ==> ");
            string EmployerPassword = Console.ReadLine();

			List<CV> PostedCv = new List<CV>();
			List<Vacancy> EmployerVacancies = new List<Vacancy>();

            Employer employer = new Employer(13, EmployerName, EmployerSurname, EmployerPhone, EmployerAge, EmployerUserName, EmployerEmail, EmployerPassword);

            userData.Employers.Add(employer);
            JsonDataManager.SaveUserData(userData);

            string logMessage = $"[{DateTime.Now}] Employer {employer.EmployerName} {employer.EmployerSurname} registered.";
            JsonDataManager.AddLogEntry(logMessage);

            Console.WriteLine("Employer Registered Successfully");
            Console.ForegroundColor = ConsoleColor.White;
            System.Threading.Thread.Sleep(1000);
        }


        static void RegisterMenu()
		{
			int registrationChoice = 1;

			while (true)
			{
				Console.Clear();
				Console.WriteLine($@"
									

																 
																 
																 

");

				if (registrationChoice == 1)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("\t\t\t\t   1. Worker Register ");
					Console.ForegroundColor = ConsoleColor.White;
					Console.WriteLine("\t\t\t\t   2. Employer Register");
					Console.WriteLine("\t\t\t\t   3. Exit To Main Menu");
				}
				else if (registrationChoice == 2)
				{
					Console.WriteLine("\t\t\t\t   1. Worker Register");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("\t\t\t\t   2. Employer Register");
					Console.ForegroundColor = ConsoleColor.White;
					Console.WriteLine("\t\t\t\t   3. Exit To Main Menu");
				}
				else if (registrationChoice == 3)
				{
					Console.WriteLine("\t\t\t\t   1. Worker Register");
					Console.WriteLine("\t\t\t\t   2. Employer Register");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("\t\t\t\t   3. Exit To Main Menu");
					Console.ForegroundColor = ConsoleColor.White;
				}
				else
				{
					Console.WriteLine("\t\t\t\t   1. Worker Register");
					Console.WriteLine("\t\t\t\t   2. Employer Register");
					Console.WriteLine("\t\t\t\t   3. Exit To Main Menu");

				}

				ConsoleKeyInfo registrationKeyInfo = Console.ReadKey();

				if (registrationKeyInfo.Key == ConsoleKey.UpArrow || registrationKeyInfo.Key == ConsoleKey.W)
				{
					registrationChoice--;
					if (registrationChoice < 1) registrationChoice = 3;
				}
				else if (registrationKeyInfo.Key == ConsoleKey.DownArrow || registrationKeyInfo.Key == ConsoleKey.S)
				{
					registrationChoice++;
					if (registrationChoice > 3) registrationChoice = 1;
				}
				else if (registrationKeyInfo.Key == ConsoleKey.Enter)
				{
					if (registrationChoice == 1)
					{
						RegisterWorker();
					}
					else if (registrationChoice == 2)
					{
						RegisterEmployer();
					}
					else if (registrationChoice == 3)
					{
						break;
					}

				}
				else if (registrationKeyInfo.Key != ConsoleKey.Escape)
				{
					break;
				}
			}
		}
		static void LoginMenu()
		{
			int loginChoice = 1;

			while (true)
			{
				Console.Clear();
				Console.WriteLine($@"
											

██╗      ██████╗  ██████╗ ██╗███╗   ██╗
██║     ██╔═══██╗██╔════╝ ██║████╗  ██║
██║     ██║   ██║██║  ███╗██║██╔██╗ ██║
██║     ██║   ██║██║   ██║██║██║╚██╗██║
███████╗╚██████╔╝╚██████╔╝██║██║ ╚████║
╚══════╝ ╚═════╝  ╚═════╝ ╚═╝╚═╝  ╚═══╝
                                       

																 
																 
																 


		");

				if (loginChoice == 1)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("\t\t\t\t=> 1. Login Worker");
					Console.ForegroundColor = ConsoleColor.White;
					Console.WriteLine("\t\t\t\t   2. Login Employer");
					Console.WriteLine("\t\t\t\t   3. Exit to Main Menu");
				}
				else if (loginChoice == 2)
				{
					Console.WriteLine("\t\t\t\t   1. Login Worker");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("\t\t\t\t=> 2. Login Employer");
					Console.ForegroundColor = ConsoleColor.White;
					Console.WriteLine("\t\t\t\t   3. Exit to Main Menu");
				}
				else if (loginChoice == 3)
				{
					Console.WriteLine("\t\t\t\t   1. Login Worker");
					Console.WriteLine("\t\t\t\t   2. Login Employer");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("\t\t\t\t=> 3. Exit to Main Menu");
					Console.ForegroundColor = ConsoleColor.White;
				}

				else
				{
					Console.WriteLine("\t\t\t\t   1. Login Worker");
					Console.WriteLine("\t\t\t\t   2. Login Employer");
					Console.WriteLine("\t\t\t\t   3. Exit to Main Menu");

				}

				ConsoleKeyInfo loginKeyInfo = Console.ReadKey();

				if (loginKeyInfo.Key == ConsoleKey.UpArrow || loginKeyInfo.Key == ConsoleKey.W)
				{
					loginChoice--;
					if (loginChoice < 1) loginChoice = 3;
				}
				else if (loginKeyInfo.Key == ConsoleKey.DownArrow || loginKeyInfo.Key == ConsoleKey.S)
				{
					loginChoice++;
					if (loginChoice > 3) loginChoice = 1;
				}
				else if (loginKeyInfo.Key == ConsoleKey.Enter)
				{
					if (loginChoice == 1)
					{
						LoginWorker();
						System.Threading.Thread.Sleep(1000);
						break;
					}
					else if (loginChoice == 2)
					{
						LoginEmployer();
						System.Threading.Thread.Sleep(1000);
						break;
					}
					else if (loginChoice == 3)
					{
						break;
					}

				}
				else if (loginKeyInfo.Key != ConsoleKey.Escape)
				{
					break;
				}
			}
		}

		static void LoginWorker()
		{
			Console.Clear();
			Console.WriteLine($@"
									 
 
██╗      ██████╗  ██████╗ ██╗███╗   ██╗
██║     ██╔═══██╗██╔════╝ ██║████╗  ██║
██║     ██║   ██║██║  ███╗██║██╔██╗ ██║
██║     ██║   ██║██║   ██║██║██║╚██╗██║
███████╗╚██████╔╝╚██████╔╝██║██║ ╚████║
╚══════╝ ╚═════╝  ╚═════╝ ╚═╝╚═╝  ╚═══╝
                                       

																 
																 
																 


 ");
			Console.WriteLine("Worker Login");

			Console.Write("Username: ");
			string userName = Console.ReadLine();

			Console.Write("Password: ");
			string password = Console.ReadLine();

            userData.Workers = JsonDataManager.LoadWorkerData();

			Worker authenticatedWorker = userData.Workers.FirstOrDefault(worker => worker.WorkerUserName == userName && worker.WorkerPassword == password);

			if (authenticatedWorker != null)
			{
				Console.WriteLine("Login Successful");
				System.Threading.Thread.Sleep(1000);
				WorkerMenu(authenticatedWorker);

				string logMessage = $"[{DateTime.Now}] Worker {authenticatedWorker.WorkerName} {authenticatedWorker.WorkerSurname} logged in.";
				JsonDataManager.AddLogEntry(logMessage);
			}
			else
			{
				Console.WriteLine("Wrong Username Or Password");

				string logMessage = $"[{DateTime.Now}] Worker login attempt with username: {userName} failed.";
				JsonDataManager.AddLogEntry(logMessage);
			}
		}

        static void LoginEmployer()
        {
            Console.Clear();
            Console.WriteLine($@"
									 

██╗      ██████╗  ██████╗ ██╗███╗   ██╗
██║     ██╔═══██╗██╔════╝ ██║████╗  ██║
██║     ██║   ██║██║  ███╗██║██╔██╗ ██║
██║     ██║   ██║██║   ██║██║██║╚██╗██║
███████╗╚██████╔╝╚██████╔╝██║██║ ╚████║
╚══════╝ ╚═════╝  ╚═════╝ ╚═╝╚═╝  ╚═══╝
                                       

																 
																 
																 


 ");
            Console.WriteLine("Employer Login");

            Console.Write("Username: ");
            string userName = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            userData.Employers = JsonDataManager.LoadEmployerData();

            Employer authenticatedWorker = userData.Employers.FirstOrDefault(employer => employer.EmployerUserName == userName && employer.EmployerPassword == password);

            if (authenticatedWorker is not null)
            {
                Console.WriteLine("Login Successful");
                System.Threading.Thread.Sleep(1000);
                EmployerMenu(authenticatedWorker);

                string logMessage = $"[{DateTime.Now}] Employers {authenticatedWorker.EmployerName} {authenticatedWorker.EmployerSurname} logged in.";
                JsonDataManager.AddLogEntry(logMessage);
            }
            else
            {
                Console.WriteLine("Wrong Username Or Password");

                string logMessage = $"[{DateTime.Now}] Employer login attempt with username: {userName} failed.";
                JsonDataManager.AddLogEntry(logMessage);
            }
        }


        static void EmployerMenu(Employer employer)
		{
			int Employerchoice = 1;

			while (true)
			{
				gotoa:
				Console.Clear();
				Console.WriteLine($@"
										 
 
███████╗███╗   ███╗██████╗ ██╗      ██████╗ ██╗   ██╗███████╗██████╗ 
██╔════╝████╗ ████║██╔══██╗██║     ██╔═══██╗╚██╗ ██╔╝██╔════╝██╔══██╗
█████╗  ██╔████╔██║██████╔╝██║     ██║   ██║ ╚████╔╝ █████╗  ██████╔╝
██╔══╝  ██║╚██╔╝██║██╔═══╝ ██║     ██║   ██║  ╚██╔╝  ██╔══╝  ██╔══██╗
███████╗██║ ╚═╝ ██║██║     ███████╗╚██████╔╝   ██║   ███████╗██║  ██║
╚══════╝╚═╝     ╚═╝╚═╝     ╚══════╝ ╚═════╝    ╚═╝   ╚══════╝╚═╝  ╚═╝
                                                                     

																 
																 
																 


		");

				if (Employerchoice == 1)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("\t\t\t\t=> 1. Create Vacancy ");
					Console.ForegroundColor = ConsoleColor.White;
					Console.WriteLine("\t\t\t\t   2. Show All Vacancy");
					Console.WriteLine("\t\t\t\t   3. Show Posted Cv");
					Console.WriteLine("\t\t\t\t   4. Exit To Last Menu");
				}
				else if (Employerchoice == 2)
				{
					Console.WriteLine("\t\t\t\t   1. Create Vacancy");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("\t\t\t\t=> 2. Show All Vacancy");
					Console.ForegroundColor = ConsoleColor.White;
					Console.WriteLine("\t\t\t\t   3. Show Posted Cv");
					Console.WriteLine("\t\t\t\t   4. Exit To Last Menu");
				}
				else if (Employerchoice == 3)
				{
					Console.WriteLine("\t\t\t\t   1. Create Vacancy");
					Console.WriteLine("\t\t\t\t   2. Show All Vacancy");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("\t\t\t\t+> 3. Show Posted Cv");
					Console.ForegroundColor = ConsoleColor.White;
					Console.WriteLine("\t\t\t\t   4. Exit To Last Menu");
				}
				else if (Employerchoice == 4)
				{
					Console.WriteLine("\t\t\t\t   1. Create Vacancy");
					Console.WriteLine("\t\t\t\t   2. Show All Vacancy");
					Console.WriteLine("\t\t\t\t   3. Show Posted Cv");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("\t\t\t\t=> 4. Exit To Last Menu");
					Console.ForegroundColor = ConsoleColor.White;
				}
				else
				{
					Console.WriteLine("\t\t\t\t   1. Create Vacancy");
					Console.WriteLine("\t\t\t\t   2. Show All Vacancy");
					Console.WriteLine("\t\t\t\t   3. Show Posted Cv");
					Console.WriteLine("\t\t\t\t   4. Exit To Last Menu");
				}

				ConsoleKeyInfo Employerkeyinfo = Console.ReadKey();

				if (Employerkeyinfo.Key == ConsoleKey.UpArrow || Employerkeyinfo.Key == ConsoleKey.W)
				{
					Employerchoice--;
					if(Employerchoice < 1) Employerchoice = 4;
				}
				else if (Employerkeyinfo.Key == ConsoleKey.DownArrow || Employerkeyinfo.Key == ConsoleKey.S)
				{
					Employerchoice++;
					if(Employerchoice > 4) Employerchoice = 1;
				}
				else if (Employerkeyinfo.Key == ConsoleKey.Enter)
				{
					if (Employerchoice == 1)
					{

						CreateVacancy(employer);
						goto gotoa;
					}
					else if (Employerchoice == 2)
					{
						ShowAllVacancy(employer);
						goto gotoa;
					}
					else if (Employerchoice == 3)
					{
						ShowPostedCv(employer);
						Console.ReadLine();
						goto gotoa;
					}
					else if (Employerchoice == 4)
					{
						break;
					}

				}
				else if (Employerkeyinfo.Key != ConsoleKey.Escape)
				{
					break;
				}

			}

		}
       
        static void CreateVacancy(Employer employer)
        {
            Console.Clear();
            Console.WriteLine("Create Vacancy");

            if (employer.EmployerVacancies == null)
            {
                employer.EmployerVacancies = new List<Vacancy>();
            }

            Console.Write("Enter Vacancy Name: ");
            string vacancyName = Console.ReadLine();

            Console.Write("Enter Vacancy Info: ");
            string vacancyInfo = Console.ReadLine();

            Console.Write("Enter Vacancy Salary: ");
            double salary;
            if (!double.TryParse(Console.ReadLine(), out salary))
            {
                Console.WriteLine("Invalid Salary. Vacancy creation failed.");
                return;
            }

            Vacancy newVacancy = new Vacancy(1, vacancyName, vacancyInfo, salary);
            employer.EmployerVacancies.Add(newVacancy);

			JsonDataManager.SaveUserData(userData);

            Console.WriteLine("Vacancy created successfully!");
        }


        static void ShowAllVacancy(Employer employer)
		{
			Console.Clear();
			Console.WriteLine($@"
									 
 /$$$$$$$                                      /$$$$$$           
| $$__  $$                                    /$$__  $$          
| $$  \ $$  /$$$$$$   /$$$$$$$ /$$$$$$$      | $$  \ $$ /$$$$$$$$
| $$$$$$$  /$$__  $$ /$$_____//$$_____/      | $$$$$$$$|____ /$$/
| $$__  $$| $$  \ $$|  $$$$$$|  $$$$$$       | $$__  $$   /$$$$/ 
| $$  \ $$| $$  | $$ \____  $$\____  $$      | $$  | $$  /$$__/  
| $$$$$$$/|  $$$$$$/ /$$$$$$$//$$$$$$$/      | $$  | $$ /$$$$$$$$
|_______/  \______/ |_______/|_______/       |__/  |__/|________/
																 
																 
																 

 ");
			Console.WriteLine("Show All Vacancy");

			if (employer.EmployerVacancies == null || employer.EmployerVacancies.Count == 0)
			{
				Console.WriteLine("No vacancies found.");
				return;
			}

			foreach (var vacancy in employer.EmployerVacancies)
			{
				Console.WriteLine($"Vacancy ID: {vacancy.VacancyId}");
				Console.WriteLine($"Vacancy Name: {vacancy.VacancyName}");
				Console.WriteLine($"Vacancy Info: {vacancy.VacancyInfo}");
				Console.WriteLine($"Vacancy Salary: {vacancy.VacancySalary}");
				Console.WriteLine();
			}
			Console.WriteLine("Press any key to continue...");
			Console.ReadKey();
		}

		static void ShowPostedCv(Employer employer)
		{
			Console.Clear();
			Console.WriteLine($@"
									 
 /$$$$$$$                                      /$$$$$$           
| $$__  $$                                    /$$__  $$          
| $$  \ $$  /$$$$$$   /$$$$$$$ /$$$$$$$      | $$  \ $$ /$$$$$$$$
| $$$$$$$  /$$__  $$ /$$_____//$$_____/      | $$$$$$$$|____ /$$/
| $$__  $$| $$  \ $$|  $$$$$$|  $$$$$$       | $$__  $$   /$$$$/ 
| $$  \ $$| $$  | $$ \____  $$\____  $$      | $$  | $$  /$$__/  
| $$$$$$$/|  $$$$$$/ /$$$$$$$//$$$$$$$/      | $$  | $$ /$$$$$$$$
|_______/  \______/ |_______/|_______/       |__/  |__/|________/
																 
																 
																 


 ");
			Console.WriteLine("Show Posted Cv");

			if (employer.PostedCv == null || employer.PostedCv.Count == 0)
			{
				Console.WriteLine("No CVs posted.");
				return;
			}

			foreach (var cv in employer.PostedCv)
			{
				Console.WriteLine($"Specialty: {cv.Specialty}");
				Console.WriteLine($"School: {cv.School}");
				Console.WriteLine($"University: {cv.University}");
				Console.WriteLine("Skills:");
				foreach (var skill in cv.Skills)
				{
					Console.WriteLine(skill);
				}
				Console.WriteLine("Companies:");
				foreach (var company in cv.Companies)
				{
					Console.WriteLine(company);
				}
				Console.WriteLine($"Start Date: {cv.StartDate}");
				Console.WriteLine($"End Date: {cv.EndDate}");
				Console.WriteLine("Languages:");
				foreach (var language in cv.Languages)
				{
					Console.WriteLine($"{language.Key}: {language.Value}");
				}
				Console.WriteLine($"Has Degree: {cv.HasDegree}");
				Console.WriteLine();
				Console.WriteLine("Press any key to continue...");
				Console.ReadKey();
			}
		}
		static void WorkerMenu(Worker worker)
		{
			int WorkerChoice = 1;
			gotoa:
			while (true)
			{
				Console.Clear();
				Console.WriteLine($@"
										  

██╗    ██╗ ██████╗ ██████╗ ██╗  ██╗███████╗██████╗ 
██║    ██║██╔═══██╗██╔══██╗██║ ██╔╝██╔════╝██╔══██╗
██║ █╗ ██║██║   ██║██████╔╝█████╔╝ █████╗  ██████╔╝
██║███╗██║██║   ██║██╔══██╗██╔═██╗ ██╔══╝  ██╔══██╗
╚███╔███╔╝╚██████╔╝██║  ██║██║  ██╗███████╗██║  ██║
 ╚══╝╚══╝  ╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝
                                                   

																 
																 
																 


		");

				if (WorkerChoice == 1)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("\t\t\t\t=> 1. Create CV ");
					Console.ForegroundColor = ConsoleColor.White;
					Console.WriteLine("\t\t\t\t   2. Show All CVs");
					Console.WriteLine("\t\t\t\t   3. CV Application");
					Console.WriteLine("\t\t\t\t   4. Exit To Last Menu");
				}
				else if (WorkerChoice == 2)
				{
					Console.WriteLine("\t\t\t\t   1. Create CV");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("\t\t\t\t=> 2. Show All CVs");
					Console.ForegroundColor = ConsoleColor.White;
					Console.WriteLine("\t\t\t\t   3. CV Application");
					Console.WriteLine("\t\t\t\t   4. Exit To Last Menu");
				}
				else if (WorkerChoice == 3)
				{
					Console.WriteLine("\t\t\t\t   1. Create CV");
					Console.WriteLine("\t\t\t\t   2. Show All CVs");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("\t\t\t\t=> 3. CV Application");
					Console.ForegroundColor = ConsoleColor.White;
					Console.WriteLine("\t\t\t\t   4. Exit To Last Menu");
				}
				else if (WorkerChoice == 4)
				{
					Console.WriteLine("\t\t\t\t   1. Create CV");
					Console.WriteLine("\t\t\t\t   2. Show All CVs");
					Console.WriteLine("\t\t\t\t   3. CV Application");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("\t\t\t\t=> 4. Exit To Last Menu");
					Console.ForegroundColor = ConsoleColor.White;
				}
				else
				{
					Console.WriteLine("\t\t\t\t   1. Create CV");
					Console.WriteLine("\t\t\t\t   2. List All CVs");
					Console.WriteLine("\t\t\t\t   3. CV Application");
					Console.WriteLine("\t\t\t\t   4. Exit To Last Menu");
				}

				ConsoleKeyInfo Workerkeyinfo = Console.ReadKey();

				if (Workerkeyinfo.Key == ConsoleKey.UpArrow || Workerkeyinfo.Key == ConsoleKey.W)
				{
					WorkerChoice--;
					if (WorkerChoice < 1) WorkerChoice = 4; 
				}
				else if (Workerkeyinfo.Key == ConsoleKey.DownArrow || Workerkeyinfo.Key == ConsoleKey.S)
				{
					WorkerChoice++;
					if (WorkerChoice > 4) WorkerChoice = 1;
						
				}
				else if (Workerkeyinfo.Key == ConsoleKey.Enter)
				{
					if (WorkerChoice == 1)
					{
						CreateCV(worker);
					}
					else if (WorkerChoice == 2)
					{
						ShowAllCVs(worker);
					}
					else if (WorkerChoice == 3)
					{
						CVApplication(worker);
						goto gotoa;
					}
					else if (WorkerChoice == 4)
					{
						break;
					}

				}
				else if (Workerkeyinfo.Key != ConsoleKey.Escape)
				{
					break;
				}

			}
           


        }
		static void CreateCV(Worker worker)
		{
			Console.Clear();
			Console.WriteLine($@"
											

 ██████╗██████╗ ███████╗ █████╗ ████████╗     ██████╗██╗   ██╗
██╔════╝██╔══██╗██╔════╝██╔══██╗╚══██╔══╝    ██╔════╝██║   ██║
██║     ██████╔╝█████╗  ███████║   ██║       ██║     ██║   ██║
██║     ██╔══██╗██╔══╝  ██╔══██║   ██║       ██║     ╚██╗ ██╔╝
╚██████╗██║  ██║███████╗██║  ██║   ██║       ╚██████╗ ╚████╔╝ 
 ╚═════╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝   ╚═╝        ╚═════╝  ╚═══╝  
                                                              

																 
																 
																 

		");
			Console.WriteLine("Create CV");

			if (worker.WorkerCVs == null)
			{
				worker.WorkerCVs = new List<CV>();
			}

			

			Console.Write("Specialty: ");
			string Specialty = Console.ReadLine();

			Console.Write("School: ");
			string  School = Console.ReadLine();

			Console.Write("University: ");
			string  University = Console.ReadLine();

			Console.Write("Skills (comma-separated): ");
			string skillsInput = Console.ReadLine();
			var Skills = skillsInput.Split(',').Select(skill => skill.Trim()).ToList();

			Console.Write("Companies (comma-separated): ");
			string companiesInput = Console.ReadLine();
			var Companies = companiesInput.Split(',').Select(company => company.Trim()).ToList();

			DateTime StartDate;

			Console.Write("Start Date (YYYY-MM-DD): ");
			if (DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
			{
				StartDate = startDate;
			}
			else
			{
				Console.WriteLine("Invalid date format. CV creation failed.");
				System.Threading.Thread.Sleep(1000);
				return;
			}

			DateTime EndDate;

			Console.Write("End Date (YYYY-MM-DD): ");
			if (DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
			{
				 EndDate = endDate;
			}
			else
			{
				Console.WriteLine("Invalid date format. CV creation failed.");
				System.Threading.Thread.Sleep(1000);
				return;
			}

			var Languages = new Dictionary<string, string>();
			while (true)
			{
				Console.Write("Language (press Enter to finish): ");
				string language = Console.ReadLine();
				if (string.IsNullOrWhiteSpace(language))
				{
					break;
				}

				Console.Write("Level: ");
				string level = Console.ReadLine();

				Languages[language] = level;
			}

			bool HasDegree;

			Console.Write("Do you have a degree? (true/false): ");
			if (bool.TryParse(Console.ReadLine(), out bool hasDegree))
			{
				HasDegree = hasDegree;
			}
			else
			{
				Console.WriteLine("Invalid input for degree. CV creation failed.");
				System.Threading.Thread.Sleep(1000);
				return;
			}

			CV cv = new CV(Specialty,School,University,StartDate,EndDate,HasDegree);

			cv.Companies = Companies;
			cv.Languages = Languages;
			cv.Skills = Skills;

			worker.WorkerCVs.Add(cv);

            foreach (var e in userData.Employers)
            {
				e.PostedCv.Add(cv);
            }

            Worker existingEWorker = userData.Workers.FirstOrDefault(e => e.WorkerId == worker.WorkerId);
			if (existingEWorker != null)
			{
				existingEWorker.WorkerCVs = worker.WorkerCVs;
			}

			JsonDataManager.SaveUserData(userData);
			Console.WriteLine("CV created successfully.");
			System.Threading.Thread.Sleep(1000);
		}

		static void CVApplication(Worker worker)
		{
			Console.Clear();
			Console.WriteLine($@"
									 
 
 ██████╗██╗   ██╗     █████╗ ██████╗ ██████╗ ██╗     ██╗ ██████╗ █████╗ ████████╗██╗ ██████╗ ███╗   ██╗
██╔════╝██║   ██║    ██╔══██╗██╔══██╗██╔══██╗██║     ██║██╔════╝██╔══██╗╚══██╔══╝██║██╔═══██╗████╗  ██║
██║     ██║   ██║    ███████║██████╔╝██████╔╝██║     ██║██║     ███████║   ██║   ██║██║   ██║██╔██╗ ██║
██║     ╚██╗ ██╔╝    ██╔══██║██╔═══╝ ██╔═══╝ ██║     ██║██║     ██╔══██║   ██║   ██║██║   ██║██║╚██╗██║
╚██████╗ ╚████╔╝     ██║  ██║██║     ██║     ███████╗██║╚██████╗██║  ██║   ██║   ██║╚██████╔╝██║ ╚████║
 ╚═════╝  ╚═══╝      ╚═╝  ╚═╝╚═╝     ╚═╝     ╚══════╝╚═╝ ╚═════╝╚═╝  ╚═╝   ╚═╝   ╚═╝ ╚═════╝ ╚═╝  ╚═══╝
                                                                                                       

																 
																 
																 

 ");
			Console.WriteLine("CV Application");

			bool VacanciesTrueOrFalse = true;
            userData.Workers  = JsonDataManager.LoadWorkerData();
            userData.Employers = JsonDataManager.LoadEmployerData();
			List<Employer> employers = userData.Employers;

			Console.WriteLine("Vacancies:");
			for (int i = 0; i < employers.Count; i++)
			{
				var employer = employers[i];
				Console.WriteLine($"Employer ID: {i + 1}");
				Console.WriteLine($"Employer Name: {employer.EmployerName}");
				Console.WriteLine();
				Console.WriteLine("Vacancies:");
				if (employer.EmployerVacancies == null || employer.EmployerVacancies.Count == 0)
				{
					Console.WriteLine("No vacancies found.");
					VacanciesTrueOrFalse = false;
				}
				else
				{
					for (int j = 0; j < employer.EmployerVacancies.Count; j++)
					{
						var vacancy = employer.EmployerVacancies[j];
						Console.WriteLine($"Vacancy ID: {j + 1}");
						Console.WriteLine($"Vacancy Name: {vacancy.VacancyName}");
						Console.WriteLine($"Vacancy Info: {vacancy.VacancyInfo}");
						Console.WriteLine($"Vacancy Salary: {vacancy.VacancySalary}");
						Console.WriteLine();
					}
				}
				Console.WriteLine();
			}

			Console.Write("Select an employer by entering their ID (or 0 to exit): ");
			if (int.TryParse(Console.ReadLine(), out int employerId))
			{
				if (employerId >= 1 && employerId <= employers.Count && VacanciesTrueOrFalse)
				{
					var selectedEmployer = employers[employerId - 1];

					Console.Clear();
					Console.WriteLine($"Vacancies from {selectedEmployer.EmployerName}:");
					Console.WriteLine();
					Console.WriteLine();
					for (int i = 0; i < selectedEmployer.EmployerVacancies.Count; i++)
					{
						var vacancy = selectedEmployer.EmployerVacancies[i];
						Console.WriteLine($"ID: {i + 1}");
						Console.WriteLine($"Name: {vacancy.VacancyName}");
						Console.WriteLine($"Info: {vacancy.VacancyInfo}");
						Console.WriteLine($"Salary: {vacancy.VacancySalary}");
						Console.WriteLine();
						Console.WriteLine();
					}

					Console.Write("Select a vacancy by entering its ID (or 0 to cancel): ");
					if (int.TryParse(Console.ReadLine(), out int vacancyId))
					{
						if (vacancyId >= 1 && vacancyId <= selectedEmployer.EmployerVacancies.Count)
						{
							var selectedVacancy = selectedEmployer.EmployerVacancies[vacancyId - 1];


							foreach (var cv in worker.WorkerCVs)
							{
								selectedEmployer.PostedCv.Add(cv);
							}
							string json = File.ReadAllText("users.json");
							UserData userDataa = JsonConvert.DeserializeObject<UserData>(json);


							Employer Existpostedcv = userDataa.Employers.FirstOrDefault(e => e.EmployerId == selectedEmployer.EmployerId);
							if (Existpostedcv != null)
							{
								Existpostedcv.PostedCv = selectedEmployer.PostedCv;
							}


							string updatedJson = JsonConvert.SerializeObject(userDataa);
							File.WriteAllText("users.json", updatedJson);
							Console.WriteLine("CV application successful.");
						}
						else if (vacancyId == 0)
						{
							Console.WriteLine("Application canceled.");
						}
						else
						{
							Console.WriteLine("Invalid vacancy ID.");
						}
					}
					else
					{
						Console.WriteLine("Invalid input.");
					}
				}
				else if (employerId == 0)
				{
					Console.WriteLine("Application canceled.");
				}
				else
				{
					Console.WriteLine("Invalid employer ID Or Dont Have Vacancies.");
				}
			}
			else
			{
				Console.WriteLine("Invalid input.");
			}

			System.Threading.Thread.Sleep(1000);
		}

		static void ShowAllCVs(Worker worker)
		{
			Console.Clear();
			Console.WriteLine($@"
										  

 █████╗ ██╗     ██╗          ██████╗██╗   ██╗███████╗
██╔══██╗██║     ██║         ██╔════╝██║   ██║██╔════╝
███████║██║     ██║         ██║     ██║   ██║███████╗
██╔══██║██║     ██║         ██║     ╚██╗ ██╔╝╚════██║
██║  ██║███████╗███████╗    ╚██████╗ ╚████╔╝ ███████║
╚═╝  ╚═╝╚══════╝╚══════╝     ╚═════╝  ╚═══╝  ╚══════╝
                                                     

																 
																 
																 


		");
			Console.WriteLine("Show All CVs");

			if (worker.WorkerCVs.Count == 0)
			{
				Console.WriteLine("You have no CVs.");
			}
			else
			{
				int index = 1;
				foreach (var cv in worker.WorkerCVs)
				{
					Console.WriteLine($"CV {index}:");
					Console.WriteLine($"Specialty: {cv.Specialty}");
					Console.WriteLine($"School: {cv.School}");
					Console.WriteLine($"University: {cv.University}");
					Console.WriteLine($"Skills: {string.Join(", ", cv.Skills)}");
					Console.WriteLine($"Companies: {string.Join(", ", cv.Companies)}");
					Console.WriteLine($"Start Date: {cv.StartDate.ToString("yyyy-MM-dd")}");
					Console.WriteLine($"End Date: {cv.EndDate.ToString("yyyy-MM-dd")}");
					Console.WriteLine("Languages:");
					foreach (var language in cv.Languages)
					{
						Console.WriteLine($"  {language.Key}: {language.Value}");
					}
					Console.WriteLine($"Has Degree: {cv.HasDegree}");
					Console.WriteLine();
					index++;
				}
			}

			Console.WriteLine("Press any key to continue...");
			Console.ReadKey();
		}

		static void START()
		{

            userData.Workers = JsonDataManager.LoadWorkerData();
            userData.Employers = JsonDataManager.LoadEmployerData();

			while (true)
			{

				Console.Clear();
				Console.WriteLine($@"
									
 /$$$$$$$                                      /$$$$$$           
| $$__  $$                                    /$$__  $$          
| $$  \ $$  /$$$$$$   /$$$$$$$ /$$$$$$$      | $$  \ $$ /$$$$$$$$
| $$$$$$$  /$$__  $$ /$$_____//$$_____/      | $$$$$$$$|____ /$$/
| $$__  $$| $$  \ $$|  $$$$$$|  $$$$$$       | $$__  $$   /$$$$/ 
| $$  \ $$| $$  | $$ \____  $$\____  $$      | $$  | $$  /$$__/  
| $$$$$$$/|  $$$$$$/ /$$$$$$$//$$$$$$$/      | $$  | $$ /$$$$$$$$
|_______/  \______/ |_______/|_______/       |__/  |__/|________/
																 
																 
																 



");

				for (int i = 0; i < totalOptions; i++)
				{
					if (i == selectedIndex)
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.Write("=> ");
					}


					switch (i)
					{
						case 0:
							Console.WriteLine("\t\t\t\t\t1. Login ");
							break;
						case 1:
							Console.WriteLine("\t\t\t\t\t2. Register ");
							break;
						case 2:
							Console.WriteLine("\t\t\t\t\t3. Exit");
							break;
					}


					Console.ForegroundColor = ConsoleColor.White;
				}

				ConsoleKeyInfo keyInfo = Console.ReadKey();


				if (keyInfo.Key == ConsoleKey.UpArrow)
				{
					selectedIndex = (selectedIndex - 1 + totalOptions) % totalOptions;
				}

				else if (keyInfo.Key == ConsoleKey.DownArrow)
				{
					selectedIndex = (selectedIndex + 1) % totalOptions;
				}

				else if (keyInfo.Key == ConsoleKey.Enter)
				{
					int choice = selectedIndex + 1;
					switch (choice)
					{
						case 1:
							LoginMenu();
							break;
						case 2:
							RegisterMenu();
							break;
						case 3:
							Console.Clear();
							Console.ForegroundColor= ConsoleColor.Red;
                            Console.WriteLine(@"

 ██████╗  ██████╗  ██████╗ ██████╗ ██████╗ ██╗   ██╗███████╗
██╔════╝ ██╔═══██╗██╔═══██╗██╔══██╗██╔══██╗╚██╗ ██╔╝██╔════╝
██║  ███╗██║   ██║██║   ██║██║  ██║██████╔╝ ╚████╔╝ █████╗  
██║   ██║██║   ██║██║   ██║██║  ██║██╔══██╗  ╚██╔╝  ██╔══╝  
╚██████╔╝╚██████╔╝╚██████╔╝██████╔╝██████╔╝   ██║   ███████╗
 ╚═════╝  ╚═════╝  ╚═════╝ ╚═════╝ ╚═════╝    ╚═╝   ╚══════╝
 

");
                            Environment.Exit(0);
							break;

					}
				}

			}

		}
		static void Main()
		{
			START();
		}
	}
}