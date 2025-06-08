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
    public class OrderRepository : IOrderRepository
    {
        public List<Order> GetOrders() => OrderDAO.Instance.GetOrderList();

        public Order GetOrderById(int id) => OrderDAO.Instance.GetOrderById(id);

        public void InsertOrder(Order p) => OrderDAO.Instance.AddOrder(p);

        public void UpdateOrder(Order p) => OrderDAO.Instance.UpdateOrder(p);

        public void DeleteOrder(int id) => OrderDAO.Instance.DeleteOrder(id);
    }
}
