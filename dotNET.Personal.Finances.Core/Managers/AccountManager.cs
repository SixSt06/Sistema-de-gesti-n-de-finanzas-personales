using dotNET.Personal.Finances.Core.Entities;
using dotNET.Personal.Finances.Core.Enums;
using dotNET.Personal.Finances.Core.Managers.Interfaces;
using dotNET.Personal.Finances.Core.Services.Interfaces;

namespace dotNET.Personal.Finances.Core.Managers;

public class AccountManager : IAccountManager {

    private readonly IAccountService _service;

    public AccountManager(IAccountService service){
        _service = service;
    }

    public Account getAccount(int id_account){
        return _service.getAccount(id_account);
    }

    public double currentBalance(int id_account){
        return _service.currentBalance(id_account);
    }

    public string report(int id_account){
        return _service.report(id_account);
    }

    public bool updateBalance(int id_account, double amount){
        return _service.updateBalance(id_account, amount);
    }

    public bool newAccount(string owner, double money, double goal, double budget, double dateGoal){
        return _service.newAccount(owner, money, goal, budget, dateGoal);
    }

    public string summary(int id_account, TransactionManager transactionManager){
        return _service.summary(id_account, transactionManager);
    }

    public List<Account> listAccounts(){
        return _service.listAccounts();
    }

}