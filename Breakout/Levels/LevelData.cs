using DIKUArcade.Entities;
using Breakout.Blocks;
using System.IO;
using System.Collections.Generic;
using System;
using DIKUArcade.Events;
using Breakout.States;
using DIKUArcade.Utilities;
using Breakout.GameInfo;
using DIKUArcade.Math;

namespace Breakout.Levels {

    public class LevelData {

        private string level;

        public EntityContainer<Block> Blocks;

        public Dictionary<string, string> MetaList { get; private set; }
        public Dictionary<char, string> LegendList { get; private set; }
        public List<string> RowsList { get; private set; }
        private Queue<string> LevelQueue;
        public Timer TimeLimit;

        public LevelData() {
            LevelQueue = new Queue<string>();

            // Load levels from Levels folder
            DirectoryInfo di = new DirectoryInfo(FileIO.GetProjectPath() + "/Assets/Levels/");
            FileInfo[] fi = di.GetFiles();
            foreach (FileInfo f in fi) {
                LevelQueue.Enqueue(f.Name);
            }

            // Create first level
            NextLevel();

        }

        public void NextLevel() {
            if (LevelQueue.TryDequeue(out string levelname)) {
                level = levelname;
                createBlocks();
                createTimer();

                Console.WriteLine("INFO: Loaded level: {0}", levelname);
            } else {
                GameRunning.DeleteInstance();
                Console.WriteLine("INFO: No more levels left");

                var gameEvent = new GameEvent();
                gameEvent.EventType = GameEventType.ControlEvent;
                gameEvent.Message = "WON_GAME";
                BreakoutBus.GetBus().RegisterEvent(gameEvent);
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
                loadMainMenu();
            } catch (InvalidDataException) {
                System.Console.WriteLine("ERROR: {0} contains invalid level data.", level);
                loadMainMenu();
            }

        }

        private void loadMainMenu() {
            System.Console.WriteLine("INFO: Returning to Main Menu");
            var gameEvent = new GameEvent();
            gameEvent.EventType = GameEventType.GameStateEvent;
            gameEvent.Message = "MAIN_MENU";
            BreakoutBus.GetBus().RegisterEvent(gameEvent);
        }

        private void createTimer() {
            if (MetaList.ContainsKey("Time")) {
                TimeLimit = new Timer(
                    new Vec2F(0.4f, 0.5f),
                    new Vec2F(0.5f, 0.5f),
                    Int32.Parse(MetaList["Time"])
                );
            } else {
                TimeLimit = null;
            }
        }

        public void CheckLevelOver() {
            var count = 0;
            foreach (var block in Blocks) {
                if (block.GetType() != typeof(UnbreakableBlock)) {
                    count++;
                }
            }
            if (count == 0) {
                NextLevel();
            }
        }
    }
}