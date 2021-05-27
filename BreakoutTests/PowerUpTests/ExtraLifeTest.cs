using NUnit.Framework;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using System.IO;
using DIKUArcade.GUI;
using Breakout.PowerUps;
using Breakout;
using System.Collections.Generic;
using DIKUArcade.Events;
using Breakout.Ball;
using Breakout.Powerups;
using Breakout.GameInfo;
using Breakout.Paddle;

namespace BreakoutTests {
    public class ExtraLifeTest {

        Lives lives;
        private GameEventBus eventBus;

        public ExtraLifeTest() {
            eventBus = BreakoutBus.GetBus();
            eventBus.InitializeEventBus(new List<GameEventType>() {
                GameEventType.ControlEvent
            });
        }

        [SetUp]
        public void Setup() {
            Window.CreateOpenGLContext();
            lives = new Lives(new Vec2F(0.1f,0.1f), new Vec2F(0.1f,0.1f));

            eventBus.Subscribe(GameEventType.ControlEvent, lives);


        }

        [Test]
        public void TestExtraLifeActivate() {
            var powerup = PowerUpCreator.Create(PowerUpType.ExtraLife, new Vec2F(0.1f,0.1f));
            var oldLives = lives.Value;

            // Active double speed
            powerup.Activate();
            eventBus.ProcessEvents();

            var newLives = lives.Value;

            Assert.Greater(newLives, oldLives);

        }
    }
}