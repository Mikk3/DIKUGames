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

namespace BreakoutTests {
    public class DoubleSpeedTest {

        Ball ball;
        private GameEventBus eventBus;

        public DoubleSpeedTest() {
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
        public void TestDoubleSpeedActivate() {
            var powerup = PowerUpCreator.Create(PowerUpType.DoubleSpeed, new Vec2F(0.1f,0.1f));
            var oldSpeed = ball.Shape.AsDynamicShape().Direction;

            // Active double speed
            powerup.Activate();
            eventBus.ProcessEvents();

            var newSpeed = ball.Shape.AsDynamicShape().Direction;

            Assert.Greater(newSpeed.X, oldSpeed.X);
            Assert.Greater(newSpeed.Y, oldSpeed.Y);

        }
    }
}