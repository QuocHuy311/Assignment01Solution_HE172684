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
    public class CategoryRepository : ICategoryRepository
    {
        public List<Category> GetCategories() => CategoryDAO.Instance.GetCategoryList();

        public Category GetCategoryById(int id) => CategoryDAO.Instance.GetCategoryById(id);

        public void InsertCategory(Category p) => CategoryDAO.Instance.AddCategory(p);

        public void UpdateCategory(Category p) => CategoryDAO.Instance.UpdateCategory(p);

        public void DeleteCategory(int id) => CategoryDAO.Instance.DeleteCategory(id);
    }
}
