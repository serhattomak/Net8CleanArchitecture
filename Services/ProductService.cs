﻿using App.Repositories;
using App.Repositories.Products;

namespace App.Services;

public class ProductService(IGenericRepository<Product> productRepository)
{

}