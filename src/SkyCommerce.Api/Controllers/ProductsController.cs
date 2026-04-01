using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SkyCommerce.Application.DTOs;
using SkyCommerce.Domain.Entities;
using SkyCommerce.Domain.Interfaces;

namespace SkyCommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public ProductsController(IRepository<Product> productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productRepository.GetAllAsync();
        var dtos = _mapper.Map<IEnumerable<ProductDto>>(products);
        return Ok(dtos);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductDto request)
    {
        var product = new Product(request.Name, request.Description, request.Price, request.Stock, request.CategoryId);
        await _productRepository.AddAsync(product);
        await _productRepository.SaveChangesAsync();
        
        var responseDto = _mapper.Map<ProductDto>(product);
        return CreatedAtAction(nameof(GetAll), new { id = product.Id }, responseDto);
    }
}
