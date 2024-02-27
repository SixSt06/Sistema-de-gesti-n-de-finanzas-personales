using dotNET.Personal.Finances.Core.Entities;
using dotNET.Personal.Finances.Core.Enums;

namespace dotNET.Personal.Finances.Core.Services.Interfaces;

public interface IAccountService {
    
    //Devuelve una cuenta a traves de su ID
    Account getAccount(int id_account);

    //Crea una nueva cuenta recibiendo todos los parametros
    bool newAccount(string owner, double money, double goal, double budget, double dateGoal);

    //Devuelve el saldo actual de la cuenta
    double currentBalance(int id_account);

    //Devuelve un estado de cuenta
    string summary(int id_account);

    //Devuelve la informacion básica de la cuenta
    string report(int id_account);

    /*Este método de vital importancia para interacción entre las 
    Transactions y la cuenta, de manera que solo con el id de la cuenta se 
    pueda aumentar o restar el saldo total de la cuenta seleccionada*/
    bool updateBalance(int id_account, double amount); 
}