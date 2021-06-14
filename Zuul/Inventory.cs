using System;
using System.Collections.Generic;
using System.Text;

namespace Zuul
{
    public class Inventory
    {
        private int maxWeight;
        private Dictionary<string, string> items;
        public Inventory(int maxWeight)
        {
            this.maxWeight = maxWeight;
            this.items = new Dictionary<string, string>();
        }
        public bool Put(Item item)
        {
            // check the Weight of the Item!
            // put Item in the items Collection
            // return true/false for success/failure
            return false;
        }
        public Item Get(string itemName)
        {
            // find Item in items Collection
            // remove Item from items Collection if found
            // return Item
            return null;
        }
    }
}
