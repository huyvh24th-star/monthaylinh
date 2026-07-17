namespace Web.Models
{
    /// <summary>
    /// ViewModel hiển thị xe ô tô trên các trang khách hàng (trang chủ, danh sách, chi tiết).
    /// Không truyền Entity Product trực tiếp xuống View.
    /// </summary>
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; } = string.Empty;
        public string? Image { get; set; }
        public bool Featured { get; set; }
        public int Stock { get; set; }

        /// <summary>
        /// Giá đã định dạng theo VNĐ, tự động hiển thị tỷ/triệu cho dễ đọc.
        /// Ví dụ: 1.235.000.000 → "1,235 tỷ đồng"
        /// </summary>
        public string FormattedPrice
        {
            get
            {
                if (Price >= 1_000_000_000m)
                {
                    // Hiển thị dạng tỷ đồng
                    var ty = Price / 1_000_000_000m;
                    return ty.ToString("0.###") + " tỷ đồng";
                }
                if (Price >= 1_000_000m)
                {
                    // Hiển thị dạng triệu đồng
                    var tr = Price / 1_000_000m;
                    return tr.ToString("0.###") + " triệu đồng";
                }
                return Price.ToString("#,##0") + "đ";
            }
        }
    }
}
