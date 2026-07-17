using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    /// <summary>
    /// ViewModel cho form liên hệ / tư vấn xe trên trang Contact.
    /// Gồm thông tin người gửi và nội dung yêu cầu.
    /// </summary>
    public class ContactViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
        [Display(Name = "Họ và tên")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập nội dung")]
        [Display(Name = "Nội dung / Mẫu xe quan tâm")]
        public string Message { get; set; } = string.Empty;
    }

    /// <summary>
    /// ViewModel hiển thị phản hồi của khách hàng trên trang Liên hệ.
    /// </summary>
    public class FeedbackViewModel
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? AdminReply { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ReplyDate { get; set; }
    }

    /// <summary>
    /// ViewModel cho form gửi phản hồi từ khách hàng.
    /// </summary>
    public class FeedbackCreateViewModel
    {
        [Required(ErrorMessage = "Vui lòng chọn chủ đề")]
        [Display(Name = "Chủ đề")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập nội dung phản hồi")]
        [Display(Name = "Nội dung")]
        public string Content { get; set; } = string.Empty;
    }
}
