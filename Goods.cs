namespace Sklad_tmc
{
    public class Goods
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int Quantity { get; set; }
        public string WarehouseName { get; set; } // Изменено для хранения имени склада
    }
}