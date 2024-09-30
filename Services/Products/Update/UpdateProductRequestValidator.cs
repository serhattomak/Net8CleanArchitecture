using App.Repositories.Products;
using FluentValidation;

namespace App.Services.Products.Update;

public class UpdateProductRequestValidator: AbstractValidator<UpdateProductRequest>
{
	private readonly IProductRepository _productRepository;
	public UpdateProductRequestValidator(IProductRepository productRepository)
	{
		_productRepository = productRepository;
		RuleFor(x => x.Name)
			.NotEmpty().WithMessage("Product name is required.")
			.Length(3, 10).WithMessage("Product name should be min 3, max 10 characters.");

		// price validation
		RuleFor(x => x.Price)
			.GreaterThan(0).WithMessage("Price should be greater than 0.");

		// stock inclusiveBetween validation
		RuleFor(x => x.Stock)
			.InclusiveBetween(1, 1000).WithMessage("Stock should be between 1 and 1000.");
	}
}