using NUnit.Framework;
using System.IO;
using Galaga;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using System.Collections.Generic;
using Galaga.MovementStrategy;
using Galaga.Squadron;
using System;

namespace GalagaTest
{
    public class TestSnake
    {
        [SetUp]
        public void Setup()
        {
            DIKUArcade.Window.CreateOpenGLContext();
            snake = new SnakeSquadron();
            enemyStridesBlue = ImageStride.CreateStrides(4, Path.Combine("..", "Galaga", "Assets", "Images", "BlueMonster.png"));
            enemyStridesRed = ImageStride.CreateStrides(2, Path.Combine("..", "Galaga", "Assets", "Images", "RedMonster.png"));
            
            snake.CreateEnemies(enemyStridesBlue, enemyStridesRed);
        }

        List<Image> enemyStridesBlue;
        List<Image> enemyStridesRed;
        SnakeSquadron snake;

        [Test] 
        public void TestSnakeMaxEnemies()
        {
            Assert.AreEqual(snake.MaxEnemies, 7);
        }

        [Test] 
        public void TestSnakeCorrectMovement()
        {
            var nextPosList = new List<Vec2F>(7);

            var p = 0.085f;
            var a = 0.1f;
            foreach (Enemy enemy in snake.Enemies)
            {
                var s = -enemy.Speed;
                var pos = new Vec2F();

                pos.X = 0.45f + a * MathF.Sin( (2 * MathF.PI * (0.5f - enemy.Shape.Position.Y) / p) );
                pos.Y = enemy.Shape.Position.Y + s;

                nextPosList.Add(pos);
            }

            snake.Move();

            var newPosList = new List<Vec2F>(7);
            foreach (Enemy enemy in snake.Enemies)
            {
                newPosList.Add(enemy.Shape.Position);
            }
            

            
            for (int i=0; i < 7; i++) {                
                Assert.IsTrue(Math.Abs(newPosList[i].X - nextPosList[i].X) < 0.00001);
                Assert.IsTrue(Math.Abs(newPosList[i].Y - nextPosList[i].Y) < 0.00001);
            }

        }
    }
}