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

    public class TwoRowsSquadron : ISquadron {
        public EntityContainer<Enemy> Enemies { get; }
        public int MaxEnemies { get; }
        private Down movementStrategy;
        public TwoRowsSquadron() {
            MaxEnemies = 12;
            Enemies = new EntityContainer<Enemy>(MaxEnemies);
            movementStrategy = new Down();
        }

        public void CreateEnemies(List<Image> enemyStrides, List<Image> alternativeEnemyStrides) {
            for (int i = 0; i < 6; i++) {
                Enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.165f + ((float) i * (0.7f / 6.0f)), 1.0f),
                    new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStrides),
                    new ImageStride(80, alternativeEnemyStrides),
                    0.00025f
                ));
            }
            for (int i = 0; i < 6; i++) {
                Enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.165f + ((float) i * (0.7f / 6.0f)), 0.9f),
                    new Vec2F(0.1f, 0.1f)),
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