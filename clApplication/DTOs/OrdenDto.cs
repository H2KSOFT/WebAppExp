namespace clApplication.DTOs
{
    public class OrdenDto
    {
        public Int64 Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public decimal Total { get; set; }
        public List<DetalleOrdenDto> Detalles { get; set; } = new List<DetalleOrdenDto>();
    }

    public class DetalleOrdenDto
    {
        public string Producto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal SubTotal { get; set; }
    }

    public class CrearOrdenDto
    {
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public List<DetalleOrdenDto> Detalles { get; set; } = new List<DetalleOrdenDto>();
    }
}
