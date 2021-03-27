using Galaga;
using NUnit.Framework;
using System;
using System.IO;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.EventBus;
using System.Collections.Generic;

namespace GalagaTest {
    public class TestPlayer {
        [SetUp]
        public void Setup() {
            DIKUArcade.Window.CreateOpenGLContext();
            window = new Window("Galaga", 500, 500);

            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("..", "Galaga", "Assets", "Images", "Player.png")));

            eventBus = new GameEventBus<object>();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent });
            window.RegisterEventBus(eventBus);
            eventBus.Subscribe(GameEventType.InputEvent, player);
        }

        Player player;
        GameEventBus<object> eventBus;
        Window window;

        [Test]
        public void TestPlayerMoveRight() {
            var posX = player.shape.Position.X;
            var nextPosX = player.shape.Position.X + player.MOVEMENT_SPEED;

            eventBus.RegisterEvent(
                GameEventFactory<object>.CreateGameEventForAllProcessors(
                    GameEventType.InputEvent,
                    this,
                    "KEY_RIGHT",
                    "KEY_PRESS", ""
                )
            );
            eventBus.ProcessEventsSequentially();

            player.Move();

            Assert.IsTrue((player.shape.Position.Y - 0.1f) < 0.0001f);
            Assert.IsTrue((player.shape.Position.X - nextPosX) < 0.0001f);

        }

        [Test]
        public void TestPlayerMoveLeft() {
            var posX = player.shape.Position.X;
            var nextPosX = player.shape.Position.X + player.MOVEMENT_SPEED;
            var dir = player.shape.Direction;

            eventBus.RegisterEvent(
                GameEventFactory<object>.CreateGameEventForAllProcessors(
                    GameEventType.InputEvent,
                    this,
                    "KEY_LEFT",
                    "KEY_PRESS", ""
                )
            );
            eventBus.ProcessEventsSequentially();

            player.Move();

            Assert.IsTrue((player.shape.Position.Y - 0.1f) < 0.0001f);
            Assert.IsTrue((player.shape.Position.X - nextPosX) < 0.0001f);

            

        }

        [Test]
        public void TestKeyReleaseRight() {
            var dir = player.shape.Direction;

            eventBus.RegisterEvent(
                GameEventFactory<object>.CreateGameEventForAllProcessors(
                    GameEventType.InputEvent,
                    this,
                    "KEY_RIGHT",
                    "KEY_PRESS", ""
                )
            );
            eventBus.ProcessEventsSequentially();

            player.Move();

            eventBus.RegisterEvent(
                GameEventFactory<object>.CreateGameEventForAllProcessors(
                    GameEventType.InputEvent,
                    this,
                    "KEY_RIGHT",
                    "KEY_RELEASE", ""
                )
            );
            eventBus.ProcessEventsSequentially();

            Assert.IsTrue(Math.Abs(dir.X) < 0.0001);
            Assert.IsTrue(Math.Abs(dir.Y) < 0.0001);

        }
        
        [Test]
        public void TestKeyReleaseLeft() {
            var dir = player.shape.Direction;

            eventBus.RegisterEvent(
                GameEventFactory<object>.CreateGameEventForAllProcessors(
                    GameEventType.InputEvent,
                    this,
                    "KEY_LEFT",
                    "KEY_PRESS", ""
                )
            );
            eventBus.ProcessEventsSequentially();

            player.Move();

            eventBus.RegisterEvent(
                GameEventFactory<object>.CreateGameEventForAllProcessors(
                    GameEventType.InputEvent,
                    this,
                    "KEY_LEFT",
                    "KEY_RELEASE", ""
                )
            );

            eventBus.ProcessEventsSequentially();


            Assert.IsTrue(Math.Abs(dir.X) < 0.0001);
            Assert.IsTrue(Math.Abs(dir.Y) < 0.0001);
        }

    }
}