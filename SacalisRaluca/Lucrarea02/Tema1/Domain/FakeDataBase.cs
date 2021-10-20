using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L01.Fake
{
    public static class FakeDataBase
    {
        public static IEnumerable<Product> LoadProducts()
        {
            return new List<Product>
            {
                new Product(1, "Produsul 1", 39.99f),
                new Product(2, "Produsul 2", 1.99f),
                new Product(3, "Produsul 3", 0.59f),
                new Product(4, "Produsul 4", 18.00f),
                new Product(5, "Produsul 5", 199.89f),
            };
        }
    }
}
