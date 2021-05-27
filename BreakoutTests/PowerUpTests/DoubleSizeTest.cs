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
using Breakout;
using System.Collections.Generic;
using DIKUArcade.Events;
using Breakout.Ball;
using Breakout.Powerups;

namespace BreakoutTests {
    public class DoubleSizeTest {

        Ball ball;
        private GameEventBus eventBus;

        public DoubleSizeTest() {
            eventBus = BreakoutBus.GetBus();
            eventBus.InitializeEventBus(new List<GameEventType>() {
                GameEventType.ControlEvent,
                GameEventType.TimedEvent,
            });
        }

        [SetUp]
        public void Setup() {
            Window.CreateOpenGLContext();
            ball = new Ball(
                new DynamicShape(new Vec2F(0.324f, 0.044f), new Vec2F(0.022f, 0.022f)),
                new Image(Path.Combine("Assets", "Images", "ball.png"))
            );
            eventBus.Subscribe(GameEventType.ControlEvent, ball);
            eventBus.Subscribe(GameEventType.TimedEvent, ball);


        }

        [Test]
        public void TestDoubleSizeActivate() {
            var powerup = PowerUpCreator.Create(PowerUpType.DoubleSize, new Vec2F(0.1f,0.1f));
            var oldSize = ball.Shape.Extent;

            powerup.Activate();

            eventBus.ProcessEvents();

            Assert.Greater(ball.Shape.Extent.X, oldSize.X);
            Assert.Greater(ball.Shape.Extent.Y, oldSize.Y);


        }
    }
}