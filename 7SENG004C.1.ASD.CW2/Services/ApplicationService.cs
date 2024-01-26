namespace _7SENG004C._1.ASD.CW2;

/// <summary>
/// The application service.
/// </summary>
public class ApplicationService
{
    /// <summary>
    /// The user service.
    /// </summary>
    private readonly UserService userService;

    /// <summary>
    /// The category service.
    /// </summary>
    private readonly CategoryService categoryService;

    /// <summary>
    /// The transaction service.
    /// </summary>
    private readonly TransactionService transactionService;

    /// <summary>
    /// The budget service.
    /// </summary>
    private readonly BudgetService budgetService;

    /// <summary>
    /// The report service.
    /// </summary>
    private readonly ReportService reportService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationService"/> class.
    /// </summary>
    public ApplicationService()
    {
        categoryService = new CategoryService();
        userService = new UserService(categoryService);
        budgetService = new BudgetService(categoryService);
        transactionService = new TransactionService(categoryService);
        reportService = new ReportService();
    }

    /// <summary>
    /// Initlizes this instance.
    /// </summary>
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

    /// <summary>
    /// Displays the user features.
    /// </summary>
    /// <param name="loginUser">The login user.</param>
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
                    case 3:
                        ManageBudgets(loginUser);
                        break;
                    case 4:
                        reportService.GetSummaryReport(loginUser);
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

    /// <summary>
    /// Manages the categories.
    /// </summary>
    /// <param name="loginUser">The login user.</param>
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

    /// <summary>
    /// Manages the transactions.
    /// </summary>
    /// <param name="loginUser">The login user.</param>
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
                    case 2:
                        transactionService.UpdateTransaction(loginUser);
                        break;
                    case 3:
                        transactionService.DeleteTransaction(loginUser);
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

    /// <summary>
    /// Manages the budgets.
    /// </summary>
    /// <param name="loginUser">The login user.</param>
    private void ManageBudgets(User loginUser)
    {
        var manageBudgetExitStatus = false;
        Console.WriteLine();
        Console.WriteLine("Enter the budget year and month (format is yyyy-MM) : ");
        var yearAndMonth = Console.ReadLine();
        var year = Convert.ToInt32(yearAndMonth.Split('-')[0]);
        var month = Convert.ToInt32(yearAndMonth.Split('-')[1]);
        do
        {
            Console.WriteLine($"Manage Budget for {yearAndMonth} : Login as {loginUser.FullName}");
            Console.WriteLine("1) Add Budget");
            Console.WriteLine("2) Update Budget");
            Console.WriteLine("3) Delete Budget");
            Console.WriteLine("4) List Budgets");
            Console.WriteLine("5) Go back");
            Console.WriteLine();
            Console.Write("Select a menu option : ");

            try
            {
                var selection = Convert.ToInt32(Console.ReadLine());
                switch (selection)
                {
                    case 1:
                        budgetService.AddBudget(loginUser, year, month);
                        break;
                    case 2:
                        budgetService.UpdateBudget(loginUser, year, month);
                        break;
                    case 3:
                        budgetService.DeleteBudget(loginUser, year, month);
                        break;
                    case 4:
                        budgetService.ListBudgets(loginUser, year, month);
                        break;
                    case 5:
                        manageBudgetExitStatus = true;
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Invalid selection. Please try again.");
            }
        } while (!manageBudgetExitStatus);
    }
}