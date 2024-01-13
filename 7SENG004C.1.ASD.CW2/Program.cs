using _7SENG004C._1.ASD.CW2;

Console.WriteLine("Hello, World!");

using (var context = new ASDDbContext())
{
    var firstUser = new User()
    {
        Name = "Imesh Ranawaka",
        CreatedDate = DateTime.Now,
        IsActive = true
    };
    context.Users.Add(firstUser);
    await context.SaveChangesAsync();

    var users = context.Users.ToList();
    foreach(var user in users)
    {
        Console.WriteLine(user.Name);
    }
}
Console.WriteLine("Ended!");