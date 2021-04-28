using System.Collections.Generic;
using System.IO;

namespace Breakout.Levels {

    public class LegendParser {

        private List<string> data;

        public LegendParser(List<string> data) {
            this.data = data;
        }

        public Dictionary<char, string> Parse() {
            var start = data.FindIndex(x => x == "Legend:") + 1;
            var end = data.FindIndex(x => x == "Legend/");

            var meta = data.GetRange(start, end - start);
            var result = new Dictionary<char, string> ();

            foreach (var x in meta) {
                var pair = x.Split(") ", 2);
                var key = pair[0][0];
                var value = pair[1];

                result.Add(key, value);
            }
            foreach (var p in result) {
            }
            return result;
        }
    }

}