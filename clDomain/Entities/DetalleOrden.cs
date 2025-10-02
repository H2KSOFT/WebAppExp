namespace clDomain.Entities
{
    public class DetalleOrden
    {
        public Int64 Id { get; set; }
        public Int64 IdOrden { get; set; }
        public string Producto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal SubTotal { get; set; }
        public Orden Orden { get; set; }
    }
}
