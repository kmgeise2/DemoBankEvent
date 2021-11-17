// Chapter 13 Handling Events
// You Do It Exercise
// Modified to display negative balances
// Modified to allow 'D' or 'd' for deposit
//   and 'W' or 'w' for withdrawal
using System;
using static System.Console;
class BankAccount
{
    private int acctNum;
    private double balance;
    /*
    // Create an event handler using built-in handler
    // Notice that EventHandler is a delegate type in the 
    //    .NET Framework
    */
    public event EventHandler BalanceAdjusted; // Delegate declared
    public BankAccount(int acct)
    {
        acctNum = acct;
        balance = 0;
    }
    public int AcctNum
    {
        get
        {
            return acctNum;
        }
    }
    public double Balance
    {
        get
        {
            return balance;
        }
    }
    public void MakeDeposit(double amt)
    {
        balance += amt;

        // Invoke the OnBalanceAdjusted event handler
        OnBalanceAdjusted(EventArgs.Empty);
    }
    public void MakeWithdrawal(double amt)
    {
        balance -= amt;

        // Invoke the OnBalanceAdjusted event handler
        OnBalanceAdjusted(EventArgs.Empty);
    }
    
    // OnBalanceAdjusted is an event handler with only one argument
    public void OnBalanceAdjusted(EventArgs e)
    {
        BalanceAdjusted(this, e);
    }
}
class EventListener
//Listens for BankAccount events
{
    private BankAccount acct; //Contains a BankAccount object
    public EventListener(BankAccount account) //Constructor
    {
        acct = account;

        //Add the BankAccountBalanceAdjusted method to the event delegate
        acct.BalanceAdjusted += new EventHandler
          (BankAccountBalanceAdjusted);
    }
    private void BankAccountBalanceAdjusted(object sender, EventArgs e)
    {
        WriteLine("The account balance has been adjusted.");
        WriteLine("   Account# {0}  balance {1}",
          acct.AcctNum, acct.Balance.ToString("N2"));
    }
}
class DemoBankAccountEvent
// This program tests the BankAccount and EventListener classes
{
    static void Main()
    {
        const int TRANSACTIONS = 5;
        char code;
        double amt;
        BankAccount acct = new BankAccount(334455);
        EventListener listener = new EventListener(acct);
        for (int x = 0; x < TRANSACTIONS; ++x)
        {
            Write("Enter D for deposit or W for withdrawal ");
            code = Convert.ToChar(ReadLine());
            Write("Enter dollar amount ");
            amt = Convert.ToDouble(ReadLine());

            if ((code == 'D') || (code == 'd'))
                acct.MakeDeposit(amt);
            else if ((code == 'W') || (code == 'w'))
                acct.MakeWithdrawal(amt);
            else acct.MakeDeposit(0);
        }
    }
}
