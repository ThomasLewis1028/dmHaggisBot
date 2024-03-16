﻿using System;
using System.Collections.Generic;
using System.Linq;
using Markov;

namespace SWNUniverseGenerator
{
    public class NameGeneration
    {
        private MarkovChain<Char> _chain = new (2);
        private Random _rand = new ();

        public bool GenerateChain(List<String> nameList)
        {

            foreach (var name in nameList)
                _chain.Add(name, 1);
            return true;

        }

        public String GenerateName()
        {
            return new String(_chain.Chain(_rand.Next()).ToArray());
        }
    }
}