using DIKUArcade.Entities;
using Breakout.Blocks;
using System.IO;
using System.Collections.Generic;
using System;

namespace Breakout.Levels {

    public class LevelData {

        private string level;

        public EntityContainer<Block> Blocks;

        public Dictionary<string, string> MetaList {get; private set;}
        public Dictionary<char, string> LegendList {get; private set;}
        public List<string> RowsList {get; private set;}

        public LevelData(string level) {
            this.level = level;

            createBlocks();

        }

        private void createBlocks() {

            try {
                var data = new Provider(level).GetDataAsList();
                RowsList = new RowParser(data).Parse();
                MetaList = new MetaParser(data).Parse();
                LegendList = new LegendParser(data).Parse();

                Blocks = Generator.GenerateBlocksContainer(RowsList, MetaList, LegendList);
            } catch (Exception ex) {

                if (ex is FileNotFoundException) {
                    System.Console.WriteLine("File does not exist.");
                    loadDefaultLevel();
                }

                if (ex is InvalidDataException) {
                    System.Console.WriteLine("Invalid level data.");
                    loadDefaultLevel();
                }
            }

        }

        private void loadDefaultLevel() {
            var data = new Provider("level1").GetDataAsList();
            RowsList = new RowParser(data).Parse();
            MetaList = new MetaParser(data).Parse();
            LegendList = new LegendParser(data).Parse();

            Blocks = Generator.GenerateBlocksContainer(RowsList, MetaList, LegendList);
            System.Console.WriteLine("Loading default level.");
        }
    }
}