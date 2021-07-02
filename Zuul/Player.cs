using System;
using System.Collections.Generic;
using System.Text;

namespace Zuul
{
    public class Player
    {
        private int health;
        private int maxHealth;
        private Inventory inventory;

        private Item medkit;
        public Room CurrentRoom { get; set; }

        public Player()
        {
            inventory = new Inventory(25);
            maxHealth = 100;
            health = 100;
            CurrentRoom = null;
            medkit = new Item(5, "a medkit to heal yourself");

            inventory.Put("medkit",medkit);
        }

        public bool Use(string itemName, string onWhat)
        {
            Item item = inventory.Get(itemName); 

            if (item == null)
            {
                Console.WriteLine("It didnt work");
                return false;
            }
            
            if (item == medkit)
            {
                if (onWhat == "self")
                {
                    if (maxHealth <= health)
                    {
                        Console.WriteLine("You wasted a medkit on max health");
                    }else
                    {
                        health += 50;
                        if (health >= maxHealth)
                        {
                            health = maxHealth;
                        }
                        Console.WriteLine("You gained some health, your health is now: " + health);
                        
                    }
                    return true;
                }
                
            }
            return false;
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
