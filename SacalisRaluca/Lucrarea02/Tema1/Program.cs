using LanguageExt;
using System;
using L01.Fake;
using System.Linq;
using static L01.Domain.AppDomain;
using static L01.Domain.CartState;
using L01.Extensions;
using L01.Domain;

namespace L01
{
    class Program
    {
        static void Main(string[] args)
        {
            var cart = (CreateCart() as EmptyCart).Cart;
            while (true)
            {
                var (product, quantity) = RequestItem();
                var result = AddItemToCart(cart, product, quantity);
                var message = string.Empty;
                (cart, message) = result switch
                {
                    ValidCart a => (a.Cart, ""),
                    InvalidCart a => (a.Cart, a.Message + (Environment.NewLine * (StringMultiplication)2))
                };
                Displayitems(cart);

                Console.Write(message);
                if (RequestPayment())
                {
                    PayCart(cart);
                    Console.WriteLine($"Good day");
                    break;
                }
            }
        }

        public static Unit Displayitems(Cart cart)
        {
            try
            {
                Console.WriteLine("Your items:");

                Console.Write(cart.Items
                .OrderBy(a => a.Product.Id)
                .Select(a => $"\t{a.Product.Id} / {a.Product.Name} / {a.Product.Price} / {a.Quantity}" + Environment.NewLine)
                .Aggregate((a, b) => a + b));

                Console.Write("Total price: ");
                Console.WriteLine(cart.Items.Select(a => a.Product.Price * a.Quantity).Aggregate((a, b) => a + b));
                Console.WriteLine();
               
            }
            catch (Exception)
            {
                
            }

            return Unit.Default;
            
            
        }

        public static string RequestCredentials()
        {
            Console.Write("Please enter password: ");
            return Console.ReadLine();
        }

        public static (Option<Product>, int) RequestItem()
        {
            var products = FakeDataBase.LoadProducts();
            products.Iter(a => Console.WriteLine($"Id: {a.Id}, Price: {a.Price}, Name: {a.Name}"));
            Console.WriteLine("Please select an item by it's Id and then input the quantity");
            Console.Write("Product Id: ");
            var productId = Console.ReadLine().Trim();

            var product = products.FirstOrDefault(a => a.Id.ToString() == productId);

            if (product != null)
            {
                Console.Write("Quantity: ");
                var quantity = int.Parse(Console.ReadLine().Trim());

                return (product, quantity);
            }
            else
            {
                Console.WriteLine("The id does not exist");
                return (product, 0);
            }
        }

        public static bool RequestPayment()
        {
            Console.WriteLine("Please insert \"Y\" if you want to pay or anything else to continue shopping:");

            var response = Console.ReadLine().Trim().ToLower();

            if(response == "y")
            {
                return true;
            }

            Console.Clear();
            return false;
        }
    }
}
