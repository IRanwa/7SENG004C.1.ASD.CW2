using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7SENG004C._1.ASD.CW2;

public class TransactionService
{
    private readonly UnitOfWork unitOfWork;
    private readonly CategoryService categoryService;

    public TransactionService(CategoryService categoryService)
    {
        unitOfWork = UnitOfWork.Instance;
        this.categoryService = categoryService;
    }

    public void AddTransaction(User user)
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
            Console.Write("Enter the transaction amount : ");
            var amount = Convert.ToDouble(Console.ReadLine());
            DisplayTransactionTypes();
            Console.Write("Enter the transaction type id : ");
            var transactionTypeId = Convert.ToInt32(Console.ReadLine());
            var transactionType = (TransactionType)Enum.ToObject(typeof(TransactionType), transactionTypeId);
            
            var startDateTime = DateTime.Now;
            var endDateTime = DateTime.Now;
            if(transactionType == TransactionType.OneTime)
            {
                Console.Write("Enter the transaction date (format is yyyy-MM-dd) : ");
                var date = Convert.ToDateTime(Console.ReadLine());
                startDateTime = endDateTime = date;
            }else if (transactionType == TransactionType.RecurringDaily || transactionType == TransactionType.RecurringWeekly)
            {
                Console.Write("Enter the transaction start date (format is yyyy-MM-dd) : ");
                startDateTime = Convert.ToDateTime(Console.ReadLine());
                Console.Write("Enter the transaction end date (format is yyyy-MM-dd) : ");
                endDateTime = Convert.ToDateTime(Console.ReadLine());
                if(endDateTime < startDateTime)
                {
                    Console.WriteLine("Entered date range invalid.");
                    return;
                }
            }
            Console.Write("Enter the transaction remark : ");
            var remark = Console.ReadLine();

            var transaction = new Transaction()
            {
                Amount = amount,
                CategoryId = categoryId,
                Type = transactionType,
                StartDate = startDateTime,
                EndDate = endDateTime,
                Remark = remark,
                IsActive = true
            };
            unitOfWork.AddEntity(transaction);
            unitOfWork.SaveChanges();
            Console.WriteLine("Transaction saved successfully.");
        }
        catch
        {
            Console.WriteLine("Input value invalid. Please try again.");
        }
    }

    public void ViewTransaction(User user)
    {
        ListTransaction(user);
        Console.Write("Enter the transaction id : ");
        var transactionId = Guid.Parse(Console.ReadLine());
        var transaction = GetTransactionById(transactionId);
        if(transaction == null)
        {
            Console.WriteLine("Entered transaction id is invalid.");
            return;
        }
        Console.WriteLine($"Transation {transactionId} details");
        Console.WriteLine($"Category : {transaction.Category.Name}");
        Console.WriteLine($"Amount : {transaction.Amount}");
        Console.WriteLine($"Transaction Type : {transaction.Type}");
        Console.WriteLine($"Start Date : {transaction.StartDate.ToShortDateString()}");
        Console.WriteLine($"End Date : {transaction.EndDate.ToShortDateString()}");
        Console.WriteLine($"Remark : {transaction.Remark}");

    }

    public void ListTransaction(User user)
    {
        Console.Write("Enter transactions from date (format is yyyy-MM-dd) : ");
        var startDateTime = Convert.ToDateTime(Console.ReadLine());
        Console.Write("Enter transactions to date (format is yyyy-MM-dd) : ");
        var endDateTime = Convert.ToDateTime(Console.ReadLine());
        var transactions = unitOfWork.GetAll<Transaction>(filter => filter.StartDate >= startDateTime && 
            filter.EndDate <= endDateTime && filter.Category.UserId == user.Id);
        Console.WriteLine();
        Console.WriteLine($"Transaction List from {startDateTime.ToShortDateString()} to {endDateTime.ToShortDateString()}");
        foreach(var transaction in transactions)
            Console.WriteLine($"Txn Id : {transaction.Id} | Date : {transaction.StartDate.ToShortDateString()} to {transaction.EndDate.ToShortDateString()} " +
                $"| Amount : {transaction.Amount} | Category : {transaction.Category.Name}");
    }

    private void DisplayTransactionTypes()
    {
        Console.WriteLine("Transaction Types");
        var transactionTypes = Enum.GetNames(typeof(TransactionType));
        for(var index = 1; index <= transactionTypes.Length; index++)
            Console.WriteLine($"{index}) {transactionTypes[index-1]}");
    }

    private Transaction GetTransactionById(Guid transactionId)
    {
        return unitOfWork.GetOne<Transaction>(filter => filter.Id == transactionId);
    }
}
