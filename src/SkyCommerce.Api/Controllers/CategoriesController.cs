using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SkyCommerce.Application.DTOs;
using SkyCommerce.Domain.Entities;
using SkyCommerce.Domain.Interfaces;

namespace SkyCommerce.Api.Controllers;

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

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryRepository.GetAllAsync();
        var dtos = _mapper.Map<IEnumerable<CategoryDto>>(categories);
        return Ok(dtos);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryDto request)
    {
        var category = new Category(request.Name, request.Description);
        await _categoryRepository.AddAsync(category);
        await _categoryRepository.SaveChangesAsync();
        
        var responseDto = _mapper.Map<CategoryDto>(category);
        return CreatedAtAction(nameof(GetAll), new { id = category.Id }, responseDto);
    }
}
