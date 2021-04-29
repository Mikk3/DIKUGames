using System.Collections.Generic;
using System.IO;

namespace Breakout.Levels {

    public class MetaParser {

        private List<string> data;

        public MetaParser(List<string> data) {
            this.data = data;
        }

        public Dictionary<string, string> Parse() {

            var start = data.FindIndex(x => x == "Meta:") + 1;
            var end = data.FindIndex(x => x == "Meta/");


            var meta = data.GetRange(start, end - start);
            var result = new Dictionary<string, string> ();

            foreach (var x in meta) {
                var pair = x.Split(": ", 2);
                var key = pair[0];
                var value = pair[1];

                result.Add(key, value);
            }
            return result;
        }

    }

}