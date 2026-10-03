using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using HoangDuonglab5.ValidationAttributes;

namespace HoangDuonglab5.Models
{
    public class Product
    {
        [Key]
        [Display(Name = "Mã sản phẩm")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(150, MinimumLength = 6, ErrorMessage = "Tên sản phẩm phải từ 6 đến 150 ký tự")]
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }

        [Display(Name = "Hình ảnh")]
        public string Image { get; set; }

        // Dùng để upload file từ máy tính lên Server
        [Required(ErrorMessage = "Hình ảnh phải được chọn")]
        [Display(Name = "Chọn hình ảnh")]
        public IFormFile ImageUpload { get; set; }

        [Required(ErrorMessage = "Giá sản phẩm không được để trống")]
        [Range(100000, float.MaxValue, ErrorMessage = "Giá sản phẩm phải nhỏ nhất là 100,000")]
        [Display(Name = "Giá chuẩn")]
        public float Price { get; set; }

        [Required(ErrorMessage = "Giá khuyến mãi không được để trống")]
        [SalePrice("Price")]
        [Display(Name = "Giá khuyến mãi")]
        public float SalePrice { get; set; }

        [Required(ErrorMessage = "Mô tả không được để trống")]
        [MaxLength(1500, ErrorMessage = "Mô tả không được vượt quá 1500 ký tự")]
        [BadWord("die", "admin", "fack")]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn danh mục")]
        [Display(Name = "Danh mục")]
        public int CategoryId { get; set; }
    }
}