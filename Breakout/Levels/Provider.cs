
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using System.Linq;
using DIKUArcade.Utilities;

namespace Breakout.Levels {

    public class Provider {

        private string levelName;

        public Provider(string levelName) {
            this.levelName = levelName;
        }

        /// <summary>
        /// Get content of files as a list whereby each line is a list entry
        /// </summary>
        /// <returns>List of strings</returns>
        public List<string> GetDataAsList() {
            var path = Path.Combine(FileIO.GetProjectPath(), "Assets", "Levels", levelName);
            return File.ReadAllLines(path).ToList<string>();
        }

    }

}