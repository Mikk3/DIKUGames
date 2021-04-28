using DIKUArcade.Entities;
using System.Collections.Generic;
using DIKUArcade.Graphics;
using DIKUArcade;
using DIKUArcade.Timers;
using System.IO;
using DIKUArcade.Math;
using DIKUArcade.EventBus;
using DIKUArcade.Physics;
using System;
using Galaga.MovementStrategy;

namespace Galaga.Squadron {

    public class StaggeredSquadron : ISquadron {
        public EntityContainer<Enemy> Enemies { get; }
        public int MaxEnemies { get; }
        private Down movementStrategy;
        public StaggeredSquadron() {
            MaxEnemies = 11;
            Enemies = new EntityContainer<Enemy>(MaxEnemies);
            movementStrategy = new Down();
        }

        public void CreateEnemies(List<Image> enemyStrides, List<Image> alternativeEnemyStrides) {
            for (int i = 0; i < 11; i += 2) {
                Enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.1f + ((float) i * (0.7f / 10.0f)), 1.2f),
                    new Vec2F(0.08f, 0.08f)),
                    new ImageStride(80, enemyStrides),
                    new ImageStride(80, alternativeEnemyStrides),
                    0.00025f
                ));
            }
            for (int i = 1; i < 10; i += 2) {
                Enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.1f + ((float) i * (0.7f / 10.0f)), 1.1f),
                    new Vec2F(0.08f, 0.08f)),
                    new ImageStride(80, enemyStrides),
                    new ImageStride(80, alternativeEnemyStrides),
                    0.00025f
                ));
            }
        }

        public void Move() {
            movementStrategy.MoveEnemies(Enemies);
        }
    }
}