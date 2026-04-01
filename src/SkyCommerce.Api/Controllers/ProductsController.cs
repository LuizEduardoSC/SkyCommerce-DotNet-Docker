using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SkyCommerce.Application.DTOs;
using SkyCommerce.Domain.Entities;
using SkyCommerce.Domain.Interfaces;

namespace SkyCommerce.Api.Controllers;

/// <summary>
/// Gerencia as operações relacionadas aos produtos.
/// </summary>
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

    /// <summary>
    /// Obtém a lista de todos os produtos cadastrados.
    /// </summary>
    /// <returns>Uma lista de produtos</returns>
    /// <response code="200">Retorna a lista de produtos com sucesso</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productRepository.GetAllAsync();
        var dtos = _mapper.Map<IEnumerable<ProductDto>>(products);
        return Ok(dtos);
    }

    /// <summary>
    /// Cadastra um novo produto na loja.
    /// </summary>
    /// <param name="request">Os dados do produto a ser criado</param>
    /// <returns>O produto recém criado</returns>
    /// <response code="201">Retorna o novo produto criado</response>
    /// <response code="400">Se o modelo enviado for inválido</response>
    [HttpPost]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateProductDto request)
    {
        var product = new Product(request.Name, request.Description, request.Price, request.Stock, request.CategoryId);
        await _productRepository.AddAsync(product);
        await _productRepository.SaveChangesAsync();
        
        var responseDto = _mapper.Map<ProductDto>(product);
        return CreatedAtAction(nameof(GetAll), new { id = product.Id }, responseDto);
    }
}
