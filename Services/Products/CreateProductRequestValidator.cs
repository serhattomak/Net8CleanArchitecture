using FluentValidation;

namespace App.Services.Products;

public class CreateProductRequestValidator: AbstractValidator<CreateProductRequest>
{
	public CreateProductRequestValidator()
	{
		RuleFor(x => x.Name)
			//.NotNull().WithMessage("Product name is required.")
			.NotEmpty().WithMessage("Product name is required.")
			.Length(3,10).WithMessage("Product name should be min 3, max 10 characters.");

		// price validation
		RuleFor(x => x.Price)
			.GreaterThan(0).WithMessage("Price should be greater than 0.");

		// stock inclusiveBetween validation
		RuleFor(x => x.Stock)
			.InclusiveBetween(1, 100).WithMessage("Stock should be between 1 and 100.");
	}
}