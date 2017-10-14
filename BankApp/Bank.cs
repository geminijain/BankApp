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

        public static List<Account> getAllAccounts(string emailAddress)
        {
            return db.Accounts.Where(a => a.EmailAddress == emailAddress).ToList();
            
        }
    }
}
