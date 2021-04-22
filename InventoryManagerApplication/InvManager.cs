using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagerApplication
{
    class InvManager
    {
        //Item list
        public List<Item> items;

        //Constructor
        public InvManager()
        {
            //Initialize the attributes
            items = new List<Item>();
        }
        //Method to add item
        public void addItem(Item item)
        {
            items.Add(item);
        }
        //Method to search by type
        public List<Item> findByType(string type)
        {
            List<Item> result = new List<Item>();
            for (int i = 0; i < items.Count(); i++)
            {

                if (items[i].Type == type)
                {
                    result.Add(items[i]);
                }
            }
            return result;
        }
        //Method to search by ID
        public Item findByID(string id)
        {
            Item result = null;
            for (int i = 0; i < items.Count(); i++)
            {
                if (items[i].ID == id)
                {
                    result = items[i];
                }
            }
            return result;
        }
        //Method to remove item
        public void removeItem(Item item)
        {
            items.Remove(item);
        }
        
        //Method to restock items
        public bool restockItem(int Count, Item Item)
        {
            if (items.Contains(Item))
            {
                Item.Quantity += Count;
                return true;
            }
            else
            {
                return false;
            }
        }
        //Method to display all the products
        public Item[] getAllItems()
        {
            Item[] result = new Item[items.Count];
            for (int i = 0; i < items.Count; i++)
            {
                result[i] = items[i];
            }
            return result;
        }
       
    }
}
