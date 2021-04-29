using NUnit.Framework;
using DIKUArcade.Entities;
using Breakout.Blocks;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using System.IO;
using DIKUArcade.GUI;
using Breakout.Paddle;

namespace BreakoutTests {
    public class PlayerTest {

        Player player;

        [SetUp]
        public void Setup() {
            Window.CreateOpenGLContext();

            player = new Player(
                new DynamicShape(new Vec2F(0.50f, 0.035f), new Vec2F(0.224f, 0.044f)),
                new Image(Path.Combine("Assets", "Images", "player.png"))
            );
        }

        [Test]
        public void TestPlayerLeftWindowLimit() {
            player.SetMoveLeft(true);

            for (int i = 0; i < 500; i++) {
                player.Move();
            }
            Assert.Greater(player.Shape.Position.X, -0.00001f);

        }
        [Test]
        public void TestPlayerRightWindowLimits() {
            player.SetMoveRight(true);

            for (int i = 0; i < 500; i++) {
                player.Move();
            }
            Assert.Less(player.Shape.Position.X, 1.00001f);

        }

        [Test]
        public void TestPlayerIsEntity() {
            Assert.That(player, Is.InstanceOf<Entity>());
        }

        [Test]
        public void TestPlayerMoveLeft() {
            var playerOldPosition = player.Shape.Position;

            player.SetMoveLeft(true);
            player.Move();

            Assert.Less(player.Shape.Position.X, playerOldPosition.X);
        }

        [Test]
        public void TestPlayerMoveRight() {
            var playerOldPosition = player.Shape.Position;

            player.SetMoveRight(true);
            player.Move();

            Assert.Greater(player.Shape.Position.X, playerOldPosition.X);
        }
    }
}