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

namespace GalagaTest {
    public class TestStaggered {
        [SetUp]
        public void Setup() {
            DIKUArcade.Window.CreateOpenGLContext();
            staggered = new StaggeredSquadron();
            enemyStridesBlue = ImageStride.CreateStrides(4, Path.Combine("..", "Galaga", "Assets", "Images", "BlueMonster.png"));
            enemyStridesRed = ImageStride.CreateStrides(2, Path.Combine("..", "Galaga", "Assets", "Images", "RedMonster.png"));

            staggered.CreateEnemies(enemyStridesBlue, enemyStridesRed);
        }

        List<Image> enemyStridesBlue;
        List<Image> enemyStridesRed;
        StaggeredSquadron staggered;

        [Test]
        public void TestStaggeredMaxEnemies() {
            Assert.AreEqual(staggered.MaxEnemies, 11);
        }

        [Test]
        public void TestMovement() {
            List<Vec2F> posList = new List<Vec2F>(11);
            foreach (Enemy enemy in staggered.Enemies) {
                posList.Add(enemy.Shape.Position);
            }

            staggered.Move();

            List<Vec2F> newPosList = new List<Vec2F>(11);
            foreach (Enemy enemy in staggered.Enemies) {
                newPosList.Add(enemy.Shape.Position);
            }

            for (int i = 0; i < 11; i++) {
                Assert.IsTrue(Math.Abs(newPosList[i].X - posList[i].X) < 0.0001f);
                Assert.IsTrue(Math.Abs(newPosList[i].Y - (posList[i].Y - 0.00025f)) < 0.0001);
            }
        }
    }
}