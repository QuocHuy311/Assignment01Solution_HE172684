using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public string? MemberEmail { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public decimal? Freight { get; set; }

        public List<OrderDetailViewModel> OrderDetails { get; set; } = new();
    }

}
