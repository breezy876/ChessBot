using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Uci
{
    [JsonObject]
    public class EngineOption
    {
        public string Name { get; init; }

        public string Type { get; init; }

        public object Value { get; set; }

        public int Min { get; init; }

        public int Max { get; init; }

        public object Default { get; init; }

        public ICollection<string>? Vars { get; init; }

    }
}
