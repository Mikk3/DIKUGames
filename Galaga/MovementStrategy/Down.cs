using DIKUArcade.Entities;
using DIKUArcade.Math;

namespace Galaga.MovementStrategy {
    public class Down : IMovementStrategy {
        public void MoveEnemy(Enemy enemy) {
            enemy.Shape.AsDynamicShape().Direction = new Vec2F(0.0f, -enemy.Speed);
            enemy.Shape.AsDynamicShape().Move();
            
        }

        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            foreach (Enemy enemy in enemies) {
                MoveEnemy(enemy);
            }
        }
    }
}