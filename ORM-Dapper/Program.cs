using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using ORM_Dapper;
using System.Data;

var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

string? connString = config.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connString))
{
    throw new InvalidOperationException("The connection string 'DefaultConnection' is not configured or is null.");
}

IDbConnection conn = new MySqlConnection(connString);

#region
var departmentRepository = new DapperDepartmentRepository(conn);

departmentRepository.InsertDepartment("HR");

var departments = departmentRepository.GetAllDepartments();

foreach (var department in departments)
{
    Console.WriteLine(department.DepartmentID);
    Console.WriteLine(department.Name);
    Console.WriteLine();
}
#endregion

var productRepository = new DapperProductRepository(conn);

var productToUpdate = productRepository.GetProductById(943);

productToUpdate.Name = "Updated Product Name";
productToUpdate.Price = 19.99;
productToUpdate.CategoryID = 2;
productToUpdate.OnSale = true;
productToUpdate.StockLevel = 50;

productRepository.UpdateProduct(productToUpdate);

var products = productRepository.GetAllProducts();

foreach (var product in products)
{
    Console.WriteLine(product.ProductID);
    Console.WriteLine(product.Name);
    Console.WriteLine(product.Price);
    Console.WriteLine(product.CategoryID);
    Console.WriteLine(product.OnSale);
    Console.WriteLine(product.StockLevel);
    Console.WriteLine();
    Console.WriteLine();
}