using System;
using System.Collections.Generic;
using System.Text;

namespace Zuul
{
    public class Player
    {
        private int health;
        private Inventory inventory;

        private Item medkit;
        public Room CurrentRoom { get; set; }

        public Player()
        {
            inventory = new Inventory(25);
            health = 20;
            CurrentRoom = null;
            medkit = new Item(5, "a medkit to heal yourself");

            inventory.Put("medkit",medkit);
        }

        public bool TakeFromChest(string itemName)
        {
            Item item = CurrentRoom.Chest.Get(itemName);

            if (item == null)
            {
                Console.WriteLine("It didnt work");
                return false;
            }
            if (inventory.Put(itemName, item))
            {
                Console.WriteLine("You took " + itemName);
                return true;
            }

            CurrentRoom.Chest.Put(itemName, item);
            Console.WriteLine("You have no room for " + itemName);
            return false;
            
        }
        public bool DropToChest(string itemName)
        {
            Item item = inventory.Get(itemName);

            if (item == null)
            {
                Console.WriteLine("It didnt work");
                return false;
            }
            if (CurrentRoom.Chest.Put(itemName, item))
            {
                Console.WriteLine("You dropped a" + itemName);
                return true;
            }

            inventory.Put(itemName, item);
            Console.WriteLine("It didnt work");
            return false;
        }

        public void items()
        {
            if (inventory.Weight() <= 0)
            {
                Console.WriteLine("No items");
            }
            else
            {
                Console.WriteLine("Your items:");
                inventory.Look();

            }

        }

        public void Damage(int amount) 
        {
            health -= amount;
            Console.WriteLine("You lost " + amount + " health. You have now " + health + " health." );
            IsAlive();
        }
        public void  Heal(int amount)
        {
            health += amount;
            Console.WriteLine("You gained " + amount + " health. You have now " + health + " health.");
        }
        public void IsAlive()
        {
            if (health > 0)
            {
                Console.WriteLine("You are still alive!");
            } else
            {
                Console.WriteLine("You are death");
            }
        }
    }
}
