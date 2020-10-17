using System;
using System.Collections.Generic;
using System.Linq;
using Markov;

namespace SWNUniverseGenerator
{
    public class NameGeneration
    {
        private MarkovChain<Char> _chain = new MarkovChain<Char>(2);
        private Random rand = new Random();

        public void GenerateChain(List<String> nameList)
        {
            foreach (var name in nameList)
            {
                _chain.Add(name, 1);
            }
        }

        public String GenerateName()
        {
            return new String(_chain.Chain(rand).ToArray());
        }
    }
}