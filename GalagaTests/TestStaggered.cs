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
    public class TestStaggered
    {
        [SetUp]
        public void Setup()
        {
            staggered = new StaggeredSquadron();
            enemyStridesBlue = ImageStride.CreateStrides(4, Path.Combine("..", "Galaga", "Assets", "Images", "BlueMonster.png"));
            enemyStridesRed = ImageStride.CreateStrides(2, Path.Combine("..", "Galaga", "Assets", "Images", "RedMonster.png"));
            
            staggered.CreateEnemies(enemyStridesBlue, enemyStridesRed);
        }

        List<Image> enemyStridesBlue;
        List<Image> enemyStridesRed;
        StaggeredSquadron staggered;

        [Test] 
        public void TestStaggeredMaxEnemies()
        {
            Assert.AreEqual(staggered.MaxEnemies, 11);
        }
    }
}