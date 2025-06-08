using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance = null;
        private static readonly object instanceLock = new object();

        private CategoryDAO() { }

        public static CategoryDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                        instance = new CategoryDAO();
                    return instance;
                }
            }
        }

        public List<Category> GetCategoryList()
        {
            using var context = new EStoreContext();
            return context.Categories.ToList();
        }

        public Category GetCategoryById(int id)
        {
            using var context = new EStoreContext();
            return context.Categories.FirstOrDefault(c => c.CategoryId == id);
        }

        public void AddCategory(Category c)
        {
            using var context = new EStoreContext();
            context.Categories.Add(c);
            context.SaveChanges();
        }

        public void UpdateCategory(Category c)
        {
            using var context = new EStoreContext();
            context.Categories.Update(c);
            context.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            using var context = new EStoreContext();
            var category = context.Categories.Find(id);
            if (category != null)
            {
                context.Categories.Remove(category);
                context.SaveChanges();
            }
        }
    }
}
