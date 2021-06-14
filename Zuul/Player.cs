using System;
using System.Collections.Generic;
using System.Text;

namespace Zuul
{
    public class Player
    {
        private int health;
        public Room CurrentRoom { get; set; }

        public Player()
        {
            health = 2;
            CurrentRoom = null;
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
                Console.WriteLine("You are till alive!");
            } else
            {
                Console.WriteLine("You are death");
            }
        }
    }
}
