using System.ComponentModel.DataAnnotations;

namespace Productos.Dtos
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        [Required]
        [Display(Name="Nombre del usuario")]
        public string Nombre { get; set; }
        [Display(Name = "Apellidos del usuario")]
        public string Apellidos { get; set; }
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Cuenta")]
        public string Username { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Perfil del usuario")]
        public int PerfilId { get; set; }
        public string Perfil { get; set; }
    }
}
