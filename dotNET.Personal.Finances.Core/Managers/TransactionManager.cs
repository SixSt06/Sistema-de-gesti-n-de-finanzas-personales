using dotNET.Personal.Finances.Core.Entities;
using dotNET.Personal.Finances.Core.Enums;
using dotNET.Personal.Finances.Core.Managers.Interfaces;
using dotNET.Personal.Finances.Core.Services.Interfaces;


namespace dotNET.Personal.Finances.Core.Managers;

public class TransactionManager : ITransactionManager{

    private readonly ITransactionService _service;

    public TransactionManager(ITransactionService service){
        _service = service;
    }

    public bool newTransaction(string concept, double money, string category, TransactionType type, int id_account, AccountManager accountManager){
        return _service.newTransaction(concept, money, category, type, id_account, accountManager);
    }


    public Transaction getTransaction(int id_transaction, int id_account){
        return _service.getTransaction(id_transaction, id_account);
    }

    public bool cancelTransaction(int id_transaction, int id_account, AccountManager accountManager){
        return _service.cancelTransaction(id_transaction, id_account, accountManager);
    }

}