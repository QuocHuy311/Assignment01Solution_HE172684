using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class ProductDAO
    {
        private static ProductDAO instance = null; 
        private static readonly object instanceLock = new object();

        private ProductDAO() { }

        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                        instance = new ProductDAO();
                    return instance;
                }
            }
        }

        public List<Product> GetProductList()
        {
            using var context = new EStoreContext();
            return context.Products.Include(p => p.Category).ToList();
        }

        public Product GetProductById(int id)
        {
            using var context = new EStoreContext();
            return context.Products.FirstOrDefault(p => p.ProductId == id);
        }

        public void AddProduct(Product p)
        {
            using var context = new EStoreContext();
            context.Products.Add(p);
            context.SaveChanges();
        }

        public void UpdateProduct(Product p)
        {
            using var context = new EStoreContext();
            context.Products.Update(p);
            context.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            using var context = new EStoreContext();
            var product = context.Products.Find(id);
            if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
            }
        }
    }
}
