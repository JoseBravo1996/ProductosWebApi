using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Productos.Data.Repostorios.Interfaces;
using Productos.Dtos;
using Productos.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Productos.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private IProductosRepositorio _productosRepositorio;
        private readonly IMapper _mapper;

        public ProductosController(IProductosRepositorio productosRepositorio, IMapper mapper) 
        {
            _productosRepositorio = productosRepositorio;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> GetProductos() 
        {
            try
            {
                var productos = await _productosRepositorio.ObtenerProductosAsync();
                return _mapper.Map<List<ProductoDto>>(productos);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductoDto>> GetProducto(int id)
        {
            var producto = await _productosRepositorio.ObtenerProductoAsync(id);

            if (producto == null) 
            {
                return NotFound();
            }

            return _mapper.Map<ProductoDto>(producto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductoDto>> PutProducto(int id, [FromBody] ProductoDto productoDto)
        {
            if (productoDto == null)
                return NotFound();

            var producto = _mapper.Map<Producto>(productoDto);
            var resultado = await _productosRepositorio.Actualizar(producto);

            if(!resultado)
                return BadRequest();
            
            return productoDto;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Producto>> PostProducto(ProductoDto productoDto)
        {
            try
            {
                var producto = _mapper.Map<Producto>(productoDto);

                var nuevoProducto = await _productosRepositorio.Agregar(producto);
                if (nuevoProducto == null) 
                {
                    return BadRequest();
                }

                var nuevoProductoDto = _mapper.Map<ProductoDto>(producto);
                return CreatedAtAction(nameof(PostProducto), new { id = nuevoProductoDto.Id }, nuevoProductoDto);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Producto>> DeleteProducto(int id)
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
                return BadRequest();
            }
            
        }
    }
}
