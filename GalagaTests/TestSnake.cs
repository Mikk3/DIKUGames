using NUnit.Framework;
using System.IO;
using Galaga;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using System.Collections.Generic;
using Galaga.MovementStrategy;
using Galaga.Squadron;

namespace GalagaTest
{
    public class TestSnake
    {
        [SetUp]
        public void Setup()
        {
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
            var snake = new SnakeSquadron();
            snake.CreateEnemies(enemyStridesBlue, enemyStridesRed);
            var enemy = snake.Enemies;
            var temp = new List<float>(snake.MaxEnemies);
            for (var i = 0; i < snake.MaxEnemies; i++) {
                temp.Add()
            }
            var tempY = enemy.Shape.Position.Y;

            
        
            var nextPos = enemy.Shape.Position.X = 0.45f + 0.1f * MathF.Sin((2 * MathF.PI * (0.5f - snake.Shape.Position.Y) / 0.085f) )
            
            Assert.
        }
    }
}