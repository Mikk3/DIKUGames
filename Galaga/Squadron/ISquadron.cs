using DIKUArcade.Entities;
using System.Collections.Generic;
using DIKUArcade.Graphics;
using Galaga.MovementStrategy;

namespace Galaga.Squadron {

    public interface ISquadron {
        EntityContainer<Enemy> Enemies { get; }
        int MaxEnemies { get; }

        void CreateEnemies(List<Image> enemyStrides, List<Image> alternativeEnemyStrides);

    }
}