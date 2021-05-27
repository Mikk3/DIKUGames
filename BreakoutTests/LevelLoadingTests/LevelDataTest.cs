using NUnit.Framework;
using DIKUArcade.Entities;
using Breakout.Blocks;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using System.IO;
using DIKUArcade.GUI;
using Breakout.Levels;
using System.Collections.Generic;
using System;

namespace BreakoutTests {
    public class ProviderTest {
        LevelData levelData;

        [SetUp]
        public void Setup() {
            Window.CreateOpenGLContext();
            levelData = new LevelData();

        }

        [Test]
        public void TestOpenNonEmptyFile() {
            Assert.That(() => new LevelData(), Throws.Nothing);
        }

        [Test]
        public void TestCheckLevelOver() {
            var oldName = levelData.MetaList["Name"];

            levelData.Blocks.Iterate(x => {
                x.DeleteEntity();
            });

            levelData.CheckLevelOver();

            Assert.AreNotEqual(levelData.MetaList["Name"], oldName);
        }

    }
}