using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Breakout.Levels {

    public class RowParser {

        private List<string> data;

        public RowParser(List<string> data) {
            this.data = data;
        }
        public List<string> Parse() {
            if (data.Contains("Map:") && data.Contains("Map/")) {

                var start = data.FindIndex(x => x == "Map:") + 1;
                var end = data.FindIndex(x => x == "Map/");

                return data.GetRange(start, end - start);
            } else {
                throw new InvalidDataException();
            }
        }
    }
}