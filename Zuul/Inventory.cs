using System;
using System.Collections.Generic;
using System.Text;

namespace Zuul
{
    public class Inventory
    {
        private int maxWeight;
        private Dictionary<string, Item> items;
        public Inventory(int maxWeight)
        {
            this.maxWeight = maxWeight;
            this.items = new Dictionary<string, Item>();
        }
        public bool Put(String itemName, Item item)
        {
            if (this.Weight()+item.Weight <= maxWeight){
                this.items.Add(itemName, item);
                return true;
            }else
            {
                return false;
            }
        }

        public int Weight()
        {
            int totalWeight = 0;

            foreach(var i in items)
            {
                totalWeight += i.Value.Weight;
            }

            return totalWeight;
        }
        public Item Get(string itemName)
        {
            if (itemName == null)
            {
                return null;
            }
            if (items.ContainsKey(itemName))
            {
                Item item = items[itemName];
                items.Remove(itemName);
                return item;
            }
            return null;
        }

        public void Look()
        {
            foreach (string key in items.Keys)
            {
                Console.WriteLine(key);
            }
        }
    }
}
