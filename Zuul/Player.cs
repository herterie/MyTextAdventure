using System;
using System.Collections.Generic;
using System.Text;

namespace Zuul
{
    public class Player
    {
        public Room CurrentRoom { get; set; }

        public Player()
        {
            CurrentRoom = null;
        }
    }
}
