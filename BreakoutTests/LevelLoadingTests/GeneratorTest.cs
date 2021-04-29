using NUnit.Framework;
using DIKUArcade.Entities;
using Breakout.Blocks;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using System.IO;
using DIKUArcade.GUI;
using System;
using Breakout.Levels;

namespace BreakoutTests {
    public class GeneratorTest {
        private LevelData leveldata;

        [SetUp]
        public void Setup() {
            //leveldata = new LevelData("level1");
        }

        [Test]
        public void TestEntityContainerExists() {
            System.Console.WriteLine(leveldata.Blocks.CountEntities());
        }
    }
}