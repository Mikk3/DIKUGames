using NUnit.Framework;
using DIKUArcade.Entities;
using Breakout.Blocks;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using System.IO;
using DIKUArcade.GUI;
using System.Collections.Generic;
using Breakout.Levels;

namespace BreakoutTests.LevelLoadingTests {
    public class ParserTest {

        private LevelData leveldata;

        [SetUp]
        public void Setup() {
            Window.CreateOpenGLContext();
            leveldata = new LevelData();
            leveldata.NextLevel();
            leveldata.NextLevel();
        }

        [Test]
        public void TestParseRows() {
            List<string> expected = new List<string>();
            expected.Add("------------");
            expected.Add("------------");
            expected.Add("------------");
            expected.Add("------------");
            expected.Add("------------");
            expected.Add("------------");
            expected.Add("------------");
            expected.Add("------------");
            expected.Add("------------");
            expected.Add("------------");
            expected.Add("------------");
            expected.Add("------------");
            expected.Add("------------");
            expected.Add("-qqqqqqqqqq-");
            expected.Add("-qqqqqqqqqq-");
            expected.Add("-111----111-");
            expected.Add("-111-##-111-");
            expected.Add("-111-22-111-");
            expected.Add("-111-##-111-");
            expected.Add("-111----111-");
            expected.Add("-qqqqqqqqqq-");
            expected.Add("-qqqqqqqqqq-");
            expected.Add("------------");
            expected.Add("------------");
            Assert.AreEqual(expected, leveldata.RowsList);
        }

        [Test]
        public void TestParseMeta() {
            var expected = new Dictionary<string, string>();
            expected.Add("Name", "LEVEL 1");
            expected.Add("Time", "300");
            expected.Add("PowerUp", "2");
            Assert.AreEqual(expected, leveldata.MetaList);
        }

        [Test]
        public void TestParseImages() {
            var expected = new Dictionary<char, string>();
            expected.Add('#', "teal-block.png");
            expected.Add('1', "blue-block.png");
            expected.Add('2', "green-block.png");
            expected.Add('q', "darkgreen-block.png");
            Assert.AreEqual(expected, leveldata.LegendList);
        }
    }
}