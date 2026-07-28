
using System;
using System.Linq;
using E_Commerce_Database.Models;
namespace E_Commerce_Database
{
    internal class Program
    {
        // Shared DbContext - created ONCE, here, so every function below reuses
        // the exact same instance instead of each function opening its own.
        static E_DBContext context = new E_DBContext();
        // Shared login state - 0 means "nobody is logged in".
        // Set by Login(), read by any function that requires a logged-in user,
        // reset back to 0 by Logout().
        static int loggedInUserId = 0;
        static void Main(string[] args)
        {
            bool exitApp = false;
            while (!exitApp)
            {
                Console.WriteLine("\n===== E-Commerce Console App =====");
                Console.WriteLine(" 1. Register New User");
                Console.WriteLine(" 2. Login");
                Console.WriteLine(" 3. Add New Category");
                Console.WriteLine(" 4. Add New Product");
                Console.WriteLine(" 5. View All Products");
                Console.WriteLine(" 6. Place an Order");
                Console.WriteLine(" 7. View My Orders");
                Console.WriteLine(" 8. View Order Details");
                Console.WriteLine(" 9. Add a Review for an Order");
                Console.WriteLine("10. View All Reviews for a Product");
                Console.WriteLine("11. Logout");
                Console.WriteLine(" 0. Exit");
                Console.Write("Enter your choice: ");
                int choice;
                try
                {
                    choice = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }
                switch (choice)
                {
                    case 1: RegisterUser(); break;
                    case 2: Login(); break;
                    case 3: AddCategory(); break;
                    case 4: AddProduct(); break;
                    case 5: ViewAllProducts(); break;
                    case 6: PlaceOrder(); break;
                    case 7: ViewMyOrders(); break;
                    case 8: ViewOrderDetails(); break;
                    case 9: AddReview(); break;
                    case 10: ViewReviewsForProduct(); break;
                    case 11: Logout(); break;
                    case 0:
                        exitApp = true;
                        Console.WriteLine("Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
        // ===================== FUNCTIONS =====================
        // Every function below talks to the console itself AND uses the
        // shared "context" field declared above - never create a new
        // AppDbContext() inside any of these functions.
        static void RegisterUser()
        {
            // TODO: implement (see Part 3 requirements)
            Console.Write("Name: "); string name = Console.ReadLine();
            Console.Write("Email: "); string email = Console.ReadLine();
            Console.Write("Password: "); string password = Console.ReadLine();

            if (context.Users.Any(u => u.Email == email))
            {
                Console.WriteLine("A user with that email already exists");
                return;
            }

            var user = new User { Name = name, Email = email, PasswordHash = password };
            context.Users.Add(user);
            context.SaveChanges();
            Console.WriteLine("User registered with Id " + user.UserId);
        }
        static void Login()
        {
            // TODO: implement - on success, set loggedInUserId = <found user's Id>
            Console.Write("Email: "); string email = Console.ReadLine();
            Console.Write("Password: "); string password = Console.ReadLine();

            var user = context.Users.FirstOrDefault(u => u.Email == email && u.PasswordHash == password);
            if (user == null)
            {
                Console.WriteLine("Invalid email or password");
                return;
            }

            loggedInUserId = user.UserId;
            Console.WriteLine("Welcome back "+ user.Name);
        }
        static void AddCategory()
        {
            // TODO: implement
            Console.Write("Category name: "); string name = Console.ReadLine();
            Console.Write("Description: "); string desc = Console.ReadLine();

            context.Categories.Add(new Category { Name = name, Description = desc });
            context.SaveChanges();
            Console.WriteLine("Category added");
        }
        static void AddProduct()
        {
            // TODO: implement
            var categories = context.Categories.ToList();
            if (!categories.Any())
            {
                Console.WriteLine("No categories exist yet add one first.");
                return;
            }

            Console.WriteLine("Categories:");
            foreach (var c in categories)
                Console.WriteLine(c.CategoryId + ") " + c.Name);

            Console.Write("Product name: "); string name = Console.ReadLine();
            Console.Write("Price: "); decimal price = decimal.Parse(Console.ReadLine());
            Console.Write("Stock: "); int stock = int.Parse(Console.ReadLine());
            Console.Write("Category Id: "); int catId = int.Parse(Console.ReadLine());

            if (!categories.Any(c => c.CategoryId == catId))
            {
                Console.WriteLine("Invalid category Id");
                return;
            }

            context.Products.Add(new Product
            {
                Name = name,
                Price = price,
                Stock = stock,
                CategoryId = catId
            });
            context.SaveChanges();
            Console.WriteLine("Product added");
        }
        static void ViewAllProducts()
        {
            // TODO: implement
        }
        static void PlaceOrder()
        {
            // TODO: implement - check loggedInUserId != 0 first
        }
        static void ViewMyOrders()
        {
            // TODO: implement - check loggedInUserId != 0 first
        }
        static void ViewOrderDetails()
        {
            // TODO: implement
        }
        static void AddReview()
        {
            // TODO: implement - check loggedInUserId != 0 first
        }
        static void ViewReviewsForProduct()
        {
        }
        // TODO: implement
        static void Logout()
        {
            // TODO: implement - reset loggedInUserId back to 0
        }
    }
}
