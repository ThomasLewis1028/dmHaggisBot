﻿using System;

namespace SWNUniverseGenerator
{
    public interface ILocation : IEntity
    {
        public String ID { get; set; }
        
        public String Name { get; set; }
    }
}