namespace DTO
{
    public class InventoryDTO
    {
        public int inventoryId { get; set; }
        public int idStrain { get; set; }
        public string strainNumber { get; set; }
        public int quantity { get; set; }
        public string price { get; set; }
        public string entryDate { get; set; }
        public string histories { get; set; }
        public decimal priceValue { get; set; }
    }
}
