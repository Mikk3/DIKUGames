using System;
using DIKUArcade.Entities;

namespace Galaga.MovementStrategy {
    public class ZigZagDown : IMovementStrategy {
        public void MoveEnemy(Enemy enemy) {
            var s = -enemy.Speed;
            var p = 0.085f;
            var a = 0.1f;
            enemy.Shape.Position.X = 0.45f + a * MathF.Sin( (2 * MathF.PI * (0.5f - enemy.Shape.Position.Y) / p) );
            enemy.Shape.Position.Y += s;
        }

        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            foreach (Enemy enemy in enemies) {
                MoveEnemy(enemy);
            }
        }
    }
}