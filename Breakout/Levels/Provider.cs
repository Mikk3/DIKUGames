
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using System.Linq;

namespace Breakout.Levels
{

    public class Provider {

        private string levelName;

        public Provider(string levelName) {
            this.levelName = levelName;
        }

        private string getFilePath() {
            return Path.Combine("Assets","Levels", levelName + ".txt");
        }

        public List<string> GetDataAsList() {
            return File.ReadAllLines(getFilePath()).ToList<string>();
        }

    }

}