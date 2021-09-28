using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Productos.Data.Repostorios.Interfaces;
using Productos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Productos.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private IProductosRepositorio _productosRepositorio;
        public ProductosController(IProductosRepositorio productosRepositorio) 
        {
            _productosRepositorio = productosRepositorio;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos() 
        {
            try
            {
                return await _productosRepositorio.ObtenerProductosAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _productosRepositorio.ObtenerProductoAsync(id);

            if (producto == null) 
            {
                return NotFound();
            }

            return producto;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Producto>> PutProducto(int id, [FromBody] Producto producto)
        {
            if (producto == null)
                return NotFound();

            var resultado = await _productosRepositorio.Actualizar(producto);

            if(!resultado)
            {
                return BadRequest();
            }
            
            return producto;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            try
            {
                var nuevoProducto = await _productosRepositorio.Agregar(producto);
                if (nuevoProducto == null) 
                {
                    return BadRequest();
                }
                return CreatedAtAction(nameof(PostProducto), new { id = nuevoProducto.Id }, producto);
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
