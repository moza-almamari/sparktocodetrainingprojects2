using EFCoreProject.Models;

namespace EFCoreProject
{
    public class Program
    {
        static void Main(string[] args)
        {
            ProjectContext context = new ProjectContext();

            //add data on table employee
            Employee e1 = new Employee();
            e1.Name = "John Doe";
            e1.Age = 30;
            e1.salary = 50000;
            context.employees.Add(e1);
            Console.WriteLine("Register employee");
            Employee e2 = new Employee();

            Console.WriteLine("enter name");
            e2.Name = Console.ReadLine();

            Console.WriteLine("enter age");
            e2.Age = int.Parse(Console.ReadLine());

            Console.WriteLine("enter salary");
            e2.salary = double.Parse(Console.ReadLine());

            context.employees.Add(e2);
            context.SaveChanges();


            //case 2 delete employee
            Console.WriteLine("enter employee ID to delete");
            int id = int.Parse(Console.ReadLine());

            Employee employee = context.employees.FirstOrDefault(e => e.EmployeeId == id);
            if (employee == null)
            {
                Console.WriteLine("employee not found");
            }
            else
            {
                context.employees.Remove(employee);
                context.SaveChanges();
            }
        }
    }
}
