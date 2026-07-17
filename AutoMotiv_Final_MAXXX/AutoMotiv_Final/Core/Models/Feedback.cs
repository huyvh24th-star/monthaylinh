using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Database.Models
{
    /// <summary>
    /// Entity đại diện cho phản hồi / góp ý của khách hàng và phản hồi của admin.
    /// Hỗ trợ cả user đã đăng nhập (UserId != null) và khách vãng lai (GuestName/Email/Phone).
    /// Bảng: Feedbacks
    /// </summary>
    [Table("Feedbacks")]
    public class Feedback
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Khóa ngoại tới User. NULL nếu khách gửi anonymous (không cần đăng nhập).
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>Họ tên khách vãng lai (nullable — chỉ dùng khi UserId == null).</summary>
        [MaxLength(200)]
        public string? GuestName { get; set; }

        /// <summary>Email khách vãng lai.</summary>
        [MaxLength(200)]
        public string? GuestEmail { get; set; }

        /// <summary>SĐT khách vãng lai.</summary>
        [MaxLength(20)]
        public string? GuestPhone { get; set; }

        /// <summary>Chủ đề phản hồi.</summary>
        [Required(ErrorMessage = "Chủ đề là bắt buộc")]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        /// <summary>Nội dung phản hồi của khách hàng.</summary>
        [Required(ErrorMessage = "Nội dung phản hồi là bắt buộc")]
        public string Content { get; set; } = string.Empty;

        /// <summary>Nội dung admin trả lời. Null nếu chưa được trả lời.</summary>
        public string? AdminReply { get; set; }

        /// <summary>Thời điểm khách hàng gửi phản hồi.</summary>
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        /// <summary>Thời điểm admin trả lời. Null nếu chưa được trả lời.</summary>
        public DateTime? ReplyDate { get; set; }

        /// <summary>Navigation property: người dùng đã gửi phản hồi này (null nếu anonymous).</summary>
        [ForeignKey(nameof(UserId))]
        public virtual User? User { get; set; }

        /// <summary>Helper: tên hiển thị (ưu tiên account name, sau đó guest name).</summary>
        [NotMapped]
        public string DisplayName =>
            User?.FullName ?? User?.Username ?? GuestName ?? "Khách vãng lai";

        /// <summary>Helper: email hiển thị.</summary>
        [NotMapped]
        public string? DisplayEmail =>
            User?.Email ?? GuestEmail;

        /// <summary>Helper: SĐT hiển thị.</summary>
        [NotMapped]
        public string? DisplayPhone =>
            User?.Phone ?? GuestPhone;
    }
}
