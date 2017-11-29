using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankUI.Controllers;
using System.Web.Mvc;
using BankUI.Models;
using BankApp;

namespace BankUI.Tests.Controllers
{
    /// <summary>
    /// Summary description for AccountsControllerTest
    /// </summary>
    [TestClass]
    public class AccountsControllerTest
    {
        [TestMethod]
        public void TestIndexViewWithNoLogin()
        {
            var controller = new AccountsController();
           // Assert.ThrowsException<NullReferenceException>(controller.Index);
        }

        [TestMethod]
        public void TestIndexViewWithLogin()
        {
            var controller = new AccountsController();
            controller.UserName = "test@test.com";
            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void VerifyAccountsDataTest()
        {
            var controller = new AccountsController();
            controller.UserName = "test@test.com";
            var result = controller.Index() as ViewResult;
            var accounts = (List<Account>)result.ViewData.Model;
            Assert.AreEqual(4, accounts.Count);
        }

        [TestMethod]
        public void TestDetailsWithId()
        {
            var controller = new AccountsController();
            var result = controller.Details(1) as ViewResult;
            var account = (Account)result.ViewData.Model;
            Assert.AreEqual(3, (int)account.AccountType);
        }

    }
}