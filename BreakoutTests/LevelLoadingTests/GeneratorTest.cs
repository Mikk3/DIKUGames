using NUnit.Framework;
using DIKUArcade.Entities;
using Breakout.Blocks;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using System.IO;
using DIKUArcade.GUI;
using System;
using Breakout.Levels;

namespace BreakoutTests.LevelLoadingTests {
    public class GeneratorTest {
        private LevelData leveldata;

        [SetUp]
        public void Setup() {
            Window.CreateOpenGLContext();
        }

        [Test]
        public void TestEntityContainerExists() {
            leveldata = new LevelData();
            Assert.That(leveldata.Blocks, Is.InstanceOf<EntityContainer<Block>>());
        }

        [Test]
        public void TestHandleMetaDifferences() {
            leveldata = new LevelData();

            Assert.That(leveldata, Is.InstanceOf<LevelData>());
            leveldata.NextLevel();
            Assert.That(leveldata, Is.InstanceOf<LevelData>());
            leveldata.NextLevel();
            Assert.That(leveldata, Is.InstanceOf<LevelData>());
            leveldata.NextLevel();
            Assert.That(leveldata, Is.InstanceOf<LevelData>());
            leveldata.NextLevel();
            Assert.That(leveldata, Is.InstanceOf<LevelData>());

            Assert.That(leveldata, Is.InstanceOf<LevelData>());
        }
    }
}