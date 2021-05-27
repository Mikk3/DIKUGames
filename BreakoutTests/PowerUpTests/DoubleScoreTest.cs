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
    public class DoubleScoreTest {

        Score score;
        private GameEventBus eventBus;

        public DoubleScoreTest() {
            eventBus = BreakoutBus.GetBus();
            eventBus.InitializeEventBus(new List<GameEventType>() {
                GameEventType.ControlEvent,
            });
        }

        [SetUp]
        public void Setup() {
            Window.CreateOpenGLContext();
            score = new Score(new Vec2F(0.1f,0.1f), new Vec2F(0.1f,0.1f));

            eventBus.Subscribe(GameEventType.ControlEvent, score);


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
    }
}