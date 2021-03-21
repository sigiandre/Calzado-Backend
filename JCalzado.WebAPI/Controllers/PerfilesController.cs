using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JCalzado.Data.Contratos;
using JCalzado.Dtos;
using JCalzado.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JCalzado.WebAPI.Controllers
{
    [Authorize(Roles = "Administrador")]
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilesController : ControllerBase
    {
        private readonly IPerfilRepositorio<Perfil> _perfilRepositorio;
        private readonly IMapper _mapper;
        private readonly ILogger<PerfilesController> _logger;

        public PerfilesController(IPerfilRepositorio<Perfil> perfilRepositorio, IMapper mapper, ILogger<PerfilesController> logger)
        {
            _perfilRepositorio = perfilRepositorio;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Perfiles
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PerfilDto>>> Get()
        {
            try
            {
                var perfiles = await _perfilRepositorio.ObtenerTodosAsync();
                return _mapper.Map<List<PerfilDto>>(perfiles);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error en {nameof(Get)}: " + ex.Message);
                return BadRequest();
            }
        }

        // GET: api/Perfiles/1
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PerfilDto>> Get(int id) 
        {
            var perfil = await _perfilRepositorio.ObtenerAsync(id);

            if (perfil == null) 
            {
                return NotFound();
            }
            return _mapper.Map<PerfilDto>(perfil);
        }

        // POST: api/Perfiles
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PerfilDto>> Post(PerfilDto perfilDto)
        {
            try
            {
                var perfil = _mapper.Map<Perfil>(perfilDto);

                var nuevoPerfil = await _perfilRepositorio.Agregar(perfil);

                if(nuevoPerfil == null)
                {
                    return BadRequest();
                }
                var nuevoPerfilDto = _mapper.Map<PerfilDto>(nuevoPerfil);
                return CreatedAtAction(nameof(Post), new { id = nuevoPerfilDto.Id }, nuevoPerfilDto);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error en {nameof(Post)}: " + ex.Message);
                return BadRequest();
            }
        }

        // PUT: api/Perfiles/1
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PerfilDto>> Put(int id, [FromBody] PerfilDto perfilDto)
        {
            if (perfilDto == null)
            {
                return NotFound();
            }

            var perfil = _mapper.Map<Perfil>(perfilDto);
            var resultado = await _perfilRepositorio.Actualizar(perfil);

            if (!resultado)
            {
                return BadRequest();
            }
            return perfilDto;
        }

        // DELETE: api/Perfiles/1
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var resultado = await _perfilRepositorio.Eliminar(id);

                if (!resultado) 
                {
                    return BadRequest();
                }
                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error en {nameof(Delete)}: " + ex.Message);
                return BadRequest();
            }
        }
    }
}
