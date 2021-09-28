using AutoMapper;
using Productos.Dtos;
using Productos.Models;

namespace Productos.WebApi.Profiles
{
    public class ProductoProfile:Profile
    {
        public ProductoProfile()
        {
            this.CreateMap<Producto, ProductoDto>().ReverseMap();
        }
    }
}
