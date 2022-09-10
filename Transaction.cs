using System;
using System.Collections.Generic;
using System.Text;

namespace Assg1_ConsoleApplication
{
    public class Transaction
    {
        private DateTime time;
        private double credit, debit, balance;
        private string desc;

        public Transaction(DateTime time, double credit, double debit, double balance, string desc)
        {
            this.time = time;
            this.credit = credit;
            this.debit = debit;
            this.balance = balance;
            this.desc = desc;
        }

        // console output
        public override string ToString() 
        {
            //string.Format: string representation of a Transaction object
            //string.PadRight: right spacing in console, with user-assigned spacing
            return  time.ToString("dd/MM/yyyy H:mm tt").PadRight(10, ' ') 
                + desc.PadRight(10, ' ') 
                + string.Format($"{balance:0.00}").PadRight(10, ' ')
                + string.Format($"{credit:0.00}").PadRight(10, ' ')
                + string.Format($"{debit:0.00}").PadRight(10, ' ');
        }

        //email output, in HTML 
        public string EmailString()
        {
            return string.Format(
                "<h2>Transaction Details</h2>" +
                $"<p>Time: {time:dd/MM/yyyy H:mm tt}</p>" +
                $"<p>Balance: {balance:0.00}</p>" +
                $"<p>Credit: {credit:0.00}</p>" +
                $"<p>Debit: {debit:0.00}</p>" + 
                "<p>Description:</p>" +
                $"<p>{desc}</p>"
            );
        }

        //text file output
        public string TextString()
        {
            return string.Format(
                $"{time:dd/MM/yyyy H:mm tt}, {balance:0.00}, {credit:0.00}, {debit:0.00}/n" +
                $"{desc}/n/n"
            );
        }
    }
}
