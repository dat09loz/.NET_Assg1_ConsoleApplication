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
			Console.WriteLine($"    |     Account Number: {id}".PadRight(67, ' ') + "|");
			Console.WriteLine($"    |     Account Balance: {balance}".PadRight(67, ' ') + "|");
			Console.WriteLine($"    |     First Name: {fname}".PadRight(67, ' ') + "|");
			Console.WriteLine($"    |     Last Name: {lname}".PadRight(67, ' ') + "|");
			Console.WriteLine($"    |     Address: {address}".PadRight(67, ' ') + "|");
			Console.WriteLine($"    |     Phone: {phone}".PadRight(67, ' ') + "|");
			Console.WriteLine($"    |     Email: {email}".PadRight(67, ' ') + "|");
			Console.WriteLine("    |______________________________________________________________|");
		}

		//output account statement form to the console
		public void OutputAccStatement()
		{ //insert each of the account properties into the console form with the appropriate padding
			Console.WriteLine("     ______________________________________________________________");
			Console.WriteLine("    |                                                              |");
			Console.WriteLine("    |                        ACCOUNT DETAILS                       |");
			Console.WriteLine("    |                                                              |");
			Console.WriteLine("    |______________________________________________________________|");
			Console.WriteLine("    |     Account Statement                                        |");
			Console.WriteLine("    |                                                              |");
			Console.WriteLine($"    |     Account Number: {id}".PadRight(67, ' ') + "|");
			Console.WriteLine($"    |     Account Balance: {balance}".PadRight(67, ' ') + "|");
			Console.WriteLine($"    |     First Name: {fname}".PadRight(67, ' ') + "|");
			Console.WriteLine($"    |     Last Name: {lname}".PadRight(67, ' ') + "|");
			Console.WriteLine($"    |     Address: {address}".PadRight(67, ' ') + "|");
			Console.WriteLine($"    |     Phone: {phone}".PadRight(67, ' ') + "|");
			Console.WriteLine($"    |     Email: {email}".PadRight(67, ' ') + "|");
			Console.WriteLine("    |______________________________________________________________|");
			Console.WriteLine("\n    Transaction History:\n");
			OutputTransactions();
		}

		//output transaction list/history to the console (table style)
		public void OutputTransactions()
		{
			Console.WriteLine("    |      Date & Time      |      Balance      |      Credit      |      Debit      |      Description      |");
			Console.WriteLine("    ".PadRight(111, '-') + "\n"); //table grid
			if (transactions.Count > 5)
			{ 
				foreach (Transaction tr in transactions) //if the transaction list is <5, output all
				{
					Console.WriteLine("    " + tr); //Transaction.ToString method 
				}
			}
			else
            {
				for (int i = transactions.Count - 1; i >= 0; --i) //if >=5, output the last 5 transactions
                {
					Transaction tr = transactions[i];
					Console.WriteLine("    " + tr); 
				}
			}
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
						"We are glad to have you on board! Here is your account details:<br><br>" +
						
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
						$"Dear, {fname}<br>" +
						$"Here is your account statement:<br><br><br>" +

						$"Account ID: {id}<br>" +
						$"First Name: {fname}<br>" +
						$"Last Name: {lname}<br>" +
						$"Address: {address}<br>" +
						$"Phone: {phone}<br>" +
						$"Email: {email}<br><br>" +

						$"Transaction History<br>" +
						$"{strBlock}<br><br><br>" +
						$"End of Statement.<br><br>" +

						"Regards,<br>" +
						"Simple Banking Co."
					);;
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
					$"{fname}\n{lname}\n{email}\n{address}\n{phone}\n{balance}\n\n" + //acc details
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

		//handle readding transaction list from the resurrected account
		public void ReAddTransactionList()
        {
			string[] trArr = File.ReadAllLines($"{id}.txt").Skip(7).ToArray(); //skip the account details section
			foreach (string tr in trArr)
            {
				//split data from each transaction format: $"{time:dd/MM/yyyy H:mm tt}, {credit:0.00}, {debit:0.00}, {balance:0.00}, {desc}"
				string[] trInfo = tr.Split(",", StringSplitOptions.RemoveEmptyEntries); //separate by comma, remove empty string before adding to the arr
				transactions.Add(new Transaction(Convert.ToDateTime(trInfo[0]), Convert.ToDouble(trInfo[1]), Convert.ToDouble(trInfo[2]), Convert.ToDouble(trInfo[3]), trInfo[4]));
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
