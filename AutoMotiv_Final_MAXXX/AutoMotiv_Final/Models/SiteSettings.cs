using System.Text.Json;

namespace Web.Models
{
    public class SiteSettings
    {
        public string SiteName { get; set; } = "AutoMotiv";
        public string SiteLogoIcon { get; set; } = "fa-car-side";
        public string SiteLogoUrl { get; set; } = "";
        public string ThemePreset { get; set; } = "automotive";
        public string PrimaryColor { get; set; } = "#1a1f2e";
        public string AccentColor { get; set; } = "#e63946";
        public string BgWarm { get; set; } = "#f5f7fa";
        public string TextDark { get; set; } = "#0d1117";
        public string SiteTagline { get; set; } = "Đại lý xe ô tô chính hãng – Uy tín – Chất lượng – Tận tâm";
        public string HeroImageUrl { get; set; } = "";
        public string FooterAddress { get; set; } = "123 Đường Nguyễn Văn Linh, Quận 7, TP.HCM";
        public string FooterPhone { get; set; } = "1800 1234";
        public string FooterEmail { get; set; } = "info@automotiv.vn";
        public bool MaintenanceMode { get; set; } = false;
        public string SocialFacebook { get; set; } = "#";
        public string SocialInstagram { get; set; } = "#";
        public string SocialTiktok { get; set; } = "#";
        public DateTime LastUpdated { get; set; } = DateTime.Now;
    }
}
