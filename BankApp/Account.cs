using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    /// <summary>
    /// This is about bank account
    /// </summary>
    class Account
    {
        private static int lastAccountNumber = 0;


        #region Properties
        /// <summary>
        /// This holds the account number
        /// </summary>
        public int AccountNumber { get;}

        // this holds the email address of user
        public string EmailAddress { get; set; }

        // this holds the balance of user
        public decimal Balance { get; private set; }

        // this holds the account type of user
        public string AccountType { get; set; }

        // this holds the date of account created
        public DateTime CreatedDate { get; set; }

        #endregion

        #region Constructors
        public Account()
        {
            AccountNumber = ++lastAccountNumber;
        }


        #endregion


        #region Methods
        public decimal Deposit(decimal amount)
        {
            Balance += amount; // Balance = Balance + amount
            return Balance;
        }

        public void Withdraw(decimal amount)
        {
            Balance -= amount;
        }
        #endregion
    }
}
