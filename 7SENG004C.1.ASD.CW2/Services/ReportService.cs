namespace _7SENG004C._1.ASD.CW2;

/// <summary>
/// The report service.
/// </summary>
public class ReportService
{
    /// <summary>
    /// The unit of work
    /// </summary>
    private readonly UnitOfWork unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReportService"/> class.
    /// </summary>
    public ReportService()
    {
        unitOfWork = UnitOfWork.Instance;
    }

    /// <summary>
    /// Gets the summary report.
    /// </summary>
    /// <param name="user">The user.</param>
    public void GetSummaryReport(User user)
    {
        try
        {
            Console.Write("Enter the summary report year and month (format is yyyy-MM) : ");
            var yearAndMonth = Console.ReadLine();
            var year = Convert.ToInt32(yearAndMonth.Split('-')[0]);
            var month = Convert.ToInt32(yearAndMonth.Split('-')[1]);
            var startDate = DateTime.Parse($"{year}-{month}-1");
            var endDate = startDate.AddMonths(1);
            var userCategories = unitOfWork.GetAll<Category>(filter => filter.IsActive);
            var transactions = unitOfWork.GetAll<Transaction>(filter => filter.StartDate >= startDate && filter.EndDate <= endDate
                && filter.Category.UserId == user.Id && filter.IsActive).ToList().GroupBy(filter => filter.Category);
            var budgets = unitOfWork.GetAll<Budget>(filter => filter.Category.UserId == user.Id && filter.Year == year
                && filter.Month == month && filter.IsActive).ToList().GroupBy(filter => filter.Category);
            Console.WriteLine($"Expense summary for {yearAndMonth}");
            foreach (var category in userCategories)
            {
                var categoryBudgetAmount = (double)default;
                var categoryTxnAmount = (double)default;

                var categoryBudget = budgets.FirstOrDefault(filter => filter.Key.Id == category.Id);
                if (categoryBudget != null)
                    categoryBudgetAmount = categoryBudget.Sum(filter => filter.Amount);
                var categoryTxns = transactions.ToList().Where(filter => filter.Key.Id == category.Id);
                if (categoryTxns.Any())
                    categoryTxnAmount = transactions.Where(filter => filter.Key.Id == category.Id).Select(filter => filter.Sum(data => data.Amount)).Sum();
                Console.WriteLine(@$"Category : {category.Name} | Allocated Budget : {categoryBudgetAmount} | Total Txn Amount : {categoryTxnAmount} | Remain Amount : {categoryBudgetAmount - categoryTxnAmount}");
            }
        }
        catch
        {
            Console.WriteLine("Report generate failed due to invalid input. Try again.");
        }
    }
}