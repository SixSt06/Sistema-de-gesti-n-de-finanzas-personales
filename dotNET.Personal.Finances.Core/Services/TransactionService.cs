using dotNET.Personal.Finances.Core.Entities;
using dotNET.Personal.Finances.Core.Enums;
using dotNET.Personal.Finances.Core.Services.Interfaces;
using dotNET.Personal.Finances.Core.Managers;
using System;

namespace dotNET.Personal.Finances.Core.Services;

public class TransactionService : ITransactionService {

    List<Transaction> transactions = new List<Transaction>();

    IDGenerator generator = new IDGenerator(); //Generador de IDs

    public bool newTransaction(string concept, double money, 
        TransactionType type, int id_account, AccountManager accountManager){
        try{
            Transaction transaction = new Transaction(generator.getNewID(), 
                concept, money, type, id_account);
            
            //Evalua si la transaccion es de tipo ingreso o egreso y actualiza el saldo total
            if(transaction.Type == TransactionType.Egress){
                accountManager.updateBalance(id_account, -transaction.Money);
            }else if(transaction.Type == TransactionType.Income){
                accountManager.updateBalance(id_account, transaction.Money);
            }
            
            //Agrega la transaccion a la lista de transacciones
            transactions.Add(transaction);

            return true;
        }catch(Exception ex){
            return false;
        }
    }

    public Transaction getTransaction(int id_transaction, int id_account){
        /*Itera sobre la lista y devuelve 
        aquella que sea la misma del ID y el ID de la cuenta*/
        foreach(Transaction transaction in transactions){
            if((transaction.Id_transaction == id_transaction) 
                && (transaction.Id_transaction == id_account) ){
                return transaction;
            }
        }
        return null;
    }

    public bool cancelTransaction(int id_transaction, int id_account, AccountManager accountManager){
        try{
            Transaction transaction = getTransaction(id_transaction, id_account);
            
            /*Evalua el tipo de transaccion a cancelar para aumentar o restar
            el saldo total de la cuenta, si es egreso -> suma, si es ingresa -> resta*/
            if(transaction.Type == TransactionType.Egress){
                accountManager.updateBalance(id_account, transaction.Money);
            }else if(transaction.Type == TransactionType.Income){
                accountManager.updateBalance(id_account, -transaction.Money);
            }

            return true;
        }catch(Exception ex){
            return false;
        }
    }
    

}