using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationExample.PropConfigurationProvider
{
    internal class PropConfigurationFileParser
    {
        private readonly IDictionary<string, string> _data = new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        public static IDictionary<string, string> Parse(Stream input)
            => new PropConfigurationFileParser().ParseStream(input);

        private IDictionary<string, string> ParseStream(Stream input)
        {
            _data.Clear();
            StreamReader sr = new StreamReader(input);
            //while (sr.EndOfStream)
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                _data.Add(line.Split(':')[0], line.Split(':')[1]);
            }
            return _data;
        }
    }
}
