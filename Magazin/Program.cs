using System;
using System.Linq;
using Magazin.Context;
using Magazin.Models;
using Microsoft.EntityFrameworkCore;

namespace Magazin
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new StoreContext())
            {
                db.Database.EnsureCreated();
                MainMenu(db);
            }
        }

        static void MainMenu(StoreContext db)
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Добре дошли");
                Console.WriteLine();
                Console.WriteLine("1) Закупуване на стоки");
                Console.WriteLine("2) Справки");
                Console.WriteLine("3) Добавяне на нов артикул");
                Console.WriteLine("4) Добавяне на нов клиент");
                
                Console.ForegroundColor = ConsoleColor.Magenta;
                var choice = Console.ReadLine();
                if (choice == "1") PurchaseMenu(db);
                else if (choice == "2") ReportsMenu(db);
                else if (choice == "3") AddProduct(db);
                else if (choice == "4") AddCustomer(db);
                break;
            }
        }

        static void PurchaseMenu(StoreContext db)
        {

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Закупуване на стоки:");
                foreach (var product in db.Products.OrderBy(p => p.Description))
                {
                    Console.WriteLine($"{product.Description} - {product.RetailPrice} лв. ({product.ProductId}.)");
                }
                Console.WriteLine();
                Console.WriteLine("Въведи покупка (например 5x3, тоест 5 бройки от 3-ти артикул) или 'exit' за връщане в главното меню:");

                string input = Console.ReadLine();

                if (input == "exit")
                {
                    exit = true;
                    MainMenu(db);
                    continue;
                }

                try
                {
                    var parts = input.Split(new[] { '>', 'x', '+' }, StringSplitOptions.RemoveEmptyEntries);
                    var sales = new Sale[parts.Length / 2];

                    for (int i = 0; i < parts.Length; i += 2)
                    {
                        var quantity = int.Parse(parts[i]);
                        var productId = int.Parse(parts[i + 1]);
                        sales[i / 2] = new Sale { ProductId = productId, Quantity = quantity, Date = DateTime.Now };
                    }

                    Console.WriteLine("Въведи клиентско ID:");
                    var customerId = int.Parse(Console.ReadLine());

                    foreach (var sale in sales)
                    {
                        sale.CustomerId = customerId;
                        db.Sales.Add(sale);
                    }

                    db.SaveChanges();
                    Console.WriteLine("Покупката е добавена успешно!");
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Възникна грешка: {ex.Message}");
                }
            }
        }

        static void ReportsMenu(StoreContext db)
        {
            Console.WriteLine("1) Топ 3 клиента по оборот");
            Console.WriteLine("2) Покупки по клиентско ID");
            Console.WriteLine("3) Хит стока");
            var choice = Console.ReadLine();
            if (choice == "1") TopCustomersReport(db);
            else if (choice == "2") CustomerPurchasesReport(db);
            else if (choice == "3") BestSellingProductReport(db);
        }

        static void AddProduct(StoreContext db)
        {
            Console.WriteLine("Описание:");
            var description = Console.ReadLine();
            Console.WriteLine("Категория:");
            var category = Console.ReadLine();
            Console.WriteLine("Цена на едро:");
            var wholesalePrice = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Цена на която продаваме:");
            var retailPrice = decimal.Parse(Console.ReadLine());
            db.Products.Add(new Product
            {
                Description = description,
                Category = category,
                WholesalePrice = wholesalePrice,
                RetailPrice = retailPrice
            });
            db.SaveChanges();
        }

        static void AddCustomer(StoreContext db)
        {
            Console.WriteLine("First Name:");
            var firstName = Console.ReadLine();
            Console.WriteLine("Last Name:");
            var lastName = Console.ReadLine();
            db.Customers.Add(new Customer { FirstName = firstName, LastName = lastName });
            db.SaveChanges();
        }

        static void TopCustomersReport(StoreContext db)
        {
            var sales = db.Sales
          .Include(s => s.Product)
          .Include(s => s.Customer)
          .ToList();

            var topClients = sales
                .GroupBy(s => s.CustomerId)
                .Select(g => new
                {
                    CustomerId = g.Key,
                    TotalSpent = g.Sum(s => s.Quantity * s.Product.RetailPrice)
                })
                .OrderByDescending(c => c.TotalSpent)
                .Take(3)
                .ToList();

            Console.WriteLine("Топ 3 клиенти в магазина");
            foreach (var client in topClients)
            {
                var customer = db.Customers.Find(client.CustomerId);
                Console.WriteLine($"Клиент: {customer.FirstName} {customer.LastName}, Похарчил: {client.TotalSpent:C}");
            }
        }

        static void CustomerPurchasesReport(StoreContext db)
        {
            Console.WriteLine("Въведи клиентско ID:");
            if (!int.TryParse(Console.ReadLine(), out int customerId))
            {
                Console.WriteLine("Невалидно ID.");
                return;
            }

            var customer = db.Customers.Find(customerId);
            if (customer == null)
            {
                Console.WriteLine("Клиентът не е намерен.");
                return;
            }

            var sales = db.Sales
                .Include(s => s.Product)
                .Include(s => s.Customer)
                .Where(s => s.CustomerId == customerId)
                .ToList();

            if (sales.Count == 0)
            {
                Console.WriteLine("Този клиент няма покупки.");
                return;
            }

            Console.WriteLine($"Покупките на {customer.FirstName} {customer.LastName}:");
            foreach (var sale in sales)
            {
                Console.WriteLine($"Продукт: {sale.Product.Description}, Количество: {sale.Quantity}, Похарчил: {sale.Quantity * sale.Product.RetailPrice:C}, Дата: {sale.Date}");
            }
        }

        static void BestSellingProductReport(StoreContext db)
        {
            var sales = db.Sales
            .Include(s => s.Product)
            .ToList();

            var bestSellingProduct = sales
                .GroupBy(s => s.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    TotalRevenue = g.Sum(s => s.Quantity * s.Product.RetailPrice)
                })
                .OrderByDescending(p => p.TotalRevenue)
                .FirstOrDefault();

            if (bestSellingProduct != null)
            {
                var product = db.Products.Find(bestSellingProduct.ProductId);
                Console.WriteLine($"Най-продаван продукт: {product.Description}");
                Console.WriteLine($"Оборот: {bestSellingProduct.TotalRevenue:C}");
            }
            else
            {
                Console.WriteLine("Няма продажби :( .");
            }
        }
    }
}

