using ETicaretAPI.Application.ViewModels.Products;
using FluentValidation;

namespace ETicaretAPI.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<VM_Creat_Product>
    {
        public CreateProductValidator()
        {
            //RuleFor(c => c.Name)
            //    .NotEmpty()
            //    .NotNull()
            //        .WithMessage("Lütfen ürün adını boş bırakmayınız.")
            //    .MaximumLength(150)
            //    .MinimumLength(3)
            //        .WithMessage("Lütfen ürün adını 3 ile 150 karakter arasında giriniz.");

            //RuleFor(p => p.Stock)
            //    .NotEmpty()
            //    .NotNull()
            //        .WithMessage("Lütfen stok bilgisini boş bırakmayınız.")
            //    .Must(s => s >= 0)
            //        .WithMessage("Stok bilgisi negatif olamaz.");

            //RuleFor(p => p.Price)
            //  .NotEmpty()
            //  .NotNull()
            //      .WithMessage("Lütfen fiyat bilgisini boş bırakmayınız.")
            //  .Must(s => s >= 0)
            //      .WithMessage("Fiyat bilgisi negatif olamaz.");

        }

    }

}
