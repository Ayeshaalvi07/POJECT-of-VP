using System;
using System.Collections.Generic;

class Product
{
    public string Name 
    {
        get;
        set;
    }
    public int Quantity
    {
        get;
        set;
    }
    public decimal Price
    {
        get;
        set;
    }

    public Product(string name, int quantity, decimal price)
    {
        Name = name;
        Quantity = quantity;
        Price = price;
    }
}

class ShoppingCart
{
    private List<Tuple<Product, int>> cart = new List<Tuple<Product, int>>();
    private const decimal discountRate = 0.10m; // 10% discount
    private const decimal salesTaxRate = 0.07m; // 7% sales tax

    public void AddToCart(Product product, int quantity)
    {
        if (product.Quantity >= quantity)
        {
            cart.Add(new Tuple<Product, int>(product, quantity));
            product.Quantity -= quantity;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n\t{quantity} x {product.Name} added to cart.\n");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n\tSorry, we only have {product.Quantity} of {product.Name} available.\n");
            Console.ResetColor();
        }
    }

    public void ViewCart()
    {
        if (cart.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\tYour cart is empty.\n");
            Console.ResetColor();
            return;
        }

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n\tItems in your cart:\n");
        Console.ResetColor();

        foreach (var item in cart)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\t{item.Item1.Name} - Quantity: {item.Item2}, Price per unit: {item.Item1.Price:C}");
            Console.ResetColor();
        }
    }

    public decimal CalculateTotal()
    {
        decimal totalPrice = 0m;

        foreach (var item in cart)
        {
            totalPrice += item.Item1.Price * item.Item2;
        }

        decimal discount = totalPrice * discountRate;
        decimal discountedPrice = totalPrice - discount;
        decimal salesTax = discountedPrice * salesTaxRate;

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\n\tSubtotal: {totalPrice:C}");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\tDiscount (10%): -{discount:C}");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\tSales Tax (7%): +{salesTax:C}\n");
        Console.ResetColor();

        return discountedPrice + salesTax;
    }

    public void Checkout()
    {
        decimal totalWithTaxAndDiscount = CalculateTotal();

        decimal userPayment;
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\tPlease enter the amount you are paying: ");
            Console.ResetColor();

            if (decimal.TryParse(Console.ReadLine().Trim(), out userPayment))
            {
                if (userPayment < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\tPayment cannot be negative. Please try again.");
                    Console.ResetColor();
                }
                else if (userPayment > totalWithTaxAndDiscount)
                {
                    decimal extraAmount = userPayment - totalWithTaxAndDiscount;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\tThank you for your payment. Your change is {extraAmount:C}.\n");
                    Console.ResetColor();
                    break;
                }
                else if (userPayment == totalWithTaxAndDiscount)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\tThank you for the exact payment. Transaction complete!\n");
                    Console.ResetColor();
                    break;
                }
                else
                {
                    decimal remaining = totalWithTaxAndDiscount - userPayment;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\tInsufficient payment. You still owe {remaining:C}. Please pay the remaining amount.\n");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\tInvalid input. Please enter a valid amount.\n");
                Console.ResetColor();
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Centered welcome message
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n\t\t\t*****************************************************");
        Console.WriteLine("\t\t\t*                                     \t            *");
        Console.WriteLine("\t\t\t*         Welcome to the Aura Book Store!           *");
        Console.WriteLine("\t\t\t*                                   \t            *");
        Console.WriteLine("\t\t\t*****************************************************");
        Console.ResetColor();

        // Ask if the user wants to shop
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("\n\tDo you want to shop from our store? (y/n): ");
        Console.ResetColor();
        string startShopping = Console.ReadLine().Trim().ToLower();

        // Proceed if the user chooses 'y'
        if (startShopping == "y")
        {
            List<Product> products = new List<Product>
            {
                new Product("Book 1: Harry Potter", 10, 15.99m),
                new Product("Book 2: Lord of the Rings", 8, 12.49m),
                new Product("Book 3: Game of Thrones", 5, 18.99m),
                new Product("Book 4: Sherlock Holmes", 7, 10.99m),
                new Product("Book 5: The Alchemist", 9, 9.99m),
                new Product("Book 6: Pride and Prejudice", 6, 8.99m),
                new Product("Book 7: Moby Dick", 3, 11.99m),
                new Product("Book 8: War and Peace", 4, 14.99m),
                new Product("Book 9: The Odyssey", 10, 16.99m),
                new Product("Book 10: The Iliad", 8, 13.99m)
            };

            ShoppingCart cart = new ShoppingCart();
            bool shopping = true;

            while (shopping)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\n\tDo you want to see the available products? (y/n): ");
                Console.ResetColor();
                string input = Console.ReadLine().Trim().ToLower();

                if (input == "y")
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("\n\t********** Available Products **********\n");
                    Console.ResetColor();

                    foreach (var product in products)
                    {
                        Console.WriteLine($"\t- {product.Name}");
                    }

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\n\tDo you want to know the quantity of the available products? (y/n): ");
                    Console.ResetColor();
                    input = Console.ReadLine().Trim().ToLower();

                    if (input == "y")
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("\n\t********** Quantity of Products **********\n");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Gray;
                        foreach (var product in products)
                        {
                            Console.WriteLine($"\t{product.Name} - Available Quantity: {product.Quantity}");
                        }
                        Console.ResetColor();
                    }

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\n\tDo you want to know the prices of the products? (y/n): ");
                    Console.ResetColor();
                    input = Console.ReadLine().Trim().ToLower();

                    if (input == "y")
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("\n\t********** Price of Products **********\n");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Gray;
                        foreach (var product in products)
                        {
                            Console.WriteLine($"\t{product.Name} - Price: {product.Price:C}");
                        }
                        Console.ResetColor();
                    }

                    bool addingProducts = true;

                    while (addingProducts)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("\n\tWhich product would you like to add to your cart? Please enter the full name of the book: ");
                        Console.ResetColor();
                        string productName = Console.ReadLine().Trim();
                        Product selectedProduct = products.Find(p => p.Name.Equals(productName, StringComparison.OrdinalIgnoreCase));

                        if (selectedProduct != null)
                        {
                            Console.Write($"\n\tHow many of {selectedProduct.Name} would you like?: ");
                            int quantity;

                            while (!int.TryParse(Console.ReadLine().Trim(), out quantity) || quantity <= 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\n\tPlease enter a valid quantity (greater than 0):");
                                Console.ResetColor();
                            }

                            cart.AddToCart(selectedProduct, quantity);
                            cart.ViewCart();

                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("\n\tDo you want to add another product? (y/n): ");
                            Console.ResetColor();
                            string addMore = Console.ReadLine().Trim().ToLower();

                            if (addMore == "n")
                            {
                                addingProducts = false;
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n\tProduct not found, please try again.");
                            Console.ResetColor();
                        }
                    }

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\n\tDo you want to checkout now? (y/n): ");
                    Console.ResetColor();
                    input = Console.ReadLine().Trim().ToLower();

                    if (input == "y")
                    {
                        cart.Checkout();
                        shopping = false;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n\tContinue shopping...");
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n\tThank you for visiting!");
                    Console.ResetColor();
                    shopping = false;
                }
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\tThank you for visiting the Aura Book Store!\n");
            Console.ResetColor();
        }
    }
}


