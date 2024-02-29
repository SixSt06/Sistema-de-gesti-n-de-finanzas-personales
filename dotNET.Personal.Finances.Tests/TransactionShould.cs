using dotNET.Personal.Finances.Core.Entities;
using dotNET.Personal.Finances.Core.Enums;
using dotNET.Personal.Finances.Core.Managers;
using dotNET.Personal.Finances.Core.Services;

namespace dotNET.Personal.Finances.Tests;

public class TransactionShould
{
    [Fact]
    public void GetTransaction_WithValidIds_ReturnsTransaction()
    {
        // Arrange
        var transactionService = new TransactionService();
        var concept = "Test Transaction";
        var category = "Test Category";
        var money = 50.0;
        var type = TransactionType.Income;
        var id_account = 1;

        // Create a new transaction
        var newTransaction = new Transaction(id_account, concept, money, category, type, id_account);
        transactionService.listTransactions().Add(newTransaction);

        // Act
        var result = transactionService.getTransaction(1, 1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(concept, result.Concept);
        Assert.Equal(money, result.Money);
        Assert.Equal(type, result.Type);
        Assert.Equal(id_account, result.Id_account);
    }
    
    [Fact]
    public void NewTransaction_WithValidInputAndEgressType_ShouldDecreaseBalance()
    {
        // Arrange
        var transactionService = new TransactionService();
        var accountService = new AccountService();  // Agregamos un servicio de cuentas
        var accountManager = new AccountManager(accountService);  // Pasamos el servicio de cuentas al manager

        var concept = "Test Egress Transaction";
        var money = 30.0;
        var type = TransactionType.Egress;
        var id_account = 1;
        var category = "Test Category";

        // Creamos una cuenta directamente con el servicio de cuentas
        var initialBalance = 100.0;
        accountService.newAccount("Test Owner", initialBalance, 500.0, 200.0, 10.0);

        // Act
        var result = transactionService.newTransaction(concept, money, category,type, id_account, accountManager);

        // Assert
        Assert.True(result);

        // Verificamos que el saldo de la cuenta se haya actualizado correctamente usando el servicio de cuentas
        var account = accountService.getAccount(id_account);
        var expectedBalance = initialBalance - money;
        Assert.Equal(expectedBalance, account.Money);
    }
}