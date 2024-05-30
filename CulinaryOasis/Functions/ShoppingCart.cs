using CulinaryOasis.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CulinaryOasis.Functions
{
    public class ShoppingCart
    {
        private CulinaryOasisEntities4 _context;

        public ShoppingCart(CulinaryOasisEntities4 context)
        {
            _context = context;
        }

        public void AddToCart(int userId, int dishId, int quantity)
        {
            // Найти активный заказ пользователя или создать новый
            var order = _context.Order.FirstOrDefault(o => o.UserID == userId && o.Status == "Active");
            if (order == null)
            {
                order = new Order
                {
                    UserID = userId,
                    OrderDate = DateTime.Now,
                    Status = "Active",
                    TotalPrice = 0
                };
                _context.Order.Add(order);
                _context.SaveChanges();
            }

            // Добавить товар в заказ
            var orderDetail = new OrderDetails
            {
                OrderID = order.OrderID,
                DishID = dishId,
                Quantity = quantity
            };
            _context.OrderDetails.Add(orderDetail);

            // Обновить общую цену заказа
            var dish = _context.Dish.Find(dishId);
            if (dish != null)
            {
                order.TotalPrice += dish.Price * quantity;
            }

            _context.SaveChanges();
        }

        public void RemoveFromCart(int? orderDetailsId)
        {
                var orderDetail = _context.OrderDetails.Find(orderDetailsId);
                if (orderDetail != null)
                {
                    var order = _context.Order.Find(orderDetail.OrderID);
                    if (order != null)
                    {
                        // Обновить общую цену заказа
                        var dish = _context.Dish.Find(orderDetail.DishID);
                        if (dish != null)
                        {
                            order.TotalPrice -= dish.Price * orderDetail.Quantity;
                        }

                        _context.OrderDetails.Remove(orderDetail);
                        _context.SaveChanges();
                    }
                }
        }

        public IQueryable<OrderDetails> GetCartItems(int userId)
        {
            var order = _context.Order.FirstOrDefault(o => o.UserID == userId && o.Status == "Active");
            if (order != null)
            {
                return _context.OrderDetails.Where(od => od.OrderID == order.OrderID);
            }
            return Enumerable.Empty<OrderDetails>().AsQueryable();
        }

        public void Checkout(int userId, TimeSpan? time)
        {
            var order = _context.Order.FirstOrDefault(o => o.UserID == userId && o.Status == "Active");
            if (order != null)
            {
                order.Status = "Completed";
                order.Time = time;  // Установить время заказа
                _context.SaveChanges();
            }
        }
    }
}
