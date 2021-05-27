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
    public class DoubleWidthTest {

        Player player;
        private GameEventBus eventBus;

        public DoubleWidthTest() {
            eventBus = BreakoutBus.GetBus();
            eventBus.InitializeEventBus(new List<GameEventType>() {
                GameEventType.ControlEvent,
                GameEventType.TimedEvent,
            });
        }

        [SetUp]
        public void Setup() {
            Window.CreateOpenGLContext();
            player = new Player(
                new DynamicShape(new Vec2F(0.50f, 0.035f), new Vec2F(0.224f, 0.044f)),
                new Image(Path.Combine("Assets", "Images", "player.png"))
            );

            eventBus.Subscribe(GameEventType.ControlEvent, player);
            eventBus.Subscribe(GameEventType.TimedEvent, player);


        }

        [Test]
        public void TestDoubleWidthActivate() {
            var powerup = PowerUpCreator.Create(PowerUpType.DoubleWidth, new Vec2F(0.1f,0.1f));
            var oldWidth = player.Shape.Extent;

            // Active double speed
            powerup.Activate();
            eventBus.ProcessEvents();

            var newWidth = player.Shape.Extent;

            Assert.Greater(newWidth.X, oldWidth.X);

        }
    }
}