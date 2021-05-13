using NUnit.Framework;
using DIKUArcade.Entities;
using Breakout.Blocks;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using System.IO;
using DIKUArcade.GUI;
using Breakout.Ball;
using DIKUArcade.Physics;
using Breakout.Paddle;

namespace BreakoutTests {
    public class BallTest {

        private Ball ball;

        private Block block;
        private Player player;

        [SetUp]
        public void Setup() {
            Window.CreateOpenGLContext();

            ball = new Ball(
                new DynamicShape(new Vec2F(0.324f, 0.044f), new Vec2F(0.022f, 0.022f)),
                new Image(Path.Combine("Assets", "Images", "ball.png"))
            );
            block = new NormalBlock(
                new DynamicShape(new Vec2F(0.50f, 0.035f), new Vec2F(0.224f, 0.044f)),
                new Image(Path.Combine("Assets", "Images", "grey-block.png"))
            );
            player = new Player(
                new DynamicShape(new Vec2F(0.50f, 0.035f), new Vec2F(0.224f, 0.044f)),
                new Image(Path.Combine("Assets", "Images", "player.png"))
            );
        }

        [Test]
        public void TestBallIsEntity() {
            Assert.That(ball, Is.InstanceOf<Entity>());
        }

        [Test]
        public void TestBallBlockCollision() {
            var oldDirection = ball.Shape.AsDynamicShape().Direction;
            var data = new CollisionData();
            data.Collision = true;
            data.CollisionDir = CollisionDirection.CollisionDirUp;

            ball.CollideWithBlock(block, data);

            Assert.AreNotEqual(oldDirection, ball.Shape.AsDynamicShape().Direction);

        }

        [Test]
        public void TestBallPlayerCollision() {
            var oldDirection = ball.Shape.AsDynamicShape().Direction;

            ball.CollideWithPlayer(player);

            Assert.AreNotEqual(oldDirection, ball.Shape.AsDynamicShape().Direction);

        }

    }
}