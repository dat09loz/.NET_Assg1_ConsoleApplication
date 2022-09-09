using System;
using System.Collections.Generic;
using System.Text;

namespace Assg1_ConsoleApplication
{
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
}
