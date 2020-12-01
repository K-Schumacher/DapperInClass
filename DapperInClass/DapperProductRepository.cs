using System;
using System.Collections.Generic;
using System.Data;
using Dapper;

namespace BestBuyCRUD
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;

        //constructor
        public DapperProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public void CreateProduct(string name, double price, int categoryID)
        {
            _connection.Execute("INSERT INTO products (Name, Price, CategoryID) " +
                                "VALUES (@name, @price, @categoryID);"
                , new { name = name, price = price, categoryID = categoryID });
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _connection.Query<Product>("SELECT * FROM products;");
        }

        public void UpdateProductName(int productID, string updatedName)
        {
            _connection.Execute("UPDATE products SET Name = @updatedName WHERE ProductID = @productID;",
                new { updatedName = updatedName, productID = productID });
        }

        public void DeleteProduct(int productID)
        {
            _connection.Execute("DELETE FROM reviews WHERE ProductID = @productID;",
                new { productID = productID });

            _connection.Execute("DELETE FROM sales WHERE ProductID = @productID;",
               new { productID = productID });

            _connection.Execute("DELETE FROM products WHERE ProductID = @productID;",
               new { productID = productID });
        }
    }
}