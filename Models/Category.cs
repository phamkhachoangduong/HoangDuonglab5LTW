using System.ComponentModel.DataAnnotations;

namespace HoangDuonglab5.Models
{
    public class Category
    {
        [Key]
        [Display(Name = "Mã danh mục")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        [Display(Name = "Tên danh mục")]
        public string Name { get; set; }
    }
}