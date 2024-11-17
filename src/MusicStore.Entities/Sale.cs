
namespace MusicStore.Entities
{
    public class Sale : EntityBase
    {
        public int CostumerId { get; set; }
        public int ConcertId {  get; set; }
        public DateTime SaleDate { get; set; }
        public string OperationNumber { get; set; } = default!;
        public decimal Total { get; set; }
        public short Quantity { get; set; }

        //propiedades de Navegacion
        public Costumer Costumer { get; set; } = default!;
        public Concert Concert { get; set; } = default!;
    }
}
