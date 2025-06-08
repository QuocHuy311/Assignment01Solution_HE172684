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
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public List<OrderDetail> GetOrderDetails() => OrderDetailDAO.Instance.GetOrderDetailList();

        public OrderDetail GetOrderDetailById(int id) => OrderDetailDAO.Instance.GetOrderDetailById(id);

        public void InsertOrderDetail(OrderDetail p) => OrderDetailDAO.Instance.AddOrderDetail(p);

        public void UpdateOrderDetail(OrderDetail p) => OrderDetailDAO.Instance.UpdateOrderDetail(p);

        public void DeleteOrderDetail(int id) => OrderDetailDAO.Instance.DeleteOrderDetail(id);
    }
}
