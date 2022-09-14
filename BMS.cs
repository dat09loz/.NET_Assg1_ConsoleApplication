using System;
// using System.Collections;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace Assg1_ConsoleApplication
{
    public class BMS
    {
        private List<BankAccount> accounts;

        public BMS()
        {
            accounts = new List<BankAccount>();
        }

        // this enum will handle menu option input (if the user press option 4, withdraw method will be called etc.)
        public enum MenuOptions 
        {
            Login,                  //0
            CreateAccount,          //1
            SearchAccount,          //2
            Deposit,                //3 
            Withdraw,               //4
            ACStatement1,           //5
            ACStatement2,           //6
            DeleteAccount1,         //7
            DeleteAccount2,         //8
        }

        //main class
        static void Main(string[] args)
        {
            ConsoleKey choice = 0; //enum, simplify user input handling
            new BMS().Login(ref choice); //MenuOptions.LoginMenu
        }

        //login

        //handle user choices (yes/no)
        private void ReadChoice(ref ConsoleKey c1, ref ConsoleKey c2, MenuOptions op, int AccId = 0)
        {
            /* logic behind 2 consolekey variables:
             * - c1: for createAccount method. If yes, create a new account (confirm the inputted data is correct). If no, call the method again (redo the process)
             * - c2: for all other methods. If yes, evaluate the MenuOptions (keep the program running). If no, not evaluate the MenuOptions (exit the program/function)
            */
            //while c2 == ConsoleKey.Y, evaluate menu options
            do
            {
                c2 = Console.ReadKey().Key; //capture key pressed
                switch (c2)
                {
                    case ConsoleKey.Y:

                        switch (op)
                        {
                            case MenuOptions.Login:
                                Login(ref c2); //Y
                                break;
                            case MenuOptions.CreateAccount:
                                CreateAccount(ref c1, ref c2);
                                break;
                            case MenuOptions.SearchAccount:
                                SearchAccount(ref c2);
                                break;
                            case MenuOptions.Deposit:
                                Deposit(ref c2);
                                break;
                            case MenuOptions.Withdraw:
                                Withdraw(ref c2);
                                break;
                            default:
                                break;
                        }
                        break;
                    default: //user pressed the wrong key, reset input
                        Console.Write("\b \b");
                        break;
                }
            } while (c2 != ConsoleKey.N);
        }

        //end handle user input

        //choice methods (0-9)

        //login
        private void Login(ref ConsoleKey c2)
        {
            //clear the console, start the application
            //print out the menu view (UI)
            Console.Clear(); 
            OutputLogin();

            //reposition cursor and read user input
            Console.SetCursorPosition(20, 8);
            string username = Console.ReadLine();
            Console.SetCursorPosition(20, 9);
            string password = "";

            //password string masking
            ConsoleKeyInfo keyInfo;
            ConsoleKey k;
            do
            {
                keyInfo = Console.ReadKey(true);
                k = keyInfo.Key;
                if (k == ConsoleKey.Backspace && password.Length > 0)
                {
                    Console.Write("\b \b");
                    password = password[0..^1];
                }
                if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    password += keyInfo.KeyChar;
                }
            } while (k != ConsoleKey.Enter);

            //login validation
            Console.SetCursorPosition(4, 13);
            if (File.Exists("login.txt")) //login file exists -> validate credentials
            {
                bool success = false;
                string[] credentials = File.ReadAllLines("login.txt");
                // validate login.txt format (username | password)
                try
                {
                    foreach (string credential in credentials)
                    {
                        //extract credential from "|"
                        string[] login = credential.Split("|", StringSplitOptions.RemoveEmptyEntries); //remove empty string from login array
                        // {username, password} login.txt
                        if (username == login[0] && password == login[1])
                        {
                            success = true;
                            c2 = ConsoleKey.N;
                            Console.Write("Login Successful. Please wait...");
                            System.Threading.Thread.Sleep(1000);
                            Menu();
                            break;
                        }
                    }
                    if (!success) //if login unsuccessful
                    {
                        Console.Write(" Incorrect Credential. Try Again? (y/n) ");
                        ReadChoice(ref c2, ref c2, MenuOptions.Login);
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    c2 = ConsoleKey.N; //end program
                    Console.Write("Error: Login credential in invalid format. Press any key to exit...");
                    Console.ReadKey();
                }
            }
            else //login.txt does not exist
            {
                c2 = ConsoleKey.N;
                Console.Write("Error: login.txt not found. Press any key to exit...");
                Console.ReadKey();
            }  
        }



        //print login menu to the console
        private void OutputLogin()
        {
            Console.WriteLine("     ______________________________________________________________");
            Console.WriteLine("    |                                                              |");
            Console.WriteLine("    |                WELCOME TO SIMPLE BANKING SYSTEM              |");
            Console.WriteLine("    |                                                              |");
            Console.WriteLine("    |______________________________________________________________|");
            Console.WriteLine("    |                                                              |");
            Console.WriteLine("    |                            SIGN IN                           |");
            Console.WriteLine("    |                                                              |");
            Console.WriteLine("    |     Username:                                                |");
            Console.WriteLine("    |     Password:                                                |");
            Console.WriteLine("    |                                                              |");
            Console.WriteLine("    |______________________________________________________________|");
        }
        
        //end login

        //main menu
        
        //handle memu choices from the user
        private void Menu()
        {
            //record user choices from the keyboard, then call appropriate methods
            ConsoleKey c, c1, c2; // c: choice handles by this method; c1 & c2 handled by ReadChoice() invoked within other methods
            c = c1 = c2 = 0;
            bool invalidInput = false; // input validation

            while (c != ConsoleKey.D7)
            {
                Console.Clear();
                OutputMenu();
                if(invalidInput)
                {
                    Console.Write("Enter valid choice number (1-7)");
                }
                invalidInput = false; //reset
                Console.SetCursorPosition(35, 15);
                c = Console.ReadKey().Key;
                switch (c)
                {
                    case ConsoleKey.D1: case ConsoleKey.NumPad1: //1
                        CreateAccount(ref c1, ref c2);
                        break;
                    case ConsoleKey.D2: case ConsoleKey.NumPad2: //2
                        SearchAccount(ref c2);
                        break;
                    case ConsoleKey.D3: case ConsoleKey.NumPad3: //3
                        Deposit(ref c2);
                        break;
                    case ConsoleKey.D4: case ConsoleKey.NumPad4: //4
                        Withdraw(ref c2);
                        break;
                    case ConsoleKey.D5: case ConsoleKey.NumPad5: //5
                        //AACStatement(ref c2);
                        break;
                    case ConsoleKey.D6: case ConsoleKey.NumPad6: //6
                        //DeleteAccount(ref c2);
                        break;
                    default:
                        invalidInput = true;
                        break;
                }
            }
        }

        //print main menu to the console
        private void OutputMenu()
        {
            Console.WriteLine("     _______________________________________________________");
            Console.WriteLine("    |                                                       |");
            Console.WriteLine("    |            WELCOME TO SIMPLE BANKING SYSTEM           |");
            Console.WriteLine("    |                                                       |");
            Console.WriteLine("    |_______________________________________________________|");
            Console.WriteLine("    |                                                       |");
            Console.WriteLine("    |     1. Create a new account                           |");
            Console.WriteLine("    |     2. Search for an account                          |");
            Console.WriteLine("    |     3. Deposit                                        |");
            Console.WriteLine("    |     4. Withdraw                                       |");
            Console.WriteLine("    |     5. A/C statement                                  |");
            Console.WriteLine("    |     6. Delete account                                 |");
            Console.WriteLine("    |     7. Exit                                           |");
            Console.WriteLine("    |_______________________________________________________|");
            Console.WriteLine("    |                                                       |");
            Console.WriteLine("    |     Enter your choice (1-7):                          |");
            Console.WriteLine("    |_______________________________________________________|");
            Console.WriteLine();
            Console.WriteLine(); //error output line
        }

        //end main menu

        //application functionalities

        //create a new account (controller)
        private void CreateAccount(ref ConsoleKey c1, ref ConsoleKey c2)
        {
            Console.Clear();
            OutputCreateAccountForm();
            // read user input
            Console.SetCursorPosition(21, 8);
            string fname = Console.ReadLine().Trim(' ');
            Console.SetCursorPosition(20, 9);
            string lname = Console.ReadLine().Trim(' ');
            Console.SetCursorPosition(18, 10);
            string address = Console.ReadLine().Trim(' ');
            Console.SetCursorPosition(16, 11);
            string phone = Console.ReadLine().Trim(' ');
            Console.SetCursorPosition(16, 12);
            string email = Console.ReadLine().Trim(' ');
            Console.SetCursorPosition(4, 17);
            Console.Write("Is the information correct? (y/n) ");
            do
            {
                c1 = Console.ReadKey().Key; //read user confirmation choice
                switch (c1)
                {
                    case ConsoleKey.Y: //if the info is correct
                        Console.WriteLine();
                        if (Int64.TryParse(phone, out long phoneOut) && phone.Length == 10) //if phone is int convertable + 10 characters
                        {
                            int id;
                            do //keep regenerating account id until it's unique
                            {
                                id = new Random().Next(100000, 99999999); //random id generator
                            } while (File.Exists($"{id}.txt"));

                            //create a new bankAccount object (int id, string fname, string lname, string email, string address, string phone, double balance)
                            BankAccount bankAccount = new BankAccount(id, fname, lname, email, address, phone, 0.0);

                            if (bankAccount.SendEmail("AccInfo")) //option to send account info
                            {
                                // if send successfully:
                                Console.WriteLine("    Account Created! Details will be provided via email.");
                                Console.WriteLine($"    Account number is: {id}");
                                accounts.Add(bankAccount);
                                bankAccount.UpdateFile(); //create a new {id}.txt file
                                Console.Write("Returning to menu... ");
                                System.Threading.Thread.Sleep(1000);
                            }
                            else //if sendEmail unsuccessful => invalid/incorrect email address
                            {
                                accounts.RemoveAt(accounts.Count - 1); //remove newly added account in accounts list
                                Console.Write("    Invalid/Incorrect email adddress. Retry? (y/n) ");
                                ReadChoice(ref c1, ref c2, MenuOptions.CreateAccount);
                            }
                        }
                        else //if not a valid phone number
                        {
                            Console.Write("    Invalid phone number. Retry? (y/n)");
                            ReadChoice(ref c1, ref c2, MenuOptions.CreateAccount);
                        }
                        break;
                    case ConsoleKey.N: //if the info is incorrect
                        CreateAccount(ref c1, ref c2);
                        break;
                    default: //invalid input
                        Console.Write("\b \b");
                        break;
                }
            } while (c1 != ConsoleKey.Y && c2 != ConsoleKey.N);

        }

        //create a new account (view)
        private void OutputCreateAccountForm()
        {
            Console.WriteLine("     ______________________________________________________________");
            Console.WriteLine("    |                                                              |");
            Console.WriteLine("    |                      CREATE A NEW ACCOUNT                    |");
            Console.WriteLine("    |                                                              |");
            Console.WriteLine("    |______________________________________________________________|");
            Console.WriteLine("    |                                                              |");
            Console.WriteLine("    |                       ENTER THE DETAILS                      |");
            Console.WriteLine("    |                                                              |");
            Console.WriteLine("    |     First Name:                                              |");
            Console.WriteLine("    |     Last Name:                                               |");
            Console.WriteLine("    |     Address:                                                 |");
            Console.WriteLine("    |     Phone:                                                   |");
            Console.WriteLine("    |     Email:                                                   |");
            Console.WriteLine("    |                                                              |");
            Console.WriteLine("    |______________________________________________________________|");
            Console.WriteLine();
        }

        //search accounts
        public void SearchAccount(ref ConsoleKey c)
        {
            Console.Clear();
            OutputOptionMenu(MenuOptions.SearchAccount); //output option menu for search an account

            Console.SetCursorPosition(26, 8);
            string id = Console.ReadLine();
            Console.SetCursorPosition(4, 11);

            if (AccountExists(id))
            {
                Account(Convert.ToInt32(id)).OutputAccDetails();
                Console.Write("Press any key to return to menu.");
                Console.ReadKey();
                Console.WriteLine();
                Console.Write(    "Returning to menu... ");
                System.Threading.Thread.Sleep(1000);
                Menu();
            }
            Console.WriteLine("Account not found!");
            Console.Write("    Check another account? (y/n) ");
            ReadChoice(ref c, ref c, MenuOptions.SearchAccount);
        }

        //check if account exists in the system
        public bool AccountExists(string idStr)
        {
            //check if id valid, valid length and .txt file exists
            bool validId = Int32.TryParse(idStr, out int id);
            bool validIdLen = idStr.Length >= 6 && idStr.Length <= 8;
            bool fileExists = File.Exists($"{id}.txt");

            if (validId && validIdLen && fileExists)
            {
                return true;
            }

            return false;
        }

        //return account by id, if not found return nothing
        public BankAccount Account(int id)
        {
            foreach (BankAccount acc in accounts)
            {
                if (acc.HasId(id))
                {
                    return acc;
                }
            }
            return null;
        }

        //deposit
        public void Deposit(ref ConsoleKey c)
        {
            Console.Clear();
            OutputOptionMenu(MenuOptions.Deposit);// output optionn menu for deposit

            Console.SetCursorPosition(26, 8);
            string idStr = Console.ReadLine();

            if (AccountExists(idStr)) //check  if account exist before enter amount
            {
                Console.SetCursorPosition(4, 11);
                Console.WriteLine("Account found! Enter the amount...");
                Console.SetCursorPosition(20, 9);
                string amountStr = Console.ReadLine();
                

                if (Double.TryParse(amountStr, out double amount)) //if the entered amount is valid
                {
                    Account(Convert.ToInt32(idStr)).Deposit(amount); //deposit to the specified account
                    Console.SetCursorPosition(4, 12);
                    Console.WriteLine("Deposit Successfull!");
                    Console.Write("    Press any key to return to menu.");
                    Console.ReadKey();
                    Console.WriteLine();
                    Console.WriteLine("    Returning to menu...");
                    System.Threading.Thread.Sleep(1000);
                    Menu();
                }
                else //amount not valid
                {
                    Console.SetCursorPosition(4, 12);
                    Console.Write("Amount not valid! Retry? (y/n) ");
                    ReadChoice(ref c, ref c, MenuOptions.Deposit);
                }
            }
            else //if not, reset table
            {
                Console.SetCursorPosition(4, 11);
                Console.WriteLine("Account Not Found!");
                Console.Write("    Retry? (y/n) ");
                ReadChoice(ref c, ref c, MenuOptions.Deposit);
            }
        }

        //withdraw
        private void Withdraw(ref ConsoleKey c)
        {


        }

        //dymanic menu to handle different options
        public void OutputOptionMenu(MenuOptions op)
        {
            Console.WriteLine("     ________________________________________________________");
            Console.WriteLine("    |                                                        |");
            if (op == MenuOptions.SearchAccount)
            Console.WriteLine("    |                    SEARCH AN ACCOUNT                   |");
            if (op == MenuOptions.Deposit)
            Console.WriteLine("    |                         DEPOSIT                        |");

            Console.WriteLine("    |                                                        |");
            Console.WriteLine("    |________________________________________________________|");
            Console.WriteLine("    |                                                        |");
            Console.WriteLine("    |                   ENTER THE DETAILS                    |");
            Console.WriteLine("    |                                                        |");
            Console.WriteLine("    |     Account Number:                                    |");
            if (op == MenuOptions.Deposit && op == MenuOptions.Withdraw)
            Console.WriteLine("    |     Amount: $                                          |");
            Console.WriteLine("    |________________________________________________________|");
        }
    }
}
