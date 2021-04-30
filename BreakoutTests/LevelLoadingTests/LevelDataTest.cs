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
        List<string> data;

        [SetUp]
        public void Setup() {
            Window.CreateOpenGLContext();
        }

        [Test]
        public void TestOpenNonEmptyFile() {
            Assert.That(() => new LevelData("level1"), Throws.Nothing);
        }

        [Test]
        public void TestOpenEmptyFile() {
            Assert.That(() => new LevelData("fileThatDoNotExist"), Throws.Nothing);

        }

        [Test]
        public void TestOpenInvalidFile() {
            Assert.That(() => new LevelData("test-emptyfile"), Throws.Nothing);

        }
    }
}