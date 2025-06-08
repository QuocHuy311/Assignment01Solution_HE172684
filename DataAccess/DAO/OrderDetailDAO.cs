using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class OrderDetailDAO
    {
        private static OrderDetailDAO instance = null;
        private static readonly object instanceLock = new object();

        private OrderDetailDAO() { }

        public static OrderDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                        instance = new OrderDetailDAO();
                    return instance;
                }
            }
        }

        public List<OrderDetail> GetOrderDetailList()
        {
            using var context = new EStoreContext();
            return context.OrderDetails.ToList();
        }

        public OrderDetail GetOrderDetailById(int id)
        {
            using var context = new EStoreContext();
            return context.OrderDetails.FirstOrDefault(p => p.OrderId == id);
        }

        public void AddOrderDetail(OrderDetail p)
        {
            using var context = new EStoreContext();
            context.OrderDetails.Add(p);
            context.SaveChanges();
        }

        public void UpdateOrderDetail(OrderDetail p)
        {
            using var context = new EStoreContext();
            context.OrderDetails.Update(p);
            context.SaveChanges();
        }

        public void DeleteOrderDetail(int id)
        {
            using var context = new EStoreContext();
            var orderDetail = context.OrderDetails.Find(id);
            if (orderDetail != null)
            {
                context.OrderDetails.Remove(orderDetail);
                context.SaveChanges();
            }
        }
    }
}
