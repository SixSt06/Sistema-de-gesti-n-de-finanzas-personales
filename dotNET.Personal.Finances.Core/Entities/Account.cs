namespace dotNET.Personal.Finances.Core.Entities;

//Definimos la clase Account con sus getter y setters
/*Los métodos no se declarán en esta clase debido a que estos
tienen que estar dentro de la carpeta services, para lograr un
sistema escalable en el futuro*/

public class Account{

    public int Id_account { get; set;}
    public string Owner { get; set; }
    public double Money { get; set; }
    public double Goal { get; set; }
    public double Budget { get; set; }
    
    // Establecido en double para determinar la cantidad de semanas necesarias
    public double DateGoal { get; set; } 

    public Account(int Id_account, string Owner, double Money, double Goal, double Budget, double DateGoal){
        this.Id_account = Id_account;
        this.Owner = Owner;
        this.Money = Money;
        this.Goal = Goal;
        this.Budget = Budget;
        this.DateGoal = DateGoal;
    }

    public Account(){
        this.Id_account = 0;
        this.Owner = "";
        this.Money = 0;
        this.Goal = 0;
        this.Budget = 0;
    }
}