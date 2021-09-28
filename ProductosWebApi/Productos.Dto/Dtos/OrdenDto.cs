using Productos.Models.Enum;
using System;
using System.Collections.Generic;

namespace Productos.Dtos
{
    public class OrdenDto
    {
        public OrdenDto()
        {
            DetalleOrdens = new List<DetalleOrdenDto>();
        }

        public int Id { get; set; }
        public decimal CantidadArticulos { get; set; }
        public decimal Importe { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public int UsuarioId { get; set; }
        public EstatusOrden EstatusOrden { get; set; }
        public List<DetalleOrdenDto> DetalleOrdens { get; set; }
    }
}
