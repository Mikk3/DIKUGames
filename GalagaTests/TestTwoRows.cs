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
    public class TestTwoRows
    {
        [SetUp]
        public void Setup()
        {
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
    }
}