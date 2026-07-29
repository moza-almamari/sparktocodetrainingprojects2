
using System;
using System.Linq;
using E_Commerce_Database.Models;
using Microsoft.EntityFrameworkCore;
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
                    //case 5: ViewAllProducts(); break;
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
       /* static void ViewAllProducts()
        {
            // TODO: implementConsole.Write("Filter by Category Id (blank for all): ");
            string input = Console.ReadLine();

            var query = context.Products.Include(p => p.Category).AsQueryable();
            if (int.TryParse(input, out int catId))
                query = query.Where(p => p.CategoryId == catId);

            foreach (var p in query.ToList())
                Console.WriteLine($"[{p.ProductId}] {p.Name} - {p.Price:C} ({p.Category.Name})");
        } */
        static void PlaceOrder()
        {
            // TODO: implement - check loggedInUserId != 0 first
            if (loggedInUserId == 0)
            {
                Console.WriteLine("You must be logged in to place an order.");
                return;
            }

            var order = new Order { UserId = loggedInUserId, OrderDate = DateTime.Now };
            context.Orders.Add(order);

            bool addingMore = true;
            while (addingMore)
            {
                Console.Write("Product Id to add (0 to finish): ");
                int productId = int.Parse(Console.ReadLine());
                if (productId == 0) { addingMore = false; continue; }

                var product = context.Products.Find(productId);
                if (product == null)
                {
                    Console.WriteLine("No such product");
                    continue;
                }

                Console.Write("Quantity: ");
                int qty = int.Parse(Console.ReadLine());

                order.OrderProducts.Add(new OrderProduct { ProductId = productId, Quantity = qty });
            }

            if (!order.OrderProducts.Any())
            {
                Console.WriteLine("No products selected - order cancelled");
                context.Orders.Remove(order);
                return;
            }

            context.SaveChanges();
            Console.WriteLine($"Order {order.OrderId} placed with {order.OrderProducts.Count} product line");   



        }
        static void ViewMyOrders()
        {
            // TODO: implement - check loggedInUserId != 0 first
            if (loggedInUserId == 0)
            {
                Console.WriteLine("You must be logged in");
                return;
            }

            var orders = context.Orders
                .Where(o => o.UserId == loggedInUserId)
                .ToList();

            foreach (var o in orders)
                Console.WriteLine($"Order {o.OrderId} - {o.OrderDate:d}");
        }
        static void ViewOrderDetails()
        {
            // TODO: implement
            Console.Write("Order Id: "); int orderId = int.Parse(Console.ReadLine());

            var order = context.Orders
                .Include(o => o.OrderProducts).ThenInclude(op => op.Product)
                .Include(o => o.Review)
                .FirstOrDefault(o => o.OrderId == orderId);

            if (order == null)
            {
                Console.WriteLine("Order not found");
                return;
            }

            decimal total = 0;
            Console.WriteLine($"Order {order.OrderId} - {order.OrderDate:d}");
            foreach (var line in order.OrderProducts)
            {
                var lineTotal = line.Product.Price * line.Quantity;
                total += lineTotal;
                Console.WriteLine($"  {line.Product.Name} x{line.Quantity} = {lineTotal:C}");
            }
            Console.WriteLine($"Total: {total:C}");

            Console.WriteLine(order.Review == null
                ? "No review yet"
                : $"Review: {order.Review.Rating}/5 - {order.Review.Comment}");
        }
        static void AddReview()
        {
            // TODO: implement - check loggedInUserId != 0 first
            if (loggedInUserId == 0)
            {
                Console.WriteLine("You must be logged in");
                return;
            }

            Console.Write("Order Id: "); int orderId = int.Parse(Console.ReadLine());
            var order = context.Orders.Include(o => o.Review)
                .FirstOrDefault(o => o.OrderId == orderId);

            if (order == null || order.UserId != loggedInUserId)
            {
                Console.WriteLine("Order not found or does not belong to you");
                return;
            }

            if (order.Review != null)
            {
                Console.WriteLine("This order already has a review");
                return;
            }

            Console.Write("Rating (1-5): "); int rating = int.Parse(Console.ReadLine());
            Console.Write("Comment: "); string comment = Console.ReadLine();

            context.Reviews.Add(new Review { OrderId = orderId, Rating = rating, Comment = comment });
            context.SaveChanges();
            Console.WriteLine("Review added");
        }
        static void ViewReviewsForProduct()
        {
            // TODO: implement
            Console.Write("Product Id: "); int productId = int.Parse(Console.ReadLine());

            var reviews = context.OrderProducts
                .Where(op => op.ProductId == productId)
                .Select(op => op.Order.Review)
                .Where(r => r != null)
                .ToList();

            if (!reviews.Any())
            {
                Console.WriteLine("No reviews yet for this product");
                return;
            }
            foreach (var r in reviews)
                Console.WriteLine($"  {r.Rating}/5 - {r.Comment} (Order {r.OrderId})");
        }

        static void Logout()
        {
            // TODO: implement - reset loggedInUserId back to 0
            loggedInUserId = 0;
            Console.WriteLine("Logged out.");
        }
    }
}
