using dotNET.Personal.Finances.Core.Enums;

namespace dotNET.Personal.Finances.Core.Entities;

//Definimos la clase Transaction con sus getter y setters
/*Los métodos no se declarán en esta clase debido a que estos
tienen que estar dentro de la carpeta services, para lograr un
sistema escalable en el futuro*/

public class Transaction {
    public int Id_transaction { get; set; }
    public string Concept { get; set;}
    public double Money { get; set; }
    public string Category{get; set;}
    public TransactionType Type { get; set; }
    public int Id_account { get; set;}

    public Transaction(int Id_transaction, string Concept, double Money, string Category, TransactionType Type, int Id_account){
        this.Id_transaction = Id_transaction;
        this.Concept = Concept;
        this.Money = Money;
        this.Category = Category;
        this.Type = Type;
        this.Id_account = Id_account;
    }

}