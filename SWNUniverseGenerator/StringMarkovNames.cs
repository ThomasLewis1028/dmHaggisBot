using System.Collections.Generic;
using MarkovSharp.TokenisationStrategies;

namespace SWNUniverseGenerator
{
    public class StringMarkovNames : StringMarkov
    {
        public override IEnumerable<string> SplitTokens(string input)
        {
            if (input == null)
                return (IEnumerable<string>) new List<string>()
                {
                    this.GetPrepadUnigram()
                };
            input = input.Trim();
            return (IEnumerable<string>) input.Split("");
        }
        
        public override string RebuildPhrase(IEnumerable<string> tokens)
        {
            return string.Join("", tokens);
        }
    }
}