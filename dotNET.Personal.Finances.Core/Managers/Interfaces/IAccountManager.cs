using dotNET.Personal.Finances.Core.Entities;
using dotNET.Personal.Finances.Core.Enums;

namespace dotNET.Personal.Finances.Core.Managers.Interfaces;

public interface IAccountManager{

    Account getAccount(int id_account);

    bool newAccount(string owner, double money, double goal, double budget, double dateGoal);

    double currentBalance(int id_account);

    string summary(int id_account);

    string report(int id_account);

    bool updateBalance(int id_account, double amount); 
}