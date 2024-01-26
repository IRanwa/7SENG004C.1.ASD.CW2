namespace _7SENG004C._1.ASD.CW2;

/// <summary>
/// The user service.
/// </summary>
public class UserService
{
    /// <summary>
    /// The unit of work
    /// </summary>
    private readonly UnitOfWork unitOfWork;

    /// <summary>
    /// The category service
    /// </summary>
    private readonly CategoryService categoryService;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserService"/> class.
    /// </summary>
    /// <param name="categoryService">The category service.</param>
    public UserService(CategoryService categoryService)
    {
        unitOfWork = UnitOfWork.Instance;
        this.categoryService = categoryService;
    }

    /// <summary>
    /// Registers the new user.
    /// </summary>
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

    /// <summary>
    /// Logins the user.
    /// </summary>
    /// <returns>Returns user.</returns>
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

    /// <summary>
    /// Saves the new user.
    /// </summary>
    /// <param name="fullName">The full name.</param>
    /// <param name="userName">Name of the user.</param>
    /// <param name="password">The password.</param>
    /// <param name="operationExitStatus">if set to <c>true</c> [operation exit status].</param>
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

    /// <summary>
    /// Gets the user.
    /// </summary>
    /// <param name="userName">Name of the user.</param>
    /// <returns>Returns user.</returns>
    public User GetUser(string userName)
    {
        return unitOfWork.GetOne<User>(filter => filter.Username == userName);
    }
}