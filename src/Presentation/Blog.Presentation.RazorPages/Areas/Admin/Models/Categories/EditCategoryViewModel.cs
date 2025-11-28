using System.ComponentModel.DataAnnotations;

namespace Blog.Presentation.RazorPages.Areas.Admin.Models.Categories
{
    public class EditCategoryViewModel
    {
        [Display(Name = " عنوان")]
        [Required(ErrorMessage = "وارد کردن عنوان اجباری است")]
        public string Title { get; set; }

        [Display(Name = "CategorySlug")]
        [Required(ErrorMessage = "وارد کردن CategorySlug اجباری است")]
        public string Slug { get; set; }

        [Display(Name = "MetaTag (با - از هم جدا کنید )")]
        public string MetaTag { get; set; }
        [DataType(DataType.MultilineText)]
        public string MetaDescription { get; set; }

    }
}