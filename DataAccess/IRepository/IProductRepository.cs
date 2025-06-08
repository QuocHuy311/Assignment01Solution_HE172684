using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IProductRepository
    {
        List<Product> GetProducts();
        Product GetProductById(int id);
        void InsertProduct(Product p);
        void UpdateProduct(Product p);
        void DeleteProduct(int id);
    }
}
