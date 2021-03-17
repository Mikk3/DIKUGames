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

    public class SnakeSquadron : ISquadron {
        public EntityContainer<Enemy> Enemies { get; }
        public int MaxEnemies { get; }
        private ZigZagDown movementStrategy;

        public SnakeSquadron() {
            MaxEnemies = 7;
            Enemies = new EntityContainer<Enemy>(MaxEnemies);
            movementStrategy = new ZigZagDown();
        }

        public void CreateEnemies(List<Image> enemyStrides, List<Image> alternativeEnemyStrides) {
            for (int i = 0; i < MaxEnemies; i++) {
                Enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.4f, (1.0f - ((float) i / 10.0f)) + 0.5f),
                    new Vec2F(0.075f, 0.075f)),
                    new ImageStride(80, enemyStrides),
                    new ImageStride(80, alternativeEnemyStrides),
                    0.0005f
                ));
            }
        }

        public void Move() {
            movementStrategy.MoveEnemies(Enemies);
        }
    }
}