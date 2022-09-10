using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;

namespace Assg1_ConsoleApplication
{
	public class BankAccount
	{
		private readonly int id;
		private string fname, lname, email, address, phone;
		private double balance;
		private List<Transaction> transactions;

		//identify new vs existing account
		

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
			// DateTime time, double credit, double debit, double balance, string desc
			transactions.Add(new Transaction(DateTime.Now, 0.0, 0.0, balance, "Account Created."));
		}


		//console output
		public void OutputAccDetails()
        {
			Console.WriteLine("     __________________________________________________");
			Console.WriteLine("    |                                                  |");
			Console.WriteLine("    |                  ACCOUNT DETAILS                 |");
			Console.WriteLine("    |                                                  |");
			Console.WriteLine("    |__________________________________________________|");
			Console.WriteLine("    |                                                  |");
			Console.WriteLine("    |     Account Number:                              |");
			Console.WriteLine("    |     Account Balance:                             |");
			Console.WriteLine("    |     First Name:                                  |");
			Console.WriteLine("    |     Last Name:                                   |");
			Console.WriteLine("    |     Address:                                     |");
			Console.WriteLine("    |     Phone:                                       |");
			Console.WriteLine("    |     Email:                                       |");
			Console.WriteLine("    |                                                  |");
			Console.WriteLine("    |__________________________________________________|");
		}

		public void OutputTransactions()
        {
			//added later
        }

		//end console output

		//validators

		//check if the id is valid
		public bool HasId(int id)
        {
			return this.id == id;
        }

		//check if the current balance is sufficient for the transaction
		public bool SufficientFund(double amount)
        {
			return balance >= amount;
        }

		//end validators

		//activities

		//handle deposit activities
		public void Deposit(double amount)
        {
			balance += amount;
        }

		//handle withdraw activities
		public void Withdraw(double amount)
        {
			if(SufficientFund(amount))
            {
				balance -= amount;
            }
        }

		//handle send emails, if send successfully, returns true
		// credentials: dotnetuts13653338@gmail.com, nyufoxdjdxhbofgm
		public bool SendEmail(string option)
        {
			SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //google smtp server on port 587
			client.Credentials = new NetworkCredential("dotnetuts13653338@gmail.com", "nyufoxdjdxhbofgm"); //this email will act as the sender
			client.EnableSsl = true; //encrypt the connection



			new MailAddress("dotnetuts13653338@gmail.com", "Simple Banking");
			return true;
        }

		//end activities

		// update database

		//handle .txt file update
		public void UpdateFile(double amount)
        {

        }

		//handle transaction list update (add new transaction)
		public void UpdateTransList(double amount, string type)
        {

        }

		//handle transaction item update (update 1)
		public void UpdateTransItem(double amount, string type)
        {

        }
	}
}
