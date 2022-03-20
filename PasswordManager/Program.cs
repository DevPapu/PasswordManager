using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PasswordManager
{
    class Program
    {
        static Random rnd = new Random();
        static string name;
        static string start;
        static string masterpassword;
        static string loginpassword;
        static string createdpassword;
        static string quit;
        static string option;
        static string password;
        static string app;
        static bool l;
        static int c;
        static string input;
        static string confirmation;
        static MySQLconnection connection = new MySQLconnection();
        static List<string> passwords = new List<string>();
        static Dictionary<string, string> kvpPasswords = new Dictionary<string, string>();
        static string[] passSplit;
        static async Task Main(string[] args)
        {
            Start();
            Console.ReadKey();
        }
        public static void Start()
        {
            
            l = true;
            while (l)
            {
                Console.Clear();
                Console.WriteLine("Please create a master password or log in!");
                Console.WriteLine("Press 1 to create a new master password, 2 to log in, 3 to change your master password and 4 to delete a base!");
                Console.WriteLine("--------------------------------------------------------------------------------------------------------------");
                start = Console.ReadLine();
                if (start == "1")
                {
                    l = false;
                    CreateMasterPassword();
                }
                else if (start == "2")
                {
                    l = false;
                    Login();
                    Menu();
                }
                else if (start == "3")
                {
                    l = false;
                    ChangeMasterPassword();
                }
                else if (start == "4")
                {
                    l = false;
                    DeleteBase();
                }
                else
                {
                    Console.WriteLine("Failed to identify your submission! Please enter again!");
                    Thread.Sleep(2000);
                }
            }
        }
        public static void ChangeMasterPassword()
        {
            Login();
            Console.WriteLine("Enter your new master password!");
            masterpassword = Console.ReadLine();
            Console.WriteLine("Press c to confirm your submission and s to stop the process!");
            confirmation = Console.ReadLine();
            if (confirmation == "s")
            {
                Start();
            }
            else if (confirmation == "c")
            {
                try
                {
                    File.WriteAllText(Environment.CurrentDirectory + @"\zz" + name + "Master.txt", masterpassword);
                    Console.WriteLine("Password stored!");
                    Thread.Sleep(2000);
                    Menu();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Failed to identify your submission! Please enter again!");
                Thread.Sleep(2000);
                ChangeMasterPassword();
            }
        }
        public static void DeleteBase()
        {
            Login();
            Console.WriteLine("Are you sure you want to delete this base?. Type \"yes\" to delete. !!!ALL OF YOUR DATA WILL BE DELETED!!!");
            input = Console.ReadLine();
            if (input == "yes" || input == "Yes" || input == "YES")
            {
                Console.WriteLine("Press c to confirm your submission and s to stop the process!");
                confirmation = Console.ReadLine();
                if (confirmation == "s")
                {
                    Start();
                }
                else if (confirmation == "c")
                {
                    try
                    {
                        File.Delete(Environment.CurrentDirectory + @"\zz" + name + "Master.txt");
                        File.Delete(Environment.CurrentDirectory + @"\zz" + name + "Passwords.txt");
                        Console.WriteLine("Base deleted!");
                        Thread.Sleep(2000);
                        Start();
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message);
                    }
                    
                }
                else
                {
                    Console.WriteLine("Failed to identify your submission! Please enter again!");
                    Thread.Sleep(2000);
                    DeleteBase();
                }
            }
            else if (input == "no" || input == "NO" || input == "No")
            {
                Start();
            }
            else
            {
                Console.WriteLine("Failed to identify your submission! Please enter again!");
                Thread.Sleep(2000);
                DeleteBase();
            }
        }
        public static void CreateMasterPassword()
        {
            Console.Clear();
            Console.WriteLine("Choose a name for your base!");
            name = Console.ReadLine();
            Console.WriteLine("Press c to confirm your submission and s to stop the process!");
            confirmation = Console.ReadLine();
            Console.Clear();
            if (confirmation == "s")
            {
                Start();
            }
            else if (confirmation == "c")
            {
                try
                {
                    using (FileStream fs = File.Create(Environment.CurrentDirectory + @"\zz" + name + "Master.txt"))
                    Console.WriteLine("Base created!");
                    Thread.Sleep(2000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Failed to identify your submission! Please enter again!");
                Thread.Sleep(2000);
                Start();
            }
            Console.Clear();
            Console.WriteLine("Please enter your new master password!");
            masterpassword = Console.ReadLine();
            Console.WriteLine("Press c to confirm your submission and s to stop the process!");
            confirmation = Console.ReadLine();
            if (confirmation == "s")
            {
                masterpassword = "";
                Start();
            }
            else if (confirmation == "c")
            {
                try
                {
                    File.WriteAllText(Environment.CurrentDirectory + @"\zz" + name + "Master.txt", masterpassword);
                    Console.WriteLine("Password stored!");
                    Thread.Sleep(2000);
                    Menu();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Failed to identify your submission! Please enter again!");
                Thread.Sleep(2000);
                Start();
            }
        }
          
        public static void Login()
        {
            Console.Clear();
            Console.WriteLine("What is the name of your base?");
            name = Console.ReadLine();
            Console.WriteLine("Press c to confirm your submission and s to stop the process!");
            confirmation = Console.ReadLine();
            Console.Clear();
            if (confirmation == "s")
            {
                name = "";
                Start();
            }
            else if (confirmation == "c")
            {
                if (!File.Exists(Environment.CurrentDirectory + @"\zz" + name + "Master.txt"))
                {
                    Console.WriteLine("Base does not exist! Check your spelling!");
                    Thread.Sleep(2000);
                    Start();
                }
            }
            else
            {
                Console.WriteLine("Failed to identify your submission! Please enter again!");
                Start();
            }
            Console.WriteLine("Enter your master password to log in!");
            loginpassword = Console.ReadLine();
            Console.WriteLine("Press c to confirm your submission and s to stop the process!");
            confirmation = Console.ReadLine();
            if (confirmation == "s")
            {
                loginpassword = "";
                Start();
            }
            else if (confirmation == "c")
            {

                masterpassword = File.ReadAllText(Environment.CurrentDirectory + @"\zz" + name + "Master.txt");
                if (masterpassword == loginpassword)
                {
                    Console.WriteLine("Success!");
                }
                else
                {
                    Console.WriteLine("Wrong password!");
                    Thread.Sleep(2000);
                    Start();
                }
            }
            else
            {
                Console.WriteLine("Failed to identify your submission! Please enter again!");
                Start();
            }
            Console.WriteLine("----------------------------------------------------------------");
        }
        public static async Task<string> RndPassword()
        {
            char[] symbols = "abcdefghijklmnopqrstuvwxyz1234567890_-§$%&/!?*#~ABCDEFGHIJKLMNOPQRSTUVWXYZ=".ToCharArray();
            string password = "";

            for (int i = 0; i < 20; i++)
            {
                password += symbols[rnd.Next(1, 66)].ToString();
            }
            return password;
        }
        public static void  Menu()
        {
            l = true;
            while (l)
            {
                Console.Clear();
                Console.WriteLine("Choose one option: " + "\n" +
                "a) to create a new password press a" + "\n" +
                "b) to check your passwords press b" + "\n" +
                "c) to change a password press c" + "\n" +
                "----------------------------------------------------------------");
                option = Console.ReadLine();
                if (option == "a")
                {
                    Console.Clear();
                    createPasswords();
                }
                else if (option == "b")
                {
                    Console.Clear();
                    showPasswords();
                    Console.ReadKey();
                }
                else if (option == "c")
                {
                    Console.Clear();
                    Overview();
                }
                else
                {
                    Console.WriteLine("Failed to identify your submission! Please enter again!");
                }
            }
            
        }
        public static void Overview()
        {
            while (l)
            {
                Console.Clear();
                Console.WriteLine("Here you can see all your passwords! ");
                Console.WriteLine("Press d to delte a password and c to change it!");
                Console.WriteLine("If you want to quit press q!" + "\n");
                showPasswords();


                input = Console.ReadLine();
                if (input == "d")
                {
                    DeletePassword();
                }
                else if (input == "c")
                {
                    UpdatePasswords();
                }
                else if (input == "q")
                {
                    l = false;
                    break;
                }
                else
                {
                    Console.WriteLine("Failed to identify your submission! Please enter again!");
                }
            }
           
        }

        private static void UpdatePasswords()
        {
            Console.Clear();
            showPasswords();
            Console.WriteLine("Which password do you want to change? Please enter the app!");
            app = Console.ReadLine();
            Console.WriteLine("Please enter your new password!");
            password = Console.ReadLine();
            Console.WriteLine("Press c to confirm your submission and s to stop the process!");
            confirmation = Console.ReadLine();
            if (confirmation == "s")
            {
                Overview();
            }
            else if (confirmation == "c")
            {
                try
                {
                    kvpPasswords.Clear();
                    passwords = File.ReadAllLines(Environment.CurrentDirectory + @"\zz" + name + "Passwords.txt").ToList();
                    foreach (string i in passwords)
                    {
                        passSplit = i.Split(':');
                        kvpPasswords.Add(passSplit[0], passSplit[1]);
                        Array.Clear(passSplit, 0, passSplit.Length);
                    }
                    if (kvpPasswords.ContainsKey(app))
                    {
                        kvpPasswords.Remove(app);
                        kvpPasswords.Add(app, password);
                        File.WriteAllText(Environment.CurrentDirectory + @"\zz" + name + "Passwords.txt", "");
                        foreach (var pair in kvpPasswords)
                        {
                            File.AppendAllText(Environment.CurrentDirectory + @"\zz" + name + "Passwords.txt", pair.Key + ":" + pair.Value + "\n");
                        }
                        Console.WriteLine("Password changed!");
                        Thread.Sleep(2000);
                        Overview();
                    }
                    else
                    {
                        Console.WriteLine("App was not found! Check your spelling!");
                        Thread.Sleep(2000);
                        Console.Clear();
                        UpdatePasswords();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Thread.Sleep(10000);
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Failed to identify your submission! Please enter again!");
                Thread.Sleep(2000);
                Overview();
            }
      
        }

        private static void DeletePassword()
        {
            Console.Clear();
            showPasswords();
            Console.WriteLine("Which password do you want to delte? Please enter the app!");
            app = Console.ReadLine();
            Console.WriteLine("Press c to confirm your submission and s to stop the process!");
            confirmation = Console.ReadLine();
            if (confirmation == "s")
            {
                Overview();
            }
            else if (confirmation == "c")
            {
                try
                {
                    kvpPasswords.Clear();
                    passwords = File.ReadAllLines(Environment.CurrentDirectory + @"\zz" + name + "Passwords.txt").ToList();
                    foreach (string i in passwords)
                    {
                        passSplit = i.Split(':');
                        kvpPasswords.Add(passSplit[0], passSplit[1]);
                        Array.Clear(passSplit, 0, passSplit.Length);
                    }
                    if (kvpPasswords.ContainsKey(app))
                    {
                        kvpPasswords.Remove(app);
                        File.WriteAllText(Environment.CurrentDirectory + @"\zz" + name + "Passwords.txt", "");
                        foreach (var pair in kvpPasswords)
                        {
                            File.AppendAllText(Environment.CurrentDirectory + @"\zz" + name + "Passwords.txt", pair.Key + ":" + pair.Value + "\n");
                        }
                        Console.WriteLine("Password deleted!");
                        Thread.Sleep(2000);
                        Overview();
                    }
                    else
                    {
                        Console.WriteLine("App was not found! Check your spelling!");
                        Thread.Sleep(2000);
                        Console.Clear();
                        UpdatePasswords();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Thread.Sleep(10000);
                }
            }
        }
        public static void createPasswords()
        {
            Console.WriteLine("To create a random password press a! To create your own password press b!");
            input = Console.ReadLine();
            if (input == "a")
            {
                Console.Clear();
                Console.WriteLine("Please enter your app: ");
                app = Console.ReadLine();
                password = RndPassword().Result;
                try
                {
                    if(!File.Exists(Environment.CurrentDirectory + @"\zz" + name + "Passwords.txt"))
                    {
                        File.WriteAllText(Environment.CurrentDirectory + @"\zz" + name + "Passwords.txt", app + ":" + password + "\n");
                    }
                    else
                    {
                        File.AppendAllText(Environment.CurrentDirectory + @"\zz" + name + "Passwords.txt", app + ":" + password + "\n");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.WriteLine("Your password is: " + password);
                Thread.Sleep(2000);
            }
            else if (input == "b")
            {
                Console.Clear();
                Console.WriteLine("Please enter your app: ");
                app = Console.ReadLine();
                Console.WriteLine("Please enter your password: ");
                password = Console.ReadLine();
                Console.WriteLine("Press c to confirm your submission and s to stop the process!");
                confirmation = Console.ReadLine();
                if (confirmation == "s")
                {
                    password = "";
                    app = "";
                    Menu();
                }
                else if (confirmation == "c")
                {
                    try
                    {
                        if (!File.Exists(Environment.CurrentDirectory + @"\zz" + name + "Passwords.txt"))
                        {
                            File.WriteAllText(Environment.CurrentDirectory + @"\zz" + name + "Passwords.txt", app + ":" + password + "\n");
                        }
                        else
                        {
                            File.AppendAllText(Environment.CurrentDirectory + @"\zz" + name + "Passwords.txt", app + ":" + password + "\n");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Failed to identify your submission! Please enter again!");
                    createPasswords();
                }
            }
            else
            {
                Console.WriteLine("Failed to identify your submission! Please enter again!");
                createPasswords();
            }
           
            
        }
        public static void showPasswords()
        {
            
            try
            {
                Console.WriteLine("Your passwords! ");
                passwords = File.ReadAllLines(Environment.CurrentDirectory + @"\zz" + name + "Passwords.txt").ToList();
                foreach (string i in passwords)
                {
                    Console.WriteLine(i);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
