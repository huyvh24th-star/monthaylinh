using Core.Database.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Models;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly OrderService _orderService;
        private readonly ProductService _productService;
        private readonly UserService _userService;
        private readonly FeedbackService _feedbackService;

        public DashboardController(OrderService orderService, ProductService productService,
            UserService userService, FeedbackService feedbackService)
        {
            _orderService = orderService;
            _productService = productService;
            _userService = userService;
            _feedbackService = feedbackService;
        }

        public async Task<IActionResult> Index()
        {
            var totalRevenue = await _orderService.GetTotalRevenueAsync();
            var totalOrders = await _orderService.CountAllOrdersAsync();
            var pendingOrders = await _orderService.CountPendingOrdersAsync();
            var totalCustomers = await _userService.CountCustomersAsync();
            var allProducts = await _productService.GetAllProductsAsync();
            var recentOrders = await _orderService.GetRecentOrdersAsync(5);
            var allFeedbacks = await _feedbackService.GetAllFeedbacksAsync();

            var model = new DashboardViewModel
            {
                TotalRevenue = totalRevenue,
                TotalOrders = totalOrders,
                PendingOrders = pendingOrders,
                TotalCustomers = totalCustomers,
                TotalProducts = allProducts.Count,
                TotalFeedbacks = allFeedbacks.Count,
                UnrepliedFeedbacks = allFeedbacks.Count(f => string.IsNullOrEmpty(f.AdminReply)),
                RecentOrders = recentOrders.Select(o => new OrderAdminListViewModel
                {
                    Id = o.Id,
                    UserId = o.UserId,
                    CustomerUsername = o.User?.Username,
                    OrderDate = o.OrderDate,
                    Status = o.Status,
                    Total = o.Total
                }).ToList()
            };

            return View(model);
        }
    }
}
