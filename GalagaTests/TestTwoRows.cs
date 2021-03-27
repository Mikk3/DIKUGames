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
    public class TestTwoRows
    {
        [SetUp]
        public void Setup()
        {
            DIKUArcade.Window.CreateOpenGLContext();
            twoRows = new TwoRowsSquadron();
            enemyStridesBlue = ImageStride.CreateStrides(4, Path.Combine("..", "Galaga", "Assets", "Images", "BlueMonster.png"));
            enemyStridesRed = ImageStride.CreateStrides(2, Path.Combine("..", "Galaga", "Assets", "Images", "RedMonster.png"));
            
            twoRows.CreateEnemies(enemyStridesBlue, enemyStridesRed);
        }

        List<Image> enemyStridesBlue;
        List<Image> enemyStridesRed;
        TwoRowsSquadron twoRows;

        [Test] 
        public void TestTwoRowsMaxEnemies()
        {
            Assert.AreEqual(twoRows.MaxEnemies, 12);
        }

        [Test]
        public void TestMovement() {
            List<Vec2F> posList = new List<Vec2F>(12);
            foreach (Enemy enemy in twoRows.Enemies) {
                posList.Add(enemy.Shape.Position);
            }

            twoRows.Move();

            List<Vec2F> newPosList = new List<Vec2F>(12);
            foreach (Enemy enemy in twoRows.Enemies) {
                newPosList.Add(enemy.Shape.Position);
            }

            for (int i = 0; i < 12; i++) {
                Assert.IsTrue(Math.Abs(newPosList[i].X - posList[i].X) < 0.0001f);
                Assert.IsTrue(Math.Abs(newPosList[i].Y - (posList[i].Y - 0.00025f)) < 0.0001);
            }
        }
    }
}