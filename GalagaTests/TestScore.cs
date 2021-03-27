using Galaga;
using NUnit.Framework;
using System;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;

namespace GalagaTest
{
    public class TestScore
    {
        [SetUp]
        public void Setup()
        {
            DIKUArcade.Window.CreateOpenGLContext();
            score = new Score(new Vec2F(0.0f, 0.5f), new Vec2F(0.5f, 0.5f));
        }

        Score score;

        [Test]
        public void AddPointToScore()
        {
            score.AddPoint();

            Assert.AreEqual(1, score.score);
        }
    }
}