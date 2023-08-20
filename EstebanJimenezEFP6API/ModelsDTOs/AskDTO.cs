namespace EstebanJimenezEFP6API.ModelsDTOs
{
    public class AskDTO
    {
        public long IdPregunta { get; set; }
        public DateTime fecha { get; set; }
        public string pregunta { get; set; } = null!;
        public int idusuario { get; set; }
        public int idEstadoPregunta{ get; set; }
        public bool? activo { get; set; }
        public string? urlUmagen { get; set; }
        public string? detallePregunta { get; set; }

    }
}
