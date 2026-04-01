using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SkyCommerce.Application.DTOs;
using SkyCommerce.Domain.Entities;
using SkyCommerce.Domain.Interfaces;

namespace SkyCommerce.Api.Controllers;

/// <summary>
/// Gerencia as operações relacionadas com categorias de produtos.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;

    public CategoriesController(IRepository<Category> categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Obtém a lista de todas as categorias.
    /// </summary>
    /// <returns>Uma lista de categorias</returns>
    /// <response code="200">Retorna a lista de categorias com sucesso</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryRepository.GetAllAsync();
        var dtos = _mapper.Map<IEnumerable<CategoryDto>>(categories);
        return Ok(dtos);
    }

    /// <summary>
    /// Cadastra uma nova categoria no sistema.
    /// </summary>
    /// <param name="request">Os dados da categoria a ser criada</param>
    /// <returns>A categoria recém criada</returns>
    /// <response code="201">Retorna a nova categoria criada</response>
    /// <response code="400">Se o modelo enviado for inválido</response>
    [HttpPost]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateCategoryDto request)
    {
        var category = new Category(request.Name, request.Description);
        await _categoryRepository.AddAsync(category);
        await _categoryRepository.SaveChangesAsync();
        
        var responseDto = _mapper.Map<CategoryDto>(category);
        return CreatedAtAction(nameof(GetAll), new { id = category.Id }, responseDto);
    }
}
