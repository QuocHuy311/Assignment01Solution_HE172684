using BusinessObject.DTO;
using BusinessObject.Models;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eStoreAPI.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderAPI : ControllerBase
    {
        private readonly IOrderRepository _orderRepository = new OrderRepository();
        private readonly IProductRepository _productRepository = new ProductRepository();
        private readonly IMemberRepository _memberRepository = new MemberRepository();

        [HttpGet]
        public IActionResult GetOrders()
        {
            var role = Request.Headers["Role"].ToString();
            var memberIdHeader = Request.Headers["MemberId"].ToString();

            var orders = _orderRepository.GetOrders();

            if (role == "Member" && int.TryParse(memberIdHeader, out int memberId))
            {
                orders = orders.Where(o => o.MemberId == memberId).ToList();
            }

            // Chuyển sang DTO
            var orderDTOs = orders.Select(o => new OrderDTO
            {
                OrderId = o.OrderId,
                MemberId = o.MemberId,
                OrderDate = o.OrderDate,
                RequiredDate = o.RequiredDate,
                ShippedDate = o.ShippedDate,
                Freight = o.Freight,
                MemberEmail = o.Member?.Email, // Lấy từ navigation property

                OrderDetails = o.OrderDetails.Select(od => new OrderDetailDTO
                {
                    ProductId = od.ProductId,
                    Quantity = od.Quantity,
                    Discount = od.Discount
                }).ToList()
            }).ToList();

            return Ok(orderDTOs);
        }


        [HttpPost]
        public IActionResult CreateOrder(OrderDTO orderDTO)
        {
            if (orderDTO == null || orderDTO.OrderDetails.Count == 0)
                return BadRequest("Order must have at least one product");

            var order = MapToOrder(orderDTO);

            foreach (var detail in order.OrderDetails)
            {
                var product = _productRepository.GetProductById(detail.ProductId);
                if (product == null)
                    return BadRequest($"Product ID {detail.ProductId} not found.");


                product.UnitsInStock -= detail.Quantity ?? 0;
                _productRepository.UpdateProduct(product);
            }

            _orderRepository.InsertOrder(order);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {
            var order = _orderRepository.GetOrderById(id);
            if (order == null) return NotFound();

            var products = _productRepository.GetProducts();
            var member = _memberRepository.GetMemberById(order.MemberId ?? 0);

            var viewModel = new OrderViewModel
            {
                OrderId = order.OrderId,
                MemberEmail = member?.Email,
                OrderDate = order.OrderDate,
                RequiredDate = order.RequiredDate,
                ShippedDate = order.ShippedDate,
                Freight = order.Freight,
                OrderDetails = order.OrderDetails.Select(od =>
                {
                    var product = products.FirstOrDefault(p => p.ProductId == od.ProductId);
                    return new OrderDetailViewModel
                    {
                        ProductId = od.ProductId,
                        ProductName = product?.ProductName ?? "Unknown",
                        UnitPrice = product?.UnitPrice,
                        Quantity = od.Quantity,
                        Discount = od.Discount
                    };
                }).ToList()
            };

            return Ok(viewModel);
        }



        private Order MapToOrder(OrderDTO dto)
        {
            var order = new Order
            {
                OrderId = dto.OrderId,
                MemberId = dto.MemberId,
                OrderDate = DateTime.Now,
                RequiredDate = DateTime.Now,
                ShippedDate = DateTime.Now,
                Freight = dto.Freight,
                OrderDetails = dto.OrderDetails.Select(d =>
                {
                    var product = _productRepository.GetProductById(d.ProductId);
                    return new OrderDetail
                    {
                        ProductId = d.ProductId,
                        Quantity = d.Quantity,
                        Discount = d.Discount,
                        UnitPrice = product?.UnitPrice ?? 0
                    };
                }).ToList()
            };

            return order;
        }

    }
}
