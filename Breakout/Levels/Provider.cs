
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
            //////////////////
            // Copied from DIKUArcade Texture.cs - testing would not function properly without this block of code
            var dir = new DirectoryInfo(Path.GetDirectoryName(
               System.Reflection.Assembly.GetExecutingAssembly().Location));

            while (dir.Name != "bin") {
                dir = dir.Parent;
            }
            dir = dir.Parent;

            var path = Path.Combine(dir.FullName.ToString(), "Assets", "Levels", levelName + ".txt");
            if (!File.Exists(path)) {
                throw new FileNotFoundException($"Error: The file \"{path}\" does not exist.");
            }
            //////////////////

            return File.ReadAllLines(path).ToList<string>();
        }

    }

}