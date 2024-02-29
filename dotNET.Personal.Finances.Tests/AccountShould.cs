using dotNET.Personal.Finances.Core.Entities;
using dotNET.Personal.Finances.Core.Services;

namespace dotNET.Personal.Finances.Tests;

public class AccountShould
{
    
    [Fact]
    public void NewAccount_WithValidInput_ReturnsTrue()
    {
        // Arrange
        var accountService = new AccountService();
        var owner = "Peter";
        var money = 100;
        var goal = 500;
        var budget = 200;
        var dateGoal = 10;

        // Act
        var result = accountService.newAccount(owner, money, goal, budget, dateGoal);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void NewAccount_WithNegativeMoney_ReturnsFalse()
    {
        // Arrange
        var accountService = new AccountService();
        var owner = "Peter";
        var money = -50;
        var goal = 500;
        var budget = 200;
        var dateGoal = 10;

        // Act
        var result = accountService.newAccount(owner, money, goal, budget, dateGoal);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void UpdateBalance_AfterIncomeTransaction_ReturnsTrue()
    {
        // Arrange
        var accountService = new AccountService();
        var owner = "Peter";
        var money = 1000.0;
        var goal = 2000.0;
        var budget = 800.0;
        var dateGoal = 20.0;
        accountService.newAccount(owner, money, goal, budget, dateGoal);
        var Id_account = accountService.accountList[0].Id_account;
        var amount = 200.0;

        // Act
        var result = accountService.updateBalance(Id_account, amount);

        // Assert
        Assert.True(result);
    }
}