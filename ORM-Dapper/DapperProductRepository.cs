using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Dapper;

public class DapperProductRepository(IDbConnection conn) : IProductRepository
{
    private readonly IDbConnection _conn = conn;

    public IEnumerable<Product> GetAllProducts()
    {
        return _conn.Query<Product>("SELECT * FROM Products;");
    }

    public Product GetProductById(int id)
    {
        return _conn.QuerySingle<Product>("SELECT * FROM Products WHERE ProductID = @Id;",
            new { Id = id }
        );
    }

    public void UpdateProduct(Product product)
    {
        _conn.Execute("UPDATE products" +
            " SET name = @name," +
            " Price = @price," + 
            " CategoryID = @catID," + 
            " OnSale = @onSale," + 
            " StockLevel = @stock" +
            " WHERE ProductID = @id",
           new
           {
               id = product.ProductID,
               name = product.Name,
               price = product.Price,
               catID = product.CategoryID,
               onSale = product.OnSale,
               stock = product.StockLevel
           });
    }
}

