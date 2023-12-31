using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infraestructura.Datos;
using Core.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using Core.Especificaciones;
using AutoMapper;
using API.Dtos;
using API.Errores;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LugaresController : ControllerBase
    {
        private readonly IRepositorio<Lugar> _lugarRepo;
        private readonly IRepositorio<Pais> _paisRepo;
        private readonly IRepositorio<Categoria> _categoriaRepo;
        private readonly IMapper _mapper;

        public LugaresController(IRepositorio<Lugar> lugarRepo, IRepositorio<Pais> paisRepo, IRepositorio<Categoria> categoriaRepo, IMapper mapper)
        {
            _lugarRepo = lugarRepo;
            _paisRepo = paisRepo;
            _categoriaRepo = categoriaRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<LugarDto>>> GetLugares() 
        {
            var espec = new LugaresConPaisCategoriaEspecificacion();
            var lugares = await _lugarRepo.ObtenerTodosEspec(espec);

            return Ok(_mapper.Map<IReadOnlyList<Lugar>, IReadOnlyList<LugarDto>>(lugares));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LugarDto>> GetLugar(int id) 
        {
            var espec = new LugaresConPaisCategoriaEspecificacion(id);
            var lugar = await _lugarRepo.ObtenerEspec(espec);

            if(lugar == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<Lugar,LugarDto>(lugar);
        }

        [HttpGet("paises")]
        public async Task<ActionResult<List<Pais>>> GetPaises()
        {
            return Ok(await _paisRepo.ObtenerTodosAsync());
        }

        [HttpGet("categorias")]
        public async Task<ActionResult<List<Categoria>>> GetCategorias()
        {
            return Ok(await _categoriaRepo.ObtenerTodosAsync());
        }
    }
}