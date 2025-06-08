using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories();
        Category GetCategoryById(int id);
        void InsertCategory(Category p);
        void UpdateCategory(Category p);
        void DeleteCategory(int id);
    }
}
