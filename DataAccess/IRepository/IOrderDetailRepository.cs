using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IOrderDetailRepository
    {
        List<OrderDetail> GetOrderDetails();
        OrderDetail GetOrderDetailById(int id);
        void InsertOrderDetail(OrderDetail p);
        void UpdateOrderDetail(OrderDetail p);
        void DeleteOrderDetail(int id);
    }
}
