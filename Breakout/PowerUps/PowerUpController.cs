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

        public EntityContainer<PowerUp> PowerUps;

        public PowerUpController() {
            PowerUps = new EntityContainer<PowerUp>();
        }

        public void CreatePowerup(PowerUpType type, Vec2F position) {
            var ent = PowerUpCreator.Create(type, position);

            PowerUps.AddEntity(ent);
        }

        public void CreateRandomPowerup(Vec2F position) {
            var numOfPowerUps = 5;
            Random rand = new Random();
            PowerUpType type = (PowerUpType) rand.Next(numOfPowerUps);

            CreatePowerup(type, position);

        }

        public void MovePowerUps() {
            PowerUps.Iterate(x => {
                x.Shape.Move();
            });
        }

        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.ControlEvent && gameEvent.Message == "CREATE_POWERUP") {
                var pos = gameEvent.ObjectArg1;

                CreateRandomPowerup((Vec2F) pos);

            }
        }
    }
}