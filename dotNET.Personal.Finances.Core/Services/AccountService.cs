using dotNET.Personal.Finances.Core.Entities;
using dotNET.Personal.Finances.Core.Enums;
using dotNET.Personal.Finances.Core.Managers;
using dotNET.Personal.Finances.Core.Services.Interfaces;
using System;

namespace dotNET.Personal.Finances.Core.Services;

public class AccountService : IAccountService {

    public List<Account> accountList = new List<Account>();

    IDGenerator generator = new IDGenerator(); //Generador de IDs

    public bool newAccount(string owner, double money, double goal, double budget, double dateGoal){
        
        try{
            Account account = new Account(generator.getNewID()+1, owner, money, goal, budget, dateGoal); 
            accountList.Add(account);

            return true;
        }catch(Exception ex){
            System.Console.WriteLine("HA OCURRIDO UN ERROR AL AGREGAR LA CUENTA");
            return false;
        }
    }

    public Account getAccount(int id_account){
        foreach (Account account in accountList){
           if(account.Id_account == id_account){
            return account;
           }
        }
        return null;
    }

    public double currentBalance(int id_account){
        try{
            Account account = getAccount(id_account);
            return account.Money;
        }catch(Exception ex){
            return 0.0;
        }
    }

    public string summary(int id_account, TransactionManager transactionManager){
        try{
            string summary = "LISTADO DE TRANSACCIONES: \n";
            Account account = getAccount(id_account);
            List<Transaction> transactions = transactionManager.listTransactions();

            foreach (Transaction transaction in transactions){
                summary += $"ID: ${transaction.Id_transaction}, Tipo: ${transaction.Type}, Concepto: ${transaction.Concept} -> $ ${transaction.Money}\n";
            }

            return summary;
        }catch(Exception ex){
            return "HA OCURRIDO UN ERROR";
        }
    }

    public string report(int id_account){
        try{
            Account account = getAccount(id_account);
            string report = $"CUENTA: {account.Id_account} \n" +
                $"PROPIETARIO: {account.Owner} \n" +
                $"SALDO: {account.Money} \n" +
                $"META: {account.Goal} \n" +
                $"PRESUPUESTO: {account.Budget} \n" +
                $"SEMANAS CALCULADAS PARA ALCANZAR LA META: {account.DateGoal}";

            return report;
        }catch(Exception ex){
            return "HA OCURRIDO UN ERROR";
        }
    }

    public bool updateBalance(int id_account, double amount){
        try
        {
            Account account = getAccount(id_account);
            account.Money += amount;
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public List<Account> listAccounts(){
        return this.accountList;
    }
}

