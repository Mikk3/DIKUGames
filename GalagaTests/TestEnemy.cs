using NUnit.Framework;
using System.IO;
using Galaga;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using System.Collections.Generic;

namespace GalagaTest
{
    public class TestEnemy
    {
        [SetUp]
        public void Setup()
        {
            DIKUArcade.Window.CreateOpenGLContext();
            enemyStridesBlue = ImageStride.CreateStrides(4, Path.Combine("..", "Galaga", "Assets", "Images", "BlueMonster.png"));
            enemyStridesRed = ImageStride.CreateStrides(2, Path.Combine("..", "Galaga", "Assets", "Images", "RedMonster.png"));
            shape = new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f));
        }

        List<Image> enemyStridesBlue;
        List<Image> enemyStridesRed;
        DynamicShape shape;

        [Test]
        public void TestIncreaseDifficulty()
        {
            Enemy enemy = new Enemy(shape, new ImageStride(80, enemyStridesBlue), new ImageStride(80, enemyStridesRed), 1.0f);
            Enemy.IncreaseDifficulty();
            Enemy enemy1 = new Enemy(shape, new ImageStride(80, enemyStridesBlue), new ImageStride(80, enemyStridesRed), 1.0f);
            
            Assert.AreNotEqual(enemy.Speed, enemy1.Speed);
        }
        
        [Test]
        public void TestDamageTaken()
        {
                
            Enemy enemy = new Enemy(shape, new ImageStride(80, enemyStridesBlue), new ImageStride(80, enemyStridesRed), 1.0f);
            enemy.Damage();
            Assert.AreEqual(enemy.hitpoints, 1);
        }

        [Test]
        public void TestEnemyEnrageSpeed()
        {
            var enemy = new Enemy(shape, new ImageStride(80, enemyStridesBlue), new ImageStride(80, enemyStridesRed), 1.0f);
            var speedBeforeEnrage = enemy.Speed;
            
            enemy.Enrage();
            
            Assert.AreEqual(enemy.Speed, speedBeforeEnrage * 1.50f);
        }

        [Test]
        public void TestIsDead() {
            var enemy = new Enemy(shape, new ImageStride(80, enemyStridesBlue), new ImageStride(80, enemyStridesRed), 1.0f);
            enemy.Damage();
            enemy.Damage();

            Assert.IsTrue(enemy.isDead());
                    
        }

        [Test]
        public void TestEnemyEnrageImage()
        {
            var enemy = new Enemy(shape, new ImageStride(80, enemyStridesBlue), new ImageStride(80, enemyStridesRed), 1.0f);
            
            enemy.Enrage();
            Assert.AreEqual(enemy.Image, enemy.alternativeImage);
        }
    }
}