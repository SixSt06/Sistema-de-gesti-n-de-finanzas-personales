using dotNET.Personal.Finances.Core.Entities;
using dotNET.Personal.Finances.Core.Enums;
using dotNET.Personal.Finances.Core.Managers;

namespace dotNET.Personal.Finances.Core.Services.Interfaces;

public interface ITransactionService {

    //Devuelce una transaccion a traves de su ID y el ID de la cuenta asociada
    Transaction getTransaction(int id_transaction, int id_account);

    /*Cancela una transaccion a traves de de ID, el ID de la cuenta asociada 
    y el manager de la instancia del servicio principal*/
    bool cancelTransaction(int id_transaction, int id_account, AccountManager accountManager);

    /*Recibe AccountManager para realizar las operaciones 
    dentro del manejador instanciado en la clase Program, es decir,
    utilizará el servicio instanciado al principio del programa*/
    bool newTransaction(string concept, double money, TransactionType type, 
        int id_account, AccountManager accountManager);

}