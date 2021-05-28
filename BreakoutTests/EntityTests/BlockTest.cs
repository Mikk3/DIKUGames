using NUnit.Framework;
using DIKUArcade.Entities;
using Breakout.Blocks;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using System.IO;
using DIKUArcade.GUI;

namespace BreakoutTests.EntityTests {
    public class BlockTest {

        public Block block;

        [SetUp]
        public void Setup() {
            Window.CreateOpenGLContext();

            block = new NormalBlock(
                new DynamicShape(new Vec2F(0.50f, 0.035f), new Vec2F(0.224f, 0.044f)),
                new Image(Path.Combine("Assets", "Images", "grey-block.png"))
            );
        }

        [Test]
        public void TestBlockIsEntity() {
            Assert.That(block, Is.InstanceOf<Entity>());
        }

        [Test]
        public void TestDamageTaken() {
            var oldHealth = block.Health;
            block.OnHit();
            Assert.AreEqual(block.Health, oldHealth - 1);
        }

        [Test]
        public void TestBlockIsDestroyed() {
            for (int i = 0; i < 5; i++) {
                block.OnHit();
            }
            Assert.IsTrue(block.IsDeleted());
        }
    }
}