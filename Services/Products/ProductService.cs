using System.Net;
using App.Repositories;
using App.Repositories.Products;
using Microsoft.EntityFrameworkCore;

namespace App.Services.Products;

public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork) : IProductService
{
	public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count)
	{
		var products = await productRepository.GetTopPriceProductsAsync(count);

		var productsAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

		return new ServiceResult<List<ProductDto>>()
		{
			Data = productsAsDto
		};
	}
	public async Task<ServiceResult<List<ProductDto>>> GetAllListAsync()
	{
		var products = await productRepository.GetAll().ToListAsync();
		var productsAsDto= products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

		return ServiceResult<List<ProductDto>>.Success(productsAsDto);
	}
	public async Task<ServiceResult<ProductDto?>> GetByIdAsync(int id)
	{
		var product = await productRepository.GetByIdAsync(id);

		if (product is null)
		{
			ServiceResult<ProductDto>.Failure("Product not found", HttpStatusCode.NotFound);
		}

		var productsAsDto = new ProductDto(product!.Id, product.Name, product.Price, product.Stock);

		return ServiceResult<ProductDto>.Success(productsAsDto)!;
	}
	public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
	{
		var product = new Product()
		{
			Name = request.Name,
			Price = request.Price,
			Stock = request.Stock
		};

		await productRepository.AddAsync(product);
		await unitOfWork.SaveChangesAsync();
		return ServiceResult<CreateProductResponse>.Success(new CreateProductResponse(product.Id));
	}
	public async Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request)
	{
		var product = await productRepository.GetByIdAsync(id);

		if (product is null)
		{
			return ServiceResult.Failure("Product not found", HttpStatusCode.NotFound);
		}

		product.Name = request.Name;
		product.Price = request.Price;
		product.Stock = request.Stock;

		productRepository.Update(product);
		await unitOfWork.SaveChangesAsync();

		return ServiceResult.Success(HttpStatusCode.NoContent);
	}
	public async Task<ServiceResult> DeleteAsync(int id)
	{
		var product = await productRepository.GetByIdAsync(id);

		if (product is null)
		{
			return ServiceResult.Failure("Product not found", HttpStatusCode.NotFound);
		}

		productRepository.Delete(product);
		await unitOfWork.SaveChangesAsync();

		return ServiceResult.Success(HttpStatusCode.NoContent);
	}
}