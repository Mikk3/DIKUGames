using DIKUArcade.Entities;
using Breakout.Blocks;
using System.IO;
using System.Collections.Generic;
using System;
using DIKUArcade.Events;
using Breakout.States;

namespace Breakout.Levels {

    public class LevelData {

        private string level;

        public EntityContainer<Block> Blocks;

        public Dictionary<string, string> MetaList { get; private set; }
        public Dictionary<char, string> LegendList { get; private set; }
        public List<string> RowsList { get; private set; }

        public Queue<string> LevelQueue { get; private set; }

        public LevelData() {
            LevelQueue = new Queue<string>();

            // Load levels from Levels folder
            DirectoryInfo di = new DirectoryInfo(Environment.CurrentDirectory + "/Assets/Levels/");
            FileInfo[] fi = di.GetFiles();
            foreach (FileInfo f in fi) {
                LevelQueue.Enqueue(f.Name);
            }

            // Create first level
            NextLevel();

        }

        public void NextLevel() {
            if (LevelQueue.TryDequeue(out string levelname)) {
                System.Console.WriteLine("INFO: Loaded level: {0}", levelname);
                level = levelname;
                createBlocks();
            } else {
                System.Console.WriteLine("INFO: No more levels left");
                GameRunning.DeleteInstance();
                LoadMainMenu();
            }
        }

        private void createBlocks() {
            try {
                var data = new Provider(level).GetDataAsList();
                RowsList = new RowParser(data).Parse();
                MetaList = new MetaParser(data).Parse();
                LegendList = new LegendParser(data).Parse();

                Blocks = Generator.GenerateBlocksContainer(RowsList, MetaList, LegendList);
            } catch (FileNotFoundException) {
                System.Console.WriteLine("ERROR: {0} does not exist.", level);
                LoadMainMenu();
            } catch (InvalidDataException) {
                System.Console.WriteLine("ERROR: {0} contains invalid level data.", level);
                LoadMainMenu();
            }

        }

        private void LoadMainMenu() {
            System.Console.WriteLine("INFO: Returning to Main Menu");
            var gameEvent = new GameEvent();
            gameEvent.EventType = GameEventType.GameStateEvent;
            gameEvent.Message = "MAIN_MENU";
            BreakoutBus.GetBus().RegisterEvent(gameEvent);
        }
    }
}