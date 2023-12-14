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

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LugaresController : ControllerBase
    {
        private readonly IRepositorio<Lugar> _lugarRepo;
        private readonly IRepositorio<Pais> _paisRepo;
        private readonly IRepositorio<Categoria> _categoriaRepo;

        public LugaresController(IRepositorio<Lugar> lugarRepo, IRepositorio<Pais> paisRepo, IRepositorio<Categoria> categoriaRepo)
        {
            _lugarRepo = lugarRepo;
            _paisRepo = paisRepo;
            _categoriaRepo = categoriaRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Lugar>>> GetLugares() 
        {
            var espec = new LugaresConPaisCategoriaEspecificacion();
            var lugares = await _lugarRepo.ObtenerTodosEspec(espec);

            return Ok(lugares);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Lugar>> GetLugar(int id) 
        {
            var espec = new LugaresConPaisCategoriaEspecificacion(id);
            return await _lugarRepo.ObtenerEspec(espec);
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