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
            Window.CreateOpenGLContext();
        }

        [Test]
        public void TestEntityContainerExists() {
            leveldata = new LevelData("level1");
            Assert.That(leveldata.Blocks, Is.InstanceOf<EntityContainer<Block>>());
        }

        [Test]
        public void TestHandleMetaDifferences() {

            Assert.That(new LevelData("level1"), Is.InstanceOf<LevelData>());
            Assert.That(new LevelData("level2"), Is.InstanceOf<LevelData>());
            Assert.That(new LevelData("level3"), Is.InstanceOf<LevelData>());
            Assert.That(new LevelData("central-mass"), Is.InstanceOf<LevelData>());
            Assert.That(new LevelData("columns"), Is.InstanceOf<LevelData>());
            Assert.That(new LevelData("wall"), Is.InstanceOf<LevelData>());
        }
    }
}