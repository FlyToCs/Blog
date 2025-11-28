using System.ComponentModel.DataAnnotations;
using Blog.Domain.core.Category.DTOs;

namespace Blog.Presentation.RazorPages.Areas.Admin.Models.Categories
{
    public class CreateCategoryViewModel
    {

        [Display(Name = " عنوان")]
        [Required(ErrorMessage = "وارد کردن عنوان اجباری است")]
        public string Title { get; set; }

        [Display(Name = "CategorySlug")]
        [Required(ErrorMessage = "وارد کردن CategorySlug اجباری است")]
        public string Slug { get; set; }
        public int? ParentId { get; set; }

        [Display(Name = "MetaTag (با - از هم جدا کنید )")]
        public string MetaTag { get; set; }
        [DataType(DataType.MultilineText)]
        public string MetaDescription { get; set; }

        public int UserId { get; set; }

        public CreateCategoryDto MapToDto()
        {
            return new CreateCategoryDto()
            {
                Title = Title,
                MetaDescription = MetaDescription,
                Slug = Slug,
                ParentId = ParentId,
                MetaTag = MetaTag,
                UserId = UserId
            };
        }
    }
}