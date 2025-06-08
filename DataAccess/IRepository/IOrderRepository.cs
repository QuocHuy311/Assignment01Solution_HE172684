using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IOrderRepository
    {
        List<Order> GetOrders();
        Order GetOrderById(int id);
        void InsertOrder(Order p);
        void UpdateOrder(Order p);
        void DeleteOrder(int id);
    }
}
