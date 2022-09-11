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
            SimpleBankingSystem,
            CreateAccount,
            SearchAccount,
            Deposit,
            Withdraw,
            ACStatement,
            DeleteAccount,
            Exit
        }

        //main class
        static void Main(string[] args)
        {
            ConsoleKey choice = 0; //enum, simplify user input handling
            new BMS().LoginMenu(ref choice);
        }

        //handle login activities
        private void LoginMenu(ref ConsoleKey choice)
        {
            //clear the console, start the application
            //print out the menu view (UI)
            Console.Clear(); 
            OutputLoginMenu();

            //reposition cursor and read user input

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
            Console.WriteLine("    |     Username:                                                |");
            Console.WriteLine("    |     Password:                                                |");
            Console.WriteLine("    |                                                              |");
            Console.WriteLine("    |______________________________________________________________|");
        }
        
    }
}
