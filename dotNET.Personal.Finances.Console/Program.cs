/*using System;
using dotNET.Personal.Finances.Core.Entities;
using dotNET.Personal.Finances.Core.Services;
using dotNET.Personal.Finances.Core.Managers;
using dotNET.Personal.Finances.Core.Enums;

namespace dotNET.Personal.Finances.Console;

public static class Program
{
    public static void Main(string[] args)
    {
        AccountService accountService = new AccountService();
        TransactionService transactionService = new TransactionService();
        AccountManager accountManager = new AccountManager(accountService);

        System.Console.WriteLine("!!! Bienvenido al sistema de gestion de finazas personal !!!");

        bool salir = false;

        while (!salir)
        {
            System.Console.WriteLine("---- M E N U ----");
            System.Console.WriteLine("1.- Crear cuenta nueva");
            System.Console.WriteLine("2.- Cuenta Existente");
            System.Console.WriteLine("3.- Salir");

            string opcion = System.Console.ReadLine();

            switch (opcion)
            {

                case "1":
                    System.Console.WriteLine();
                    System.Console.WriteLine("---- CREAR NUEVA CUENTA ----");
                    NewAcount();
                    break;

                case "2":
                    System.Console.WriteLine();
                    System.Console.WriteLine("---- CUENTAS EXISTENTES ----");
                    ShowExistingAccounts();
                    break;

                case "3":
                    System.Console.WriteLine("Hasta Pronto!!");
                    salir = true;
                    break;

                default:
                    System.Console.WriteLine("Opcion Invalida, Ingrese otro valor.");
                    break;

            }
            System.Console.WriteLine();
        }

        void NewAcount()
        {

            System.Console.Write("Ingrese el propietario de la cuenta: ");
            string owner = System.Console.ReadLine();

            System.Console.Write("Ingrese el saldo inicial: ");
            double money;

            while (!double.TryParse(System.Console.ReadLine(), out money) || money < 0)
            {
                System.Console.WriteLine("Ingrese un saldo válido.");

                System.Console.Write("Ingrese el saldo inicial: ");
            }

            System.Console.Write("Ingrese la meta de la cuenta: ");
            double goal;

            while (!double.TryParse(System.Console.ReadLine(), out goal) || goal < 0)
            {
                System.Console.WriteLine("Ingrese una meta válida.");
                System.Console.Write("Ingrese la meta de la cuenta: ");
            }

            System.Console.Write("Ingrese el presupuesto para la meta: ");
            double budget;

            while (!double.TryParse(System.Console.ReadLine(), out budget) || budget < 0)
            {
                System.Console.WriteLine("Ingrese un presupuesto válido.");
                System.Console.Write("Ingrese el presupuesto de la cuenta: ");
            }

            System.Console.Write("Ingrese las semanas calculadas para alcanzar la meta: ");
            double dateGoal;

            while (!double.TryParse(System.Console.ReadLine(), out dateGoal) || dateGoal < 0)
            {
                System.Console.WriteLine("Ingrese un valor válido para las semanas calculadas.");
                System.Console.Write("Ingrese las semanas calculadas para alcanzar la meta: ");
            }

            bool result = accountService.newAccount(owner, money, goal, budget, dateGoal);

            if (result)
            {
                System.Console.WriteLine("Cuenta creada con éxito.");
            }
            else
            {
                System.Console.WriteLine("No se pudo crear la cuenta. Ha ocurrido un error.");
            }
        }

        void ShowExistingAccounts()
        {
            System.Console.Write("Ingrese el ID de la cuenta a consultar: ");
            if (int.TryParse(System.Console.ReadLine(), out int Id_account))
            {
                Account account = accountService.getAccount(Id_account);
                string report = accountService.report(Id_account);

                if (account != null)
                {
                    System.Console.WriteLine(report);
                    System.Console.WriteLine();
                    HandleTransactionOptions(account);
                }
                else
                {
                    System.Console.WriteLine("No se encontró ninguna cuenta con el ID proporcionado.");
                }
            }
            else
            {
                System.Console.WriteLine("Ingrese un ID válido.");
            }
        }


        void HandleTransactionOptions(Account account)
        {
            System.Console.WriteLine("Seleccione una opción:");
            System.Console.WriteLine("1. Nueva Transacción");
            System.Console.WriteLine("2. Consultar Estado de cuenta");
            System.Console.WriteLine("3. Cancelar Transacción");
            System.Console.WriteLine("4. Salir");

            string opcion = System.Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    System.Console.WriteLine("---- NUEVA TRANSACCION ----");
                    NewTransaction();
                    break;

                case "2":
                    System.Console.WriteLine("---- CONSULTAR ESTADO DE CUENTA ----");
                    GetTransaction();
                    break;

                case "3":
                    System.Console.WriteLine("---- CANCELAR TRANSACCIÓN ----");
                    CancelTransaction(account);
                    break;

                case "4":
                    System.Console.WriteLine("Volviendo al menu principal...");
                    break;

                default:
                    System.Console.WriteLine("Opción no válida. Intente de nuevo.");
                    break;
            }
        }

        void NewTransaction()
        {
            System.Console.Write("Ingrese el ID de la cuenta para la transacción: ");
            if (int.TryParse(System.Console.ReadLine(), out int Id_account))
            {
                Account account = accountService.getAccount(Id_account);
                System.Console.WriteLine($"Bienvenido {account.Owner}");

                if (account != null)
                {
                    System.Console.WriteLine("Ingrese el concepto de la transacción: ");
                    string concept = System.Console.ReadLine();

                    System.Console.WriteLine("Ingrese la categoria de la transaccion: ");
                    string category = System.Console.ReadLine();

                    System.Console.Write("Ingrese el monto de la transacción: ");
                    double amount;
                    while (!double.TryParse(System.Console.ReadLine(), out amount) || amount < 0)
                    {
                        System.Console.WriteLine("Ingrese un monto válido.");
                        System.Console.Write("Ingrese el monto de la transacción: ");
                    }

                    System.Console.Write("Seleccione el tipo de transacción (1: Ingreso, 2: Egreso): ");
                    if (int.TryParse(System.Console.ReadLine(), out int transactionType) && (transactionType == 1 || transactionType == 2))
                    {
                        TransactionType type = (transactionType == 1) ? TransactionType.Income : TransactionType.Egress;

                        bool result = transactionService.newTransaction(concept, amount, category, type, Id_account, accountManager);

                        if (result)
                        {
                            System.Console.WriteLine("Transacción realizada con éxito.");
                        }
                        else
                        {
                            System.Console.WriteLine("No se pudo realizar la transacción. Ha ocurrido un error.");
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("Seleccione un tipo de transacción válido (1 o 2).");
                    }
                }
                else
                {
                    System.Console.WriteLine("No se encontró ninguna cuenta con el ID proporcionado.");
                }
            }
            else
            {
                System.Console.WriteLine("Ingrese un ID válido.");
            }
        }

        void GetTransaction()
        {
            System.Console.Write("Ingrese el ID de la transacción: ");
            if (int.TryParse(System.Console.ReadLine(), out int Id_transaction))
            {
                System.Console.Write("Ingrese el ID de la cuenta: ");
                if (int.TryParse(System.Console.ReadLine(), out int Id_account))
                {
                    Transaction transaction = transactionService.getTransaction(Id_transaction, Id_account);

                    if (transaction != null)
                    {
                        System.Console.WriteLine($"Detalles de la transacción:");
                        System.Console.WriteLine($"ID: {transaction.Id_transaction}");
                        System.Console.WriteLine($"Concepto: {transaction.Concept}");
                        System.Console.WriteLine($"Monto: {transaction.Money}");
                        System.Console.WriteLine($"Tipo: {transaction.Type}");
                    }
                    else
                    {
                        System.Console.WriteLine("No se encontró ninguna transacción con el ID proporcionado para la cuenta especificada.");
                    }
                }
                else
                {
                    System.Console.WriteLine("Ingrese un ID de cuenta válido.");
                }
            }
            else
            {
                System.Console.WriteLine("Ingrese un ID de transacción válido.");
            }
        }

        void CancelTransaction(Account account)
        {
            System.Console.Write("Ingrese el ID de la transacción a cancelar: ");
            if (int.TryParse(System.Console.ReadLine(), out int Id_transaccion))
            {
                bool resultadoCancelacion = transactionService.cancelTransaction(Id_transaccion, account.Id_account, accountManager);

                if (resultadoCancelacion)
                {
                    System.Console.WriteLine("Transacción cancelada con éxito.");
                }
                else
                {
                    System.Console.WriteLine("No se pudo cancelar la transacción. Ha ocurrido un error.");
                }
            }
            else
            {
                System.Console.WriteLine("Ingrese un ID válido.");
            }
        }
    }
}
*/