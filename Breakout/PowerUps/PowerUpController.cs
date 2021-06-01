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

        /// <summary>
        /// Creates a powerup at the given position with the given type and
        /// adds it to the PowerUps EntityContainer
        /// </summary>
        /// <param name="type">The type of power up</param>
        /// <param name="position">The position to create the powerup at</param>
        public void CreatePowerUp(PowerUpType type, Vec2F position) {
            var ent = PowerUpCreator.Create(type, position);

            PowerUps.AddEntity(ent);
        }

        /// <summary>
        /// Takes and random power up from the PowerUpType enums and
        /// call the CreatePowerUp method to generate the powerup
        /// </summary>
        /// <param name="position">The position to create the powerup at</param>
        public void CreateRandomPowerUp(Vec2F position) {
            var numOfPowerUps = 5;
            Random rand = new Random();
            PowerUpType type = (PowerUpType) rand.Next(numOfPowerUps);

            CreatePowerUp(type, position);

        }

        /// <summary>
        /// Iterate through all power ups and call their shapes move method
        /// </summary>
        public void MovePowerUps() {
            PowerUps.Iterate(x => {
                x.Shape.Move();
            });
        }

        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.ControlEvent && gameEvent.Message == "CREATE_POWERUP") {
                var pos = gameEvent.ObjectArg1;

                CreateRandomPowerUp((Vec2F) pos);

            }
        }
    }
}