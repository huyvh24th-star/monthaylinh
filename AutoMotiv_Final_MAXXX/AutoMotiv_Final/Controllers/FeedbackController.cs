using System.Security.Claims;
using Core.Database.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly FeedbackService _feedbackService;

        public FeedbackController(FeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        // GET: /Feedback/Contact
        [AllowAnonymous]
        public IActionResult Contact()
        {
            return View(new ContactViewModel());
        }

        // POST: /Feedback/Contact — form liên hệ tư vấn (hỗ trợ cả anonymous và đã đăng nhập)
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(ContactViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            string title = $"Yêu cầu tư vấn từ {model.Name}";
            string content = $"Họ tên: {model.Name}\n" +
                             $"Email: {model.Email}\n" +
                             $"Điện thoại: {model.Phone}\n\n" +
                             $"Nội dung:\n{model.Message}";

            if (User.Identity?.IsAuthenticated == true)
            {
                // Khách đã đăng nhập → gắn với tài khoản
                var userId = GetCurrentUserId();
                await _feedbackService.SubmitFeedbackAsync(userId, title, content);
            }
            else
            {
                // Khách vãng lai → lưu guest fields, UserId = NULL (không FK violation)
                await _feedbackService.SubmitGuestFeedbackAsync(
                    guestName: model.Name,
                    guestEmail: model.Email,
                    guestPhone: model.Phone,
                    title: title,
                    content: content);
            }

            TempData["Success"] = "Cảm ơn bạn! Chuyên viên AutoMotiv sẽ liên hệ trong vòng 30 phút.";
            return RedirectToAction("Contact");
        }

        // GET: /Feedback/MyFeedbackPartial — AJAX lịch sử phản hồi
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> MyFeedbackPartial()
        {
            var userId = GetCurrentUserId();
            var feedbacks = await _feedbackService.GetFeedbacksByUserAsync(userId);
            var model = feedbacks.Select(MapToViewModel).ToList();
            return PartialView("_FeedbackHistoryPartial", model);
        }

        // POST: /Feedback/Submit — AJAX gửi feedback nhanh (từ form AJAX, giữ để tương thích)
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit([FromBody] FeedbackCreateViewModel model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Content))
                return Json(new { success = false, message = "Vui lòng nhập nội dung phản hồi" });

            var userId = GetCurrentUserId();
            var result = await _feedbackService.SubmitFeedbackAsync(userId, model.Title, model.Content);
            return Json(new { success = result.Success, message = result.Message });
        }

        private static FeedbackViewModel MapToViewModel(Core.Database.Models.Feedback f) => new()
        {
            Id = f.Id,
            Username = f.User?.Username ?? f.GuestName ?? "Khách vãng lai",
            Title = f.Title,
            Content = f.Content,
            AdminReply = f.AdminReply,
            CreatedDate = f.CreatedDate,
            ReplyDate = f.ReplyDate
        };

        private int GetCurrentUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            return claim != null ? int.Parse(claim.Value) : 0;
        }
    }
}
