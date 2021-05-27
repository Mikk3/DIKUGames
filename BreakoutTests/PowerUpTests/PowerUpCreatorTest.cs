using NUnit.Framework;
using DIKUArcade.Entities;
using Breakout.Blocks;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using System.IO;
using DIKUArcade.GUI;
using System;
using Breakout.Levels;
using Breakout.PowerUps;
using Breakout.Powerups;

namespace BreakoutTests {
    public class PowerUpCreatorTest {
        [SetUp]
        public void Setup() {
            Window.CreateOpenGLContext();
        }

        [Test]
        public void TestCreate() {
            var DoubleScore = PowerUpCreator.Create(PowerUpType.DoubleScore, new Vec2F(0.1f,0.1f));
            var ExtraLife = PowerUpCreator.Create(PowerUpType.ExtraLife, new Vec2F(0.1f,0.1f));
            var DoubleSize = PowerUpCreator.Create(PowerUpType.DoubleSize, new Vec2F(0.1f,0.1f));
            var DoubleWidth = PowerUpCreator.Create(PowerUpType.DoubleWidth, new Vec2F(0.1f,0.1f));
            var DoubleSpeed = PowerUpCreator.Create(PowerUpType.DoubleSpeed, new Vec2F(0.1f,0.1f));

            Assert.That(DoubleScore, Is.InstanceOf<DoubleScorePowerUp>());
            Assert.That(ExtraLife, Is.InstanceOf<ExtraLifePowerUp>());
            Assert.That(DoubleSize, Is.InstanceOf<DoubleSizePowerUp>());
            Assert.That(DoubleWidth, Is.InstanceOf<DoubleWidthPowerUp>());
            Assert.That(DoubleSpeed, Is.InstanceOf<DoubleSpeedPowerUp>());

        }

    }
}