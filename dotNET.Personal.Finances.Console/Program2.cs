using System;
using dotNET.Personal.Finances.Core.Entities;
using dotNET.Personal.Finances.Core.Services;
using dotNET.Personal.Finances.Core.Managers;
using dotNET.Personal.Finances.Core.Enums;

namespace dotNET.Personal.Finances.Console;

public static class Program{

    public static void Main(string[] args){

        //Creamos una instancia de nuestros servicios
        AccountService accountService = new AccountService();
        TransactionService transactionService = new TransactionService();

        //Manejamos nuestros servicios con clases Manager
        AccountManager accountManager = new AccountManager(accountService);
        TransactionManager transactionManager = new TransactionManager(transactionService);

        Account account = new Account();

        //Menu principal

        System.Console.WriteLine("!!! Bienvenido al sistema de gestion de finazas personal !!!");

        bool menu = true;

        do{

            System.Console.WriteLine("---- M E N U ----");
            System.Console.WriteLine("1.- Crear cuenta nueva");
            System.Console.WriteLine("2.- Mostrar cuentas");
            System.Console.WriteLine("3.- Cuenta existente");
            System.Console.WriteLine("4.- Salir");

            System.Console.Write("Ingresa una opción: ");
            string opcions =System.Console.ReadLine();

            if(int.TryParse(opcions, out int opcion)){
                
                switch (opcion)
                {
                    case 1:
                        
                        System.Console.WriteLine("\n---- CREAR NUEVA CUENTA ----");
                        if(NewAcount(accountManager)){
                            System.Console.WriteLine("Cuenta creada con exito!");
                        }else{
                            System.Console.WriteLine("Operación rechazada D:");
                        }
                        break;

                    case 2:

                        ShowExistingAccounts(accountManager);

                        break;

                    case 3:

                        int id = 0;
                        System.Console.Write("\nIngresa el ID de la cuenta: ");

                        while (!int.TryParse(System.Console.ReadLine(), out id) || id < 0){
                            System.Console.WriteLine("Ingrese una meta válida.");
                            System.Console.Write("\nIngresa el ID de la cuenta: ");
                        }

                        account = SelectAccount(id, accountManager);

                        System.Console.WriteLine($"CUENTA SELECCIONADA: {account.Owner}");

                        break;
                    
                    case 4: 
                        menu = false;
                        break;
                    default:
                        System.Console.WriteLine("NO EXISTE ESA OPCION\n");
                        break;
                }

            }else{
                System.Console.WriteLine("SOLO OPCIONES VÁLIDAS");
            }

        } while (menu);

        //Opciones del menu principal

        bool NewAcount(AccountManager accountManager){

            try{

                string owner = "";
                double goal = 0.0;
                double budget = 0.0;
                double dateGoal = 0.0;

                System.Console.Write("Ingrese el propietario de la cuenta: ");
                owner = System.Console.ReadLine();

                System.Console.Write("Ingrese la meta de la cuenta: ");
                while (!double.TryParse(System.Console.ReadLine(), out goal) || goal < 0){
                    System.Console.WriteLine("Ingrese una meta válida.");
                    System.Console.Write("Ingrese la meta de la cuenta: ");
                }

                System.Console.Write("Ingrese los meses calculados para alcanzar la meta: ");
                while (!double.TryParse(System.Console.ReadLine(), out dateGoal) || dateGoal < 0){
                    System.Console.WriteLine("Ingrese un valor válido para los meses calculados.");
                    System.Console.Write("Ingrese los meses calculados para alcanzar la meta: ");
                }

                budget = goal/dateGoal;

                System.Console.WriteLine($"PRESUPUESTO MENSUAL CALCULADO: {budget}\n");

                return accountService.newAccount(owner, 0.0, goal, budget, dateGoal);
            }catch(Exception){
                return false;    
            }
        }

        bool ShowExistingAccounts(AccountManager accountManager){
            try{

                List<Account> accounts = accountManager.listAccounts();
                System.Console.WriteLine("LISTADO DE CUENTAS:\n");
                foreach (Account account in accounts){
                    System.Console.WriteLine($"ID: {account.Id_account} -> Nombre: {account.Owner}");
                }

                System.Console.WriteLine();

                return true;
            }catch(Exception){
                return false;
            }
        }

        Account SelectAccount(int id_account, AccountManager accountManager){

            Account account;

            try{
                return accountManager.getAccount(id_account);
            }catch(Exception){
                return new Account();
            }
            
        }


        //Opciones del sub-menu de cuenta individual

        bool ShowExistingTransactions(TransactionManager transactionManager){

            try{

                System.Console.Write("Ingrese el ID de la cuenta a consultar: ");
                if (int.TryParse(System.Console.ReadLine(), out int Id_account)){
                    Account account = accountService.getAccount(Id_account);
                    string summary = accountService.report(Id_account);

                    if (account != null){
                        System.Console.WriteLine(summary);
                        System.Console.WriteLine();
                    }else{
                        System.Console.WriteLine("No se encontró ninguna cuenta con el ID proporcionado.");
                    }
                }else{
                    System.Console.WriteLine("Ingrese un ID válido.");
                }

                return true;
            }catch(Exception){
                return false;
            }

            
        }

    }

}