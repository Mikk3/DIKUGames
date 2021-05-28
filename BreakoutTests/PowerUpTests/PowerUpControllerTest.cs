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

namespace BreakoutTests.PowerUpTests {
    public class PowerUpControllerTest {

        PowerUpController controller;

        [SetUp]
        public void Setup() {
            Window.CreateOpenGLContext();
            controller = new PowerUpController();
        }

        [Test]
        public void TestCreatePowerUp() {
            controller.CreatePowerUp(PowerUpType.DoubleScore, new Vec2F(0.5f, 0.5f));
            Assert.IsNotEmpty(controller.PowerUps);
        }

        [Test]
        public void TestMovePowerUps() {
            var oldPos = new Vec2F(0.5f, 0.5f);
            controller.MovePowerUps();
            var newPos = new Vec2F(0.5f, 0.5f);

            foreach (PowerUp pow in controller.PowerUps) {
                newPos = pow.Shape.Position;
            }

            Assert.AreNotEqual(oldPos, newPos);
        }
    }
}