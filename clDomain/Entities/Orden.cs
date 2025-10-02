namespace clDomain.Entities
{
    public class Orden
    {
        public Int64 Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public decimal Total { get; set; }
        public ICollection<DetalleOrden> Detalles { get; set; } = new List<DetalleOrden>();
    }
}
