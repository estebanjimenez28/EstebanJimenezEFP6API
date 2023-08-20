namespace EstebanJimenezEFP6API.ModelsDTOs
{
    public class UserDTO
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public string PrimerNombre { get; set; } = null!;
        public string SegundoNombre { get; set; } = null!;
        public string? NumeroTelefono { get; set; }
        public string Contraseña { get; set; } = null!;
        public int recuento { get; set; }
        public string correoRespaldo { get; set; } = null!;
        public string? DescripcionTrabajo { get; set; }
        public int IdEstadoUsuario { get; set; }
        public int IDPais { get; set; }
        public int IdRolUsuario { get; set; }
    }
}
