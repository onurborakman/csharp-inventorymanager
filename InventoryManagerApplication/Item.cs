using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagerApplication
{
    class Item
    {
        public string ID { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double Size { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        

        public Item(string id, string brand, string name, string type, double size, int quantity, double price)
        {
            this.ID = id;
            this.Brand = brand;
            this.Name = name;
            this.Type = type;
            this.Size = size;
            this.Quantity = quantity;
            this.Price = price;
        }

        public override string ToString()
        {
            return this.ID + "|" + this.Brand + "|" + this.Name + "|" + this.Type + "|" + this.Size + "|" + this.Quantity + "|" + this.Price;
        }
    }
}
