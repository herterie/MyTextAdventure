﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Zuul
{
    public class Item
    {
        public int Weight { get; }
        public string Description { get; }
        public Item(int weight, string description)
        {
            Weight = weight;
            Description = description;
        }
    }
}
