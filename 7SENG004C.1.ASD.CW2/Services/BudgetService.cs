namespace _7SENG004C._1.ASD.CW2;

/// <summary>
/// The budget service.
/// </summary>
public class BudgetService
{
    /// <summary>
    /// The unit of work.
    /// </summary>
    private readonly UnitOfWork unitOfWork;

    /// <summary>
    /// The category service.
    /// </summary>
    private readonly CategoryService categoryService;

    /// <summary>
    /// Initializes a new instance of the <see cref="BudgetService"/> class.
    /// </summary>
    /// <param name="categoryService">The category service.</param>
    public BudgetService(CategoryService categoryService)
    {
        unitOfWork = UnitOfWork.Instance;
        this.categoryService = categoryService;
    }

    /// <summary>
    /// Adds the budget.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="year">The year.</param>
    /// <param name="month">The month.</param>
    public void AddBudget(User user, int year, int month)
    {
        try
        {
            categoryService.ListCategories(user);
            Console.Write("Select the category id : ");
            var categoryId = Convert.ToInt32(Console.ReadLine());
            var existingCategory = categoryService.GetCategoryById(categoryId);
            if (existingCategory == null)
            {
                Console.WriteLine("Entered category id invalid.");
                return;
            }
            var budgetExistForCategory = GetBudgetByCategoryAndYearMonth(categoryId, year, month);
            if (budgetExistForCategory != null)
            {
                Console.WriteLine("Budget already exists for the selected category and the time period.");
                return;
            }
            Console.Write("Enter the budget amount : ");
            var amount = Convert.ToDouble(Console.ReadLine());
            var budget = new Budget()
            {
                Amount = amount,
                Year = year,
                Month = month,
                CategoryId = categoryId,
                CreatedDate = DateTime.Now,
                IsActive = true
            };
            unitOfWork.AddEntity(budget);
            unitOfWork.SaveChanges();
            Console.WriteLine("Budget saved successfully.");
        }
        catch
        {
            Console.WriteLine("Budget saved failed.");
        }
    }

    /// <summary>
    /// Updates the budget.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="year">The year.</param>
    /// <param name="month">The month.</param>
    public void UpdateBudget(User user, int year, int month)
    {
        try
        {
            ListBudgets(user, year, month);
            Console.Write($"Enter the budget id : ");
            var budgetId = Convert.ToInt32(Console.ReadLine());
            var budget = GetBudgetById(budgetId);
            if (budget == null)
            {
                Console.WriteLine("Entered budget id is invalid.");
                return;
            }
            Console.Write($"Enter the budget new amount (Current amount is {budget.Amount}) : ");
            var amount = Convert.ToDouble(Console.ReadLine());
            budget.Amount = amount;
            unitOfWork.UpdateEntity(budget);
            unitOfWork.SaveChanges();
            Console.WriteLine("Budget updated successfully.");
        }
        catch
        {
            Console.WriteLine("Budget update failed.");
        }
    }

    /// <summary>
    /// Deletes the budget.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="year">The year.</param>
    /// <param name="month">The month.</param>
    public void DeleteBudget(User user, int year, int month)
    {
        try
        {
            ListBudgets(user, year, month);
            Console.Write($"Enter the budget id : ");
            var budgetId = Convert.ToInt32(Console.ReadLine());
            var budget = GetBudgetById(budgetId);
            if (budget == null)
            {
                Console.WriteLine("Entered budget id is invalid.");
                return;
            }
            budget.IsActive = false;
            unitOfWork.UpdateEntity(budget);
            unitOfWork.SaveChanges();
            Console.WriteLine("Budget removed successfully.");
        }
        catch
        {
            Console.WriteLine("Budget remove failed.");
        }
    }

    /// <summary>
    /// Lists the budgets.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="year">The year.</param>
    /// <param name="month">The month.</param>
    public void ListBudgets(User user, int year, int month)
    {
        var budgets = unitOfWork.GetAll<Budget>(filter => filter.Year == year &&
            filter.Month == month && filter.Category.UserId == user.Id && filter.IsActive);
        Console.WriteLine();
        Console.WriteLine($"Budget List for {year}-{month}");
        foreach (var budget in budgets)
            Console.WriteLine($"{budget.Id}) Category : {budget.Category.Name} | Amount : {budget.Amount}");
    }

    /// <summary>
    /// Gets the budget by identifier.
    /// </summary>
    /// <param name="budgetId">The budget identifier.</param>
    /// <returns>Returns budget.</returns>
    private Budget GetBudgetById(int budgetId)
    {
        return unitOfWork.GetOne<Budget>(filter => filter.Id == budgetId && filter.IsActive);
    }

    /// <summary>
    /// Gets the budget by category and year month.
    /// </summary>
    /// <param name="categoryId">The category identifier.</param>
    /// <param name="year">The year.</param>
    /// <param name="month">The month.</param>
    /// <returns>Returns budget.</returns>
    private Budget GetBudgetByCategoryAndYearMonth(int categoryId, int year, int month)
    {
        return unitOfWork.GetOne<Budget>(filter => filter.CategoryId == categoryId && filter.Year == year && filter.Month == month);
    }
}