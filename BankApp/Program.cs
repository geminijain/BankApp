using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("**********************************");
            Console.WriteLine("Welcome to my Bank");
            Console.WriteLine("**********************************");
            while (true)
            {
                Console.WriteLine("Please choose an option below : ");
                Console.WriteLine("0. Exit");
                Console.WriteLine("1. Create an account");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Print all accounts");
                Console.WriteLine("5. Print all transactions");

                var choice = Console.ReadLine();
                switch(choice)
                {
                    case "0":
                        return;

                    case "1":
                        Console.Write("Email Address :");
                        var emailAddress = Console.ReadLine();
                        Console.WriteLine("Account type:");
                        var accountTypes = Enum.GetNames(typeof(TypeOfAccount));
                        for(var i =0; i < accountTypes.Length; i++)
                        {
                            Console.WriteLine($"{i}. {accountTypes[i]}");
                        }
                        var accountType = (TypeOfAccount)Enum.Parse(typeof(TypeOfAccount), Console.ReadLine());
                        Console.Write("Amount to deposit: ");
                        var amount = Convert.ToDecimal(Console.ReadLine());
                        var account = Bank.CreateAccount(emailAddress, accountType, amount);
                        Console.WriteLine($"AN: {account.AccountNumber}, AT: {account.AccountType}, Balance: {account.Balance:C}, Created Date: {account.CreatedDate}");
                        break;

                    case "2":
                        PrintAllAccounts();
                        try
                        {

                            Console.Write("Account number:");
                            var aNumber = Convert.ToInt32(Console.ReadLine());
                            Console.Write("amount to deposit: ");
                            amount = Convert.ToDecimal(Console.ReadLine());
                            Bank.Deposit(aNumber, amount);
                            Console.WriteLine("Deposit was successful !");
                        }
                        catch(FormatException)
                        {
                            Console.WriteLine("Either the account number or amount is invalid");
                        }
                        catch (OverflowException)
                        {
                            Console.WriteLine("Either the account number or amount is beyond the range");
                        }
                        catch (ArgumentOutOfRangeException ax)
                        {
                            Console.WriteLine(ax.Message);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Oops..!Try again!");
                        }
                        break;

                    case "3":
                        PrintAllAccounts();
                        try
                        {

                            Console.Write("Account number:");
                            var aNumber = Convert.ToInt32(Console.ReadLine());
                            Console.Write("amount to withdraw: ");
                            amount = Convert.ToDecimal(Console.ReadLine());
                            Bank.Withdraw(aNumber, amount);
                            Console.WriteLine("Withdrawl was sussesful !");
                        }
                        catch(ArgumentOutOfRangeException ax)
                        {
                            Console.WriteLine(ax.Message);
                        }
                        break;

                    case "4":
                        PrintAllAccounts();
                        break;
                    
                    case "5":
                        PrintAllAccounts();
                        Console.Write("Account number:");
                        var accountNumber = Convert.ToInt32(Console.ReadLine());
                        var transactions = Bank.GetAllTransactions(accountNumber);
                        foreach(var tran in transactions)
                        {
                            Console.WriteLine($"Id:{tran.TransactionId}, Date:{tran.TransactionDate}, Type: {tran.TypeOfTransaction}, Amount:{tran.Amount:C}, Description:{tran.Description}");
                        }
                        break;

                    default:
                        break;

                }

            }
        }

        private static void PrintAllAccounts()
        {
            Console.Write("Email Address: ");
            var emailAddress = Console.ReadLine();
            var accounts = Bank.GetAllAccounts(emailAddress);
            foreach (var item in accounts)
            {
                Console.WriteLine($"AN: {item.AccountNumber}, AT: {item.AccountType}, Balance: {item.Balance:C}, Created Date: {item.CreatedDate}");
            }
        }
    }
}
