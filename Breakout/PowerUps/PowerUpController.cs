using System;
using System.Security.Cryptography.X509Certificates;
using Breakout.Powerups;
using Breakout.PowerUps;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Events.Generic;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using OpenTK.Graphics.OpenGL;

namespace Breakout.PowerUps {

    public class PowerUpController : IGameEventProcessor {

        public EntityContainer<Entity> PowerUps;

        public PowerUpController() {
            PowerUps = new EntityContainer<Entity>();
        }

        public void CreatePowerup(PowerUpType type, Vec2F position) {
            var ent = PowerUpCreator.Create(type, position);

            PowerUps.AddEntity(ent);
        }

        public void CreateRandomPowerup(Vec2F position) {
            var numOfPowerUps = 5;
            int rand = new Random().Next(numOfPowerUps-1);
            CreatePowerup(PowerUpType.DoubleScore, position);

        }

        public void MovePowerUps() {
            PowerUps.Iterate(x => {
                x.Shape.Move();
            });

        }

        public void ProcessEvent(GameEvent gameEvent) {
            throw new NotImplementedException();
        }
    }
}