using dotNET.Personal.Finances.Core.Entities;
using dotNET.Personal.Finances.Core.Enums;

namespace dotNET.Personal.Finances.Core.Managers.Interfaces;

public interface ITransactionManager{

    Transaction getTransaction(int id_transaction, int id_account);

    bool cancelTransaction(int id_transaction, int id_account, AccountManager accountManager);

    /*Recibe AccountManager para realizar las operaciones 
    dentro del manejador instanciado en la clase Program, es decir,
    utilizará el servicio instanciado al principio del programa*/
    bool newTransaction(string concept, double money, string category, TransactionType type, 
        int id_account, AccountManager accountManager);

}