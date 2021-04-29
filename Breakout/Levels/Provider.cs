
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using System.Linq;

namespace Breakout.Levels {

    public class Provider {

        private string levelName;

        public Provider(string levelName) {
            this.levelName = levelName;
        }

        public List<string> GetDataAsList() {
            var file = Path.Combine("Assets", "Levels", levelName + ".txt");

            return File.ReadAllLines(file).ToList<string>();
        }

    }

}