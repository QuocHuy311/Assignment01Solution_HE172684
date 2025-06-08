using BusinessObject.Models;
using DataAccess.DAO;
using DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        public List<Product> GetProducts() => ProductDAO.Instance.GetProductList();

        public Product GetProductById(int id) => ProductDAO.Instance.GetProductById(id);

        public void InsertProduct(Product p) => ProductDAO.Instance.AddProduct(p);

        public void UpdateProduct(Product p) => ProductDAO.Instance.UpdateProduct(p);

        public void DeleteProduct(int id) => ProductDAO.Instance.DeleteProduct(id);
    }
}
