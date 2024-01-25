using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7SENG004C._1.ASD.CW2;

public class ApplicationService
{
    private readonly UserService userService;
    private readonly CategoryService categoryService;
    private readonly TransactionService transactionService;
    public ApplicationService() {
        userService = new UserService();
        categoryService = new CategoryService();
        transactionService = new TransactionService(categoryService);
    }

    public void Initlize()
    {
        Console.WriteLine("Welcome to the expense tracker.");
        Console.WriteLine("-------------------------------");
        var programExitStatus = false;
        do
        {
            Console.WriteLine();
            Console.WriteLine("Main Menu");
            Console.WriteLine("1) Login");
            Console.WriteLine("2) Register");
            Console.WriteLine("0) Exit from Application");
            Console.WriteLine();
            Console.Write("Select a menu option : ");
            try
            {
                var selection = Convert.ToInt32(Console.ReadLine());
                switch (selection)
                {
                    case 0:
                        programExitStatus = true;
                        break;
                    case 1:
                        var loginUser = userService.LoginUser();
                        if (loginUser != null)
                            DisplayUserFeatures(loginUser);
                        break;
                    case 2:
                        userService.RegisterNewUser();
                        break;
                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Invalid selection. Please try again.");
            }
        } while (!programExitStatus);

        Console.WriteLine();
        Console.WriteLine("Thanks for using the expense tracking application.");
    }

    private void DisplayUserFeatures(User loginUser)
    {
        var loginExitStatus = false;
        do
        {
            Console.WriteLine();
            Console.WriteLine($"Expense Tracking : Login as {loginUser.FullName}");
            Console.WriteLine("1) Manage Categories");
            Console.WriteLine("2) Manage Transactions");
            Console.WriteLine("3) Manage Budget");
            Console.WriteLine("4) View Summary Report By Month");
            Console.WriteLine("5) Logout");
            Console.WriteLine();
            Console.Write("Select a menu option : ");

            try
            {
                var selection = Convert.ToInt32(Console.ReadLine());
                switch (selection)
                {
                    case 1:
                        ManageCategories(loginUser);
                        break;
                    case 2:
                        ManageTransactions(loginUser); 
                        break;
                    case 5:
                        loginExitStatus = true;
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Invalid selection. Please try again.");
            }
        } while (!loginExitStatus);
    }
    private void ManageCategories(User loginUser)
    {
        var manageCategoriesExitStatus = false;
        do
        {
            Console.WriteLine();
            Console.WriteLine($"Manage Categories : Login as {loginUser.FullName}");
            Console.WriteLine("1) Add Category");
            Console.WriteLine("2) Update Category");
            Console.WriteLine("3) Delete Category");
            Console.WriteLine("4) List categories");
            Console.WriteLine("5) Go back");
            Console.WriteLine();
            Console.Write("Select a menu option : ");

            try
            {
                var selection = Convert.ToInt32(Console.ReadLine());
                switch (selection)
                {
                    case 1:
                        categoryService.AddCategory(loginUser);
                        break;
                    case 2:
                        categoryService.UpdateCategory(loginUser);
                        break;
                    case 3:
                        categoryService.DeleteCategory(loginUser);
                        break;
                    case 4:
                        categoryService.ListCategories(loginUser);
                        break;
                    case 5:
                        manageCategoriesExitStatus = true;
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Invalid selection. Please try again.");
            }
        } while (!manageCategoriesExitStatus);
    }

    private void ManageTransactions(User loginUser)
    {
        var manageTransactionExitStatus = false;
        do
        {
            Console.WriteLine();
            Console.WriteLine($"Manage Transactions : Login as {loginUser.FullName}");
            Console.WriteLine("1) Add Transaction");
            Console.WriteLine("2) Update Transaction");
            Console.WriteLine("3) Delete Transaction");
            Console.WriteLine("4) List Transactions");
            Console.WriteLine("5) View Transaction");
            Console.WriteLine("6) Go back");
            Console.WriteLine();
            Console.Write("Select a menu option : ");

            try
            {
                var selection = Convert.ToInt32(Console.ReadLine());
                switch (selection)
                {
                    case 1:
                        transactionService.AddTransaction(loginUser);
                        break;
                    case 4:
                        transactionService.ListTransaction(loginUser);
                        break;
                    case 5:
                        transactionService.ViewTransaction(loginUser);
                        break;
                    case 6:
                        manageTransactionExitStatus = true;
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Invalid selection. Please try again.");
            }
        } while (!manageTransactionExitStatus);
    }
}