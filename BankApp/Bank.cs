using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    public static class Bank
    {
        private static BankModel db = new BankModel();

        /// <summary>
        /// Bank creates an account for the user        /// 
        /// </summary>
        /// <param name="emailAddress"> Email address of the account</param>
        /// <param name="accountType">Type of Account</param>
        /// <param name="initalDeposit">Initial amount to deposit</param>
        /// <returns>returns the new account</returns>
        public static Account CreateAccount(String emailAddress, TypeOfAccount accountType = TypeOfAccount.Checking, decimal initalDeposit = 0)
        {
            var account = new Account
            {
                EmailAddress = emailAddress,
                AccountType = accountType
            };
            
            if(initalDeposit > 0)
            {
                account.Deposit(initalDeposit);
            }
            db.Accounts.Add(account);
            db.SaveChanges();
            return account;
        }

        public static void EditAccount(Account account)
        {
            var oldAccount = GetAccountByAccountNumber(account.AccountNumber);
            db.Entry(oldAccount).State = System.Data.Entity.EntityState.Modified;
            oldAccount.AccountType = account.AccountType;
            db.SaveChanges();
        }

        public static List<Account> GetAllAccounts(string emailAddress)
        {
            return db.Accounts.Where(a => a.EmailAddress == emailAddress).ToList();
            
        }

        /// <summary>
        /// deposit money into the account
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <param name="amount"></param>
        /// <exception cref="ArgumentOutOfRangeException">ArgumentOutOfRangeException</exception>
        public static void Deposit(int accountNumber, decimal amount)
        {
            try
            {
                var account = GetAccountByAccountNumber(accountNumber);
                account.Deposit(amount);

                var transaction = new Transaction
                {
                    TransactionDate = DateTime.UtcNow,
                    TypeOfTransaction = TransactionType.Credit,
                    Description = "Branch deposit",
                    Amount = amount,
                    AccountNumber = account.AccountNumber
                };
                db.Transactions.Add(transaction);

                db.SaveChanges();
            }
            catch
            {
                //Log
                throw;
            }

        }

        public static void Withdraw(int accountNumber, decimal amount)
        {
            var account = GetAccountByAccountNumber(accountNumber);
            account.Withdraw(amount);

            var transaction = new Transaction
            {
                TransactionDate = DateTime.UtcNow,
                TypeOfTransaction = TransactionType.Debit,
                Description = "Branch Withdrawl",
                Amount = amount,
                AccountNumber = account.AccountNumber
            };
            db.Transactions.Add(transaction);

            db.SaveChanges();
        }

        public static Account GetAccountByAccountNumber(int accountNumber)
        {
            var account = db.Accounts.Where(a => a.AccountNumber == accountNumber).FirstOrDefault();
            if (account == null)
                throw new ArgumentOutOfRangeException("Invalid account Number");
            return account;
        }

        public static List<Transaction> GetAllTransactions(int accountNumber)
        {
            return db.Transactions.Where(t => t.AccountNumber == accountNumber).OrderByDescending(t => t.TransactionDate).ToList();

        }
    }
}
