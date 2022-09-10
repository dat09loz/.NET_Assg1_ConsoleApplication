using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Net.Mail;

namespace Assg1_ConsoleApplication
{
	public class BankAccount
	{
		private readonly int id;
		private string fname, lname, email, address, phone;
		private double balance;
		private List<Transaction> transactions;

		//identify new vs existing account
		public enum AccType
		{
			New,
			Existing
		}

		public BankAccount(int id, string fname, string lname, string email, string address, string phone, double balance)
		{
			this.id = id;
			this.fname = fname;
			this.lname = lname;
			this.email = email;
			this.address = address;
			this.phone = phone;
			this.balance = balance;
			transactions = new List<Transaction>();
		}

		public void OutputAccDetails()
        {
			Console.WriteLine("     __________________________________________________");
			Console.WriteLine("    |                                                  |");
			Console.WriteLine("    |                  ACCOUNT DETAILS                 |");
			Console.WriteLine("    |                                                  |");
			Console.WriteLine("    |__________________________________________________|");
			Console.WriteLine("    |                                                  |");
			Console.WriteLine("    |     Account Number:                                             |");
			Console.WriteLine("    |     Account Balance:                                   |");
			Console.WriteLine("    |     Last Name:                                  |");
			Console.WriteLine("    |     Email:                                             |");
			Console.WriteLine("    |     Address:                                             |");
			Console.WriteLine("    |     Phone:                                             |");
			Console.WriteLine("    |                                                  |");
			Console.WriteLine("    |__________________________________________________|");
		}
	}
}
