using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _7SENG004C._1.ASD.CW2;

public class UserService
{
    private readonly UnitOfWork unitOfWork;
    private readonly CategoryService categoryService;

    public UserService()
    {
        unitOfWork = UnitOfWork.Instance;
        categoryService = new CategoryService();
    }

    public void RegisterNewUser()
    {
        Console.WriteLine();
        Console.WriteLine("Registering new user account");
        Console.Write("Enter the user full name : ");
        var fullName = Console.ReadLine();
        Console.Write("Enter the user username : ");
        var userName = Console.ReadLine();
        Console.Write("Enter the user password : ");
        var password = Console.ReadLine();

        var operationExitStatus = false;
        do
        {
            Console.Write("Confirm to register the user [Y/N] : ");
            var confirmation = Console.ReadLine();

            switch (confirmation)
            {
                case "Y":
                case "y":
                    SaveNewUser(fullName, userName, password, ref operationExitStatus);
                    break;
                case "N":
                case "n":
                    operationExitStatus = true;
                    break;
                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    break;
            }
        } while (!operationExitStatus);
    }

    public User LoginUser()
    {
        Console.WriteLine();
        Console.WriteLine("Login to user account");
        Console.Write("Enter the user username : ");
        var userName = Console.ReadLine();
        Console.Write("Enter the user password : ");
        var password = Console.ReadLine();

        var user = GetUser(userName);
        if (user == null || (user != null && user.Password != password))
        {
            Console.WriteLine("User credentials invalid!");
            return null;
        }
        Console.WriteLine("User login successful!");
        return user;
    }

    private void SaveNewUser(string fullName, string userName, string password, ref bool operationExitStatus)
    {
        operationExitStatus = true;
        var existingUser = GetUser(userName);
        if (existingUser != null)
        {
            Console.WriteLine("Username already exists.");
            return;
        }
        if (userName == string.Empty)
        {
            Console.WriteLine("Entered username not valid.");
            return;
        }
        var user = new User()
        {
            FullName = fullName,
            Username = userName,
            Password = password,
            IsActive = true,
            CreatedDate = DateTime.Now
        };
        unitOfWork.AddEntity(user);
        unitOfWork.SaveChanges();
        categoryService.AddPredefinedCategories(user);
        Console.WriteLine($"User \"{userName}\" registered successfully.");
    }

    public User GetUser(string userName)
    {
        return unitOfWork.GetOne<User>(filter => filter.Username == userName);
    }
}
