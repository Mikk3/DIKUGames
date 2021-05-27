using NUnit.Framework;
using DIKUArcade.Entities;
using Breakout.Blocks;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using System.IO;
using DIKUArcade.GUI;
using Breakout;
using DIKUArcade.Events;
using System.Collections.Generic;
using Breakout.GameInfo;

namespace BreakoutTests {
    public class ScoreTest {

        private Block block;
        private Score score;
        private GameEventBus eventBus;

        public ScoreTest() {
            eventBus = BreakoutBus.GetBus();
            eventBus.InitializeEventBus(new List<GameEventType>() {
                GameEventType.ControlEvent,
            });
        }

        [SetUp]
        public void Setup() {
            Window.CreateOpenGLContext();

            score = new Score(new Vec2F(0.5f,0.5f), new Vec2F(0.5f,0.5f));

            eventBus.Subscribe(GameEventType.ControlEvent, score);

            block = new NormalBlock(
                new DynamicShape(new Vec2F(0.50f, 0.035f), new Vec2F(0.224f, 0.044f)),
                new Image(Path.Combine("Assets", "Images", "grey-block.png"))
            );
        }

        [Test]
        public void TestAddScoreBasedOnBlockValue() {
            var gameEvent = new GameEvent();
            gameEvent.EventType = GameEventType.ControlEvent;
            gameEvent.Message = "ADD_SCORE";
            gameEvent.IntArg1 = block.Value;
            eventBus.RegisterEvent(gameEvent);
            eventBus.ProcessEvents();

            Assert.That(score.score, Is.EqualTo(block.Value));
        }
    }
}