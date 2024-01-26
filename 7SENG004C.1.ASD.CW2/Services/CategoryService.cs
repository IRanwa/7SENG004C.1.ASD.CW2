namespace _7SENG004C._1.ASD.CW2;

/// <summary>
/// The category service.
/// </summary>
public class CategoryService
{
    /// <summary>
    /// The unit of work
    /// </summary>
    private readonly UnitOfWork unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryService"/> class.
    /// </summary>
    public CategoryService()
    {
        unitOfWork = UnitOfWork.Instance;
    }

    /// <summary>
    /// Adds the predefined categories.
    /// </summary>
    /// <param name="user">The user.</param>
    public void AddPredefinedCategories(User user)
    {
        var predefinedCategories = Enum.GetNames(typeof(PredefinedCategories));
        foreach (var predefinedCategory in predefinedCategories)
        {
            var category = new Category()
            {
                Name = predefinedCategory,
                UserId = user.Id,
                IsActive = true,
                CreatedDate = DateTime.Now,
            };
            unitOfWork.AddEntity(category);
        }
        unitOfWork.SaveChanges();
    }

    /// <summary>
    /// Adds the category.
    /// </summary>
    /// <param name="user">The user.</param>
    public void AddCategory(User user)
    {
        Console.Write("New category name : ");
        var categoryName = Console.ReadLine();
        var existingCategory = GetCategoryByName(user, categoryName);
        if (existingCategory != null)
        {
            Console.WriteLine("Category already exists.");
            return;
        }
        var category = new Category()
        {
            Name = categoryName,
            UserId = user.Id,
            IsActive = true,
            CreatedDate = DateTime.Now
        };
        unitOfWork.AddEntity(category);
        unitOfWork.SaveChanges();
        Console.WriteLine("Category saved successfully.");
    }

    /// <summary>
    /// Updates the category.
    /// </summary>
    /// <param name="user">The user.</param>
    public void UpdateCategory(User user)
    {
        ListCategories(user);
        Console.Write("Enter the category Id to update : ");
        try
        {
            var categoryId = Convert.ToInt32(Console.ReadLine());
            var category = GetCategoryById(categoryId);
            if (category == null)
            {
                Console.WriteLine("Enetered category id is invalid.");
                return;
            }
            Console.WriteLine("Enter new category name : ");
            var categoryName = Console.ReadLine();
            var existingCategory = GetCategoryByName(user, categoryName);
            if (existingCategory != null)
            {
                Console.WriteLine("Category already exists.");
                return;
            }
            category.Name = categoryName;
            unitOfWork.UpdateEntity(category);
            unitOfWork.SaveChanges();
            Console.WriteLine("Category updated successfully!");
        }
        catch
        {
            Console.WriteLine("Invalid input. Please try again.");
        }
    }

    /// <summary>
    /// Deletes the category.
    /// </summary>
    /// <param name="user">The user.</param>
    public void DeleteCategory(User user)
    {
        ListCategories(user);
        Console.WriteLine("Enter the category Id to remove : ");
        try
        {
            var categoryId = Convert.ToInt32(Console.ReadLine());
            var category = GetCategoryById(categoryId);
            if (category == null)
            {
                Console.WriteLine("Enetered category id is invalid.");
                return;
            }
            category.IsActive = false;
            unitOfWork.UpdateEntity(category);
            unitOfWork.SaveChanges();
            Console.WriteLine("Category removed successfully!");
        }
        catch
        {
            Console.WriteLine("Invalid input. Please try again.");
        }
    }

    /// <summary>
    /// Lists the categories.
    /// </summary>
    /// <param name="user">The user.</param>
    public void ListCategories(User user)
    {
        var categories = unitOfWork.GetAll<Category>(filter => filter.UserId == user.Id && filter.IsActive).ToList();
        Console.WriteLine("List of categories");
        foreach (var category in categories)
            Console.WriteLine($"{category.Id}) {category.Name}");
    }

    /// <summary>
    /// Gets the name of the category by.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="categoryName">Name of the category.</param>
    /// <returns>Returns category.</returns>
    private Category GetCategoryByName(User user, string categoryName)
    {
        return unitOfWork.GetOne<Category>(filter => filter.UserId == user.Id &&
        filter.IsActive && filter.Name.ToLower() == categoryName.ToLower());
    }

    /// <summary>
    /// Gets the category by identifier.
    /// </summary>
    /// <param name="categoryId">The category identifier.</param>
    /// <returns>Returns category.</returns>
    public Category GetCategoryById(int categoryId)
    {
        return unitOfWork.GetOne<Category>(filter => filter.Id == categoryId && filter.IsActive);
    }
}