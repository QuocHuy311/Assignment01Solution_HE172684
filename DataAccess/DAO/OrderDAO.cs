using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class OrderDAO
    {
        private static OrderDAO instance = null;
        private static readonly object instanceLock = new object();

        private OrderDAO() { }

        public static OrderDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                        instance = new OrderDAO();
                    return instance;
                }
            }
        }

        public List<Order> GetOrderList()
        {
            using var context = new EStoreContext();
            return context.Orders.ToList();
        }

        public Order GetOrderById(int id)
        {
            using var context = new EStoreContext();
            return context.Orders.FirstOrDefault(m => m.OrderId == id);
        }

        public void AddOrder(Order o)
        {
            using var context = new EStoreContext();
            context.Orders.Add(o);
            context.SaveChanges();
        }

        public void UpdateOrder(Order o)
        {
            using var context = new EStoreContext();
            context.Orders.Update(o);
            context.SaveChanges();
        }

        public void DeleteOrder(int id)
        {
            using var context = new EStoreContext();
            var order = context.Orders.Find(id);
            if (order != null)
            {
                context.Orders.Remove(order);
                context.SaveChanges();
            }
        }
    }
}
