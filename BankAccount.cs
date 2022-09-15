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

		//output account details form to the console
		public void OutputAccDetails()
        { //insert each of the account properties into the console form with the appropriate padding
			Console.WriteLine("     ______________________________________________________________");
			Console.WriteLine("    |                                                              |");
			Console.WriteLine("    |                        ACCOUNT DETAILS                       |");
			Console.WriteLine("    |                                                              |");
			Console.WriteLine("    |______________________________________________________________|");
			Console.WriteLine("    |                                                              |");
			Console.WriteLine($"    |     Account Number: {id}".PadRight(41, ' ') + "|");
			Console.WriteLine($"    |     Account Balance: {balance}".PadRight(40, ' ') + "|");
			Console.WriteLine($"    |     First Name: {fname}".PadRight(45, ' ') + "|");
			Console.WriteLine($"    |     Last Name: {lname}".PadRight(46, ' ') + "|");
			Console.WriteLine($"    |     Address: {address}".PadRight(48, ' ') + "|");
			Console.WriteLine($"    |     Phone: {phone}".PadRight(50, ' ') + "|");
			Console.WriteLine($"    |     Email: {email}".PadRight(50, ' ') + "|");
			Console.WriteLine("    |                                                              |");
			Console.WriteLine("    |______________________________________________________________|");
		}

		//output transaction list/history to the console (table style)
		public void OutputTransactions()
        {
			Console.WriteLine(// table header
				"   Date&Time".PadRight(25, ' ') + 
				"Credit".PadRight(15, ' ') +
				"Debit".PadRight(15, ' ') +
				"Balance".PadRight(15, ' ') +
				"Description".PadRight(15, ' ')
			);
			Console.WriteLine("   ".PadRight(90, '-')); //table grid
			foreach (Transaction tr in transactions)
            {
				Console.WriteLine(" " + tr); //Transaction.ToString method 
            }
			Console.WriteLine(); //new line
        }

		//end console 

		//activities

		//handle deposit activities
		public void Deposit(double amount)
        {
			balance += amount;
			UpdateTransactionList(amount, "deposit"); //add a deposit transaction entry
			UpdateFile();
        }

		//handle withdraw activities
		public void Withdraw(double amount)
        {
			if (SufficientFund(amount)) // if the balance is sufficient
            {
				balance -= amount;
				UpdateTransactionList(amount, "withdraw"); //add a withdraw transaction entry
				UpdateFile();
            }
        }

		//handle send emails, if send successfully, returns true
		// credentials: dotnetuts13653338@gmail.com, nyufoxdjdxhbofgm
		public bool SendEmail(string option)
        {
			SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //google smtp server on port 587
			client.Credentials = new NetworkCredential("dotnetuts13653338@gmail.com", "nyufoxdjdxhbofgm"); //this email will act as the sender
			client.EnableSsl = true; //encrypt the connection

			try //try to send an email
            {
				MailMessage mail = new MailMessage(
					new MailAddress("dotnetuts13653338@gmail.com", "Simple Banking"), //sender
					new MailAddress(email) //receiver (user's email address)
				);
				mail.IsBodyHtml = true; //use HTML for mail body formatting

				if (option == "AccInfo") //option to send email about account details
                {
					mail.Subject = "Welcome to Simple Banking"; //mail subject
					mail.Body = string.Format(
						$"Dear {fname},<br><br>" +
						"We are glad to have you on board! Here is your account details:<br>" +
						
						$"Account ID: {id}<br>" +
						$"First Name: {fname}<br>" +
						$"Last Name: {lname}<br>" +
						$"Address: {address}<br>" +
						$"Phone: {phone}<br>" +
						$"Email: {email}<br><br>" +

						"Thank you for choosing us as your banking solution.<br><br>" +
						"Regards,<br>" +
						"Simple Banking Co."
					);
                }
				if (option == "Statement") //option to send the account statement
                {
					//convert each transaction item into a string format
					string strBlock = "";
					foreach (Transaction tr in transactions)
                    {
						strBlock += tr.EmailString(); //Transaction.EmailString method
                    }
					mail.Subject = "Your Account Statement";
					mail.Body = string.Format(
						$"Dear, {fname}<br>",
						$"Here is your account statement:<br><br><br>" +

						$"{strBlock}<br><br><br>" +
						$"End of Statement.<br><br>" +

						"Regards,<br>" +
						"Simple Banking Co."
					);
                }

				client.Send(mail); //send the mail
				return true;
            } 
			catch (Exception ex) when (ex is SmtpException || ex is FormatException) //smtp server can't send email or the format of the mail is invalid
            {
				return false;
			}
        }

		//end activities

		//update database

		//handle <account id>.txt file update 
		public void UpdateFile()
        {
			//add all transactions into strBlock, then write it into file
			//WriteAllText: create a file, write file and close it (overriten existed file)
			string strBlock = "";
			foreach (Transaction tr in transactions)
            {
				strBlock += tr.TextString();
			}
			File.WriteAllText(
				string.Format($"{id}.txt"), //filename
				string.Format(
					$"{balance}\n" + 
					$"{fname}, {lname}\n" +
					$"{address}\n" +
					$"{phone}\n" +
					$"{email}\n\n"+
					$"{strBlock}" //transaction list
				)
			);	
        }

		//handle transaction list update (add new transaction)
		public void UpdateTransactionList(double amount, string type)
        {
			if (type == "deposit") //if this is a deposit transaction
            {
				transactions.Add(new Transaction(DateTime.Now, amount, 0, balance, "Deposit"));
            }
			if (type == "withdraw")//if this is a withdraw transaction
			{
				transactions.Add(new Transaction(DateTime.Now, 0, amount, balance, "Withdraw"));
			}
        }

		//end update database

		//validators

		//check if the id exists
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
	}
	//end of class
}
