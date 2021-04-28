using System.Collections.Generic;
using NUnit.Framework;

namespace BreakoutTests
{
    public class ParserTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestParseRows()
        {
            List<string> expected = new List<string>();
            expected.Add("------------");
            expected.Add("------------");
            expected.Add("-qqqqqqqqqq-");
            expected.Add("-qqqqqqqqqq-");
            expected.Add("-111----111-");
            expected.Add("-111-##-111-");
            expected.Add("-111-22-111-");
            expected.Add("-111-##-111-");
            expected.Add("-111----111-");
            expected.Add("-##########-");
            expected.Add("-##########-");
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
            expected.Add("------------");
            Assert.Fail();
        }

        [Test]
        public void TestParseMeta()
        {
            var expected = new List<KeyValuePair<string, string>>();
            expected.Add(new KeyValuePair<string, string>("name", "LEVEL 1"));
            expected.Add(new KeyValuePair<string, string>("time", "300"));
            expected.Add(new KeyValuePair<string, string>("hardened", "#"));
            expected.Add(new KeyValuePair<string, string>("powerup", "2"));
            Assert.Fail();

        }

        [Test]
        public void TestParseImages()
        {
            var expected = new List<KeyValuePair<char, string>>();
            expected.Add(new KeyValuePair<char, string>('#', "teal-block.png"));
            expected.Add(new KeyValuePair<char, string>('1', "blue-block.png"));
            expected.Add(new KeyValuePair<char, string>('2', "green-block.png"));
            expected.Add(new KeyValuePair<char, string>('q', "darkgreen-block.png"));
            Assert.Fail();
        }
    }
}