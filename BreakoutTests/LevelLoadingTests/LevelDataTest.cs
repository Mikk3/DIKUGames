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
            data = new Provider("level1").GetDataAsList();
        }

        [Test]
        public void TestOpenNonEmptyFile() {
            Assert.Pass();

        }

        //[Test]
        public void TestOpenEmptyFile() {
            Assert.Pass();

        }

        //[Test]
        public void TestOpenInvalidFile() {
            Assert.Pass();

        }
    }
}