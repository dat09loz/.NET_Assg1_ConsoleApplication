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
            new BMS().LoginMenu(ref choice); //MenuOptions.LoginMenu
        }

        //login

        //handle user choices (yes/no)
        private void ReadChoice(ref ConsoleKey c1, ref ConsoleKey c2, MenuOptions op, int AccId = 0)
        {
            /* logic behind 2 consolekey variables:
             * - c1: for createAccount method. If yes, create a new account (confirm the inputted data is correct). If no, call the method again (redo the process)
             * - c2: for all other methods. If yes, evaluate the MenuOptions (keep the program running). If no, not evaluate the MenuOptions (exit the program)
            */
        }

        //end handle user input

        //choice methods (0-9)

        //login
        private void LoginMenu(ref ConsoleKey choice) //c2
        {
            //clear the console, start the application
            //print out the menu view (UI)
            Console.Clear(); 
            OutputLoginMenu();

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
            if(File.Exists("login.txt")) //login file exists -> validate credentials
            {
                bool success = false;
                string[] credentials = File.ReadAllLines("login.txt");
                // validate login.txt format (username | password)
                try
                {
                    foreach (string credential in credentials)
                    {
                        //separate credential from "|" and space
                        string[] separator = { "|", " " };
                        string[] login = credential.Split(separator, StringSplitOptions.RemoveEmptyEntries); //remove empty string from login array
                        // {username, password} login.txt
                        if (username == login[0] && password == login[1])
                        {
                            success = true;
                            choice = ConsoleKey.N; //c2 == N
                            Console.Write("Login Successful");
                            break;
                        }
                    }
                    if (!success) //if login unsuccessful
                    {
                        Console.Write("   Incorrect Credential. Try Again? (y/n) ");
                        ReadChoice(ref choice, ref choice, MenuOptions.Login);
                    }
                } 
                catch
                {

                }
            }
        }



        //print login menu to the console
        private void OutputLoginMenu()
        {
            Console.WriteLine("     ______________________________________________________________");
            Console.WriteLine("    |                                                              |");
            Console.WriteLine("    |           WELCOME TO SIMPLE BANK MANAGEMENT SYSTEM           |");
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














        //end main menu
    }
}
