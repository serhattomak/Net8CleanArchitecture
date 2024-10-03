using App.Application.Contracts.Persistence;
using FluentValidation;

namespace App.Application.Features.Products.Create;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    private readonly IProductRepository _productRepository;
    public CreateProductRequestValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;
        RuleFor(x => x.Name)
            //.NotNull().WithMessage("Product name is required.")
            .NotEmpty().WithMessage("Product name is required.")
            .Length(3, 100).WithMessage("Product name should be min 3, max 100 characters.");
        //.MustAsync(MustUniqueProductNameAsync).WithMessage("Product name is already exist in database.");
        //.Must(MustUniqueProductName).WithMessage("Product name is already exist in database.");

        // price validation
        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price should be greater than 0.");

        RuleFor(x => x.CategoryId)
	        .GreaterThan(0).WithMessage("Category Id should be greater than 0.");

		// stock inclusiveBetween validation
		RuleFor(x => x.Stock)
            .InclusiveBetween(1, 1000).WithMessage("Stock should be between 1 and 1000.");
    }

    #region async validation

    //private async Task<bool> MustUniqueProductNameAsync(string name, CancellationToken cancellationToken)
    //{
    //	return !await _productRepository.Where(x => x.Name == name).AnyAsync(cancellationToken);
    //}

    #endregion

    #region sync validation

    //private bool MustUniqueProductName(string name)
    //{
    //	return !_productRepository.Where(x => x.Name == name).Any();
    //}

    #endregion
}