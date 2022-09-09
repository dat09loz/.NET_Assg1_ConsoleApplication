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
        static void Main(string[] args)
        {
            new BMS().LoginMenu();
        }

        private void LoginMenu()
        {
            OutputLoginMenu();
        }

        private void OutputLoginMenu()
        {
            Console.WriteLine("___________________________________________________");
            Console.WriteLine("|                                                  |");
            Console.WriteLine("|     WELCOME TO SIMPLE BANK MANAGEMENT SYSTEM     |");
            Console.WriteLine("|                                                  |");
            Console.WriteLine("___________________________________________________");
            Console.WriteLine("|                                                  |");
            Console.WriteLine("|                      SIGN IN                     |");
            Console.WriteLine("|     Username:                                    |");
            Console.WriteLine("|     Password:                                    |");
            Console.WriteLine("|                                                  |");
            Console.WriteLine("___________________________________________________");


        }
    }
}
