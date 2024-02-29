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

                        System.Console.WriteLine($"\nCUENTA SELECCIONADA: {account.Owner}\n");

                        //Sub menu de transacciones

                        bool submenu = true;

                        do{
                            System.Console.WriteLine("\nSeleccione una opción:");
                            System.Console.WriteLine("1. Nueva Transacción");
                            System.Console.WriteLine("2. Consultar Estado de cuenta");
                            System.Console.WriteLine("3. Saldo actual");
                            System.Console.WriteLine("4. Informe");
                            //System.Console.WriteLine("4. Cancelar Transacción");
                            System.Console.WriteLine("5. Salir");
                            System.Console.Write("Ingresa una opcion: ");

                            string opcionx = System.Console.ReadLine();

                            switch (opcionx){
                                case "1":
                                    System.Console.WriteLine("\n---- NUEVA TRANSACCION ----");
                                    NewTransaction(accountManager);
                                    break;

                                case "2":
                                    System.Console.WriteLine("\n---- CONSULTAR ESTADO DE CUENTA ----");
                                    GetTransactions(transactionManager, account);
                                    break;

                                case "3":
                                    System.Console.WriteLine("\n---- SALDO ACTUAL ----");
                                    GetCurrentBalance(accountManager, account);
                                    break;
                                /*
                                case "4":
                                    System.Console.WriteLine("---- CANCELAR TRANSACCIÓN ----");
                                    
                                    if(CancelTransaction(account.Id_account, accountManager)){
                                        System.Console.WriteLine("\nCancelada correctamente\n");
                                    }else{
                                        System.Console.WriteLine("\nOperación rechazada\n");
                                    }
                                    
                                    break;*/

                                case "4":
                                    System.Console.WriteLine("\n---- INFORMACIÓN ----");
                                    System.Console.WriteLine(accountManager.report(account.Id_account)+"\n");
                                    break;

                                case "5":
                                    System.Console.WriteLine("Volviendo al menu principal...");
                                    submenu = false;
                                    break;

                                default:
                                    System.Console.WriteLine("Opción no válida. Intente de nuevo.");
                                    break;
                            }               
                        }while(submenu);


                        break;
                    
                    case 4: 
                        menu = false;
                        break;
                    default:
                        System.Console.WriteLine("\nNO EXISTE ESA OPCION\n");
                        break;
                }

            }else{
                System.Console.WriteLine("\nSOLO OPCIONES VÁLIDAS\n");
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
                    string summary = accountService.summary(Id_account, transactionManager);

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

        void NewTransaction(AccountManager accountManager){
            if (account != null){
                double amount = 0;
                string concept = "";
                string category = "";

                System.Console.WriteLine("Ingrese el concepto de la transacción: ");
                concept = System.Console.ReadLine();

                System.Console.WriteLine("Ingrese la categoria de la transaccion: ");
                category = System.Console.ReadLine();

                System.Console.Write("Ingrese el monto de la transacción: ");
                
                while (!double.TryParse(System.Console.ReadLine(), out amount) || amount < 0){
                    System.Console.WriteLine("Ingrese un monto válido.");
                    System.Console.Write("Ingrese el monto de la transacción: ");
                }

                System.Console.Write("Seleccione el tipo de transacción (1: Ingreso, 2: Egreso): ");
                if (int.TryParse(System.Console.ReadLine(), out int transactionType) && (transactionType == 1 || transactionType == 2)){
                    TransactionType type = (transactionType == 1) ? TransactionType.Income : TransactionType.Egress;

                    bool result = transactionService.newTransaction(concept, amount, category, type, account.Id_account, accountManager);

                    if (result){
                        System.Console.WriteLine("Transacción realizada con éxito.");
                    }else{
                        System.Console.WriteLine("No se pudo realizar la transacción. Ha ocurrido un error.");
                    }

                }else{
                    System.Console.WriteLine("Seleccione un tipo de transacción válido (1 o 2).");
                }
            }else{
                System.Console.WriteLine("No se encontró ninguna cuenta con el ID proporcionado.");
            }
        }

        bool CancelTransaction(int id_account, AccountManager accountManager){
            try{
                
                int id_transactionc = 0;

                System.Console.Write("Ingrese el ID de la transacción: ");

                while (!int.TryParse(System.Console.ReadLine(), out id_transactionc) || id_transactionc < 0){
                    System.Console.WriteLine("\nIngrese una transaccion valida.");
                    System.Console.Write("Ingrese el ID de la transacción: ");
                }

                transactionManager.cancelTransaction(id_transactionc, id_account, accountManager);

                return true;
            }catch(Exception){
                return false;
            }
        }

        void GetTransactions(TransactionManager transactionManager, Account account){
            
            List<Transaction> transactions = transactionManager.listTransactions();

            foreach (Transaction trans in transactions){
                if(trans.Id_account == account.Id_account){
                    System.Console.WriteLine($"ID: {trans.Id_transaction}, Tipo: {trans.Type}, Concepto: {trans.Concept} -> Monto: {trans.Money}");
                }
            }
            
            
        }

        void GetCurrentBalance(AccountManager accountManager, Account account){
            try{
                double currentBalance = accountManager.currentBalance(account.Id_account);
                System.Console.WriteLine($"\nSALDO ACTUAL: {currentBalance}\n");
            }catch(Exception){

            }
        }


    }

}