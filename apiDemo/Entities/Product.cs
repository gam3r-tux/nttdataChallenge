using System.ComponentModel.DataAnnotations;

namespace apiDemo.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Cost { get; set; }
        public byte Active { get; set; }

    }
}
