namespace DTO
{
    public class OrderDetailDTO
    {
        public int idOrderDetail { get; set; }
        public int idOrder { get; set; }
        public int idStrain { get; set; }
        public string nameStrain { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }
    }
}
