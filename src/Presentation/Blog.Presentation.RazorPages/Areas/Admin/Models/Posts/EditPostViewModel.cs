using System.ComponentModel.DataAnnotations;

namespace Blog.Presentation.RazorPages.Areas.Admin.Models.Posts;

public class EditPostViewModel
{
    [Display(Name = "انتخاب دسته بندی")]
    [Required(ErrorMessage = "لطفا دسته بندی را وارد کنید")]
    public int CategoryId { get; set; }

    [Display(Name = "انتخاب زیر دسته بندی")]
    [Required(ErrorMessage = "لطفا  زیر دسته بندی را وارد کنید")]
    public int? SubCategoryId { get; set; }

    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "لطفا عنوان را وارد کنید")]
    public string Title { get; set; }

    [Display(Name = "slug")]
    [Required(ErrorMessage = "لطفا slug را وارد کنید")]
    public string Slug { get; set; }
    [Display(Name = "توضیحات")]
    [Required(ErrorMessage = "لطفا توضیحات را وارد کنید")]
    public string Description { get; set; }

    [Display(Name = "متن پست")]
    [Required(ErrorMessage = "لطفا متن پست را وارد کنید")]
    [UIHint("Ckeditor4")]
    public string Context { get; set; }

    [Display(Name = "عکس")]
    public IFormFile ImageFile { get; set; }
}