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

namespace BreakoutTests.PowerUpTests {
    public class PowerUpActivationTest {

        private GameEventBus eventBus;
        Score score;
        Lives lives;
        Player player;
        Ball ball;

        public PowerUpActivationTest() {
            eventBus = BreakoutBus.GetBus();
            eventBus.InitializeEventBus(new List<GameEventType>() {
                GameEventType.ControlEvent,
                GameEventType.TimedEvent,
            });
        }

        [SetUp]
        public void Setup() {
            Window.CreateOpenGLContext();

            // Score
            score = new Score(new Vec2F(0.1f,0.1f), new Vec2F(0.1f,0.1f));
            eventBus.Subscribe(GameEventType.ControlEvent, score);

            // Lives
            lives = new Lives(new Vec2F(0.1f,0.1f), new Vec2F(0.1f,0.1f));
            eventBus.Subscribe(GameEventType.ControlEvent, lives);

            // Player
            player = new Player(
                new DynamicShape(new Vec2F(0.50f, 0.035f), new Vec2F(0.224f, 0.044f)),
                new Image(Path.Combine("Assets", "Images", "player.png"))
            );
            eventBus.Subscribe(GameEventType.ControlEvent, player);
            eventBus.Subscribe(GameEventType.TimedEvent, player);

            // Ball
            ball = new Ball(
                new DynamicShape(new Vec2F(0.324f, 0.044f), new Vec2F(0.022f, 0.022f)),
                new Image(Path.Combine("Assets", "Images", "ball.png"))
            );
            eventBus.Subscribe(GameEventType.ControlEvent, ball);
            eventBus.Subscribe(GameEventType.TimedEvent, ball);

        }

        [Test]
        public void TestDoubleScoreActivate() {
            var powerup = PowerUpCreator.Create(PowerUpType.DoubleScore, new Vec2F(0.1f,0.1f));

            // Active double score
            powerup.Activate();

            // Add Score
            var gameEvent = new GameEvent();
            gameEvent.EventType = GameEventType.ControlEvent;
            gameEvent.Message = "ADD_SCORE";
            gameEvent.IntArg1 = 5;
            eventBus.RegisterEvent(gameEvent);

            eventBus.ProcessEvents();

            Assert.AreEqual(score.Value, 10);

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