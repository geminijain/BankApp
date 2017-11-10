using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
 {
    public enum TypeOfAccount
    {
        Checking ,
        Savings ,
        Loan ,
        CD
    }

    /// <summary>
    /// This is about bank account
    /// </summary>
    public class Account
    {
        
        #region Properties
        /// <summary>
        /// This holds the account number
        /// </summary>
        
        [Key]   
        public int AccountNumber { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Email address cannot be greater than 50 characters in length")]
        // this holds the email address of user
        public string EmailAddress { get; set; }

        // this holds the balance of user
        public decimal Balance { get; set; }

        // this holds the account type of user
        public TypeOfAccount AccountType { get; set; }

        // this holds the date of account created
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        #endregion

        #region Constructors
        public Account()
        {  CreatedDate = DateTime.UtcNow;
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
            if (amount > Balance)
                throw new ArgumentOutOfRangeException("amount", "Insufficient Funds");
            Balance -= amount;
        }
        #endregion
    }
}
