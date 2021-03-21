using AutoMapper;
using JCalzado.Data.Contratos;
using JCalzado.Dtos;
using JCalzado.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JCalzado.WebAPI.Controllers
{
    [Authorize(Roles = "Administrador,Vendedor")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductosRepositorio _productosRepositorio;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductosController> _logger;
        public ProductosController(IProductosRepositorio productosRepositorio, IMapper mapper, ILogger<ProductosController> logger)
        {
            _productosRepositorio = productosRepositorio;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Productos 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> Get()
        {
            try
            {
                var productos = await _productosRepositorio.ObtenerProductosAsync();
                return _mapper.Map<List<ProductoDto>>(productos);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Get)}: ${ex.Message}");
                return BadRequest();
            }
        }

        // GET: api/Productos/1
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductoDto>> Get(int id)
        {
            var producto = await _productosRepositorio.ObtenerProductoAsync(id);

            if (producto == null)
            {
                return NotFound();
            }
            return _mapper.Map<ProductoDto>(producto);
        }

        // POST: api/Productos
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductoDto>> Post(ProductoDto productoDto)
        {
            try
            {
                var producto = _mapper.Map<Producto>(productoDto);

                var nuevoProducto = await _productosRepositorio.Agregar(producto);

                if (nuevoProducto == null)
                {
                    return BadRequest();
                }
                var nuevoProductoDto = _mapper.Map<ProductoDto>(nuevoProducto);
                return CreatedAtAction(nameof(Post), new { id = nuevoProductoDto.Id }, nuevoProductoDto);
            }catch(Exception ex)
            {
                _logger.LogError($"Error en {nameof(Post)}: ${ex.Message}");
                return BadRequest();
            }
        }

        // PUT: api/Productos/1
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductoDto>> Put(int id, [FromBody] ProductoDto productoDto)
        {
            if (productoDto == null)
            {
                return NotFound();
            }

            var producto = _mapper.Map<Producto>(productoDto);

            var resultado = await _productosRepositorio.Actualizar(producto);

            if (!resultado)
            {
                return BadRequest();
            }
            return productoDto;
        }

        // DELETE: api/Productos/1
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var resultado = await _productosRepositorio.Eliminar(id);

                if (!resultado)
                {
                    return BadRequest();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Delete)}: ${ex.Message}");
                return BadRequest();
            }
        }
    }
}
