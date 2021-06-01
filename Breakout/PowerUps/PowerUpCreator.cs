using System;
using Breakout.PowerUps;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using System.IO;
using DIKUArcade.Utilities;

namespace Breakout.Powerups

{
    public class PowerUpCreator {

        /// <summary>
        /// Returns a instance of powerup with the given type and position
        /// </summary>
        /// <param name="type">The type of power up</param>
        /// <param name="position">The position to create the powerup at</param>
        /// <returns>An instance of PowerUp</returns>

        public static PowerUp Create (PowerUpType type, Vec2F position) {
           switch (type)
            {
                case PowerUpType.DoubleScore:
                    return new DoubleScorePowerUp(
                        new DynamicShape(position, new Vec2F(0.05f, 0.05f)),
                        new Image(Path.Combine(FileIO.GetProjectPath(), "Assets", "Images", "DoublePointPowerUp.png"))
                    );

                case PowerUpType.ExtraLife:
                    return new ExtraLifePowerUp(
                        new DynamicShape(position, new Vec2F(0.05f, 0.05f)),
                        new Image(Path.Combine(FileIO.GetProjectPath(), "Assets", "Images", "LifePickUp.png"))
                    );

                case PowerUpType.DoubleSize:
                    return new DoubleSizePowerUp(
                        new DynamicShape(position, new Vec2F(0.05f, 0.05f)),
                        new Image(Path.Combine(FileIO.GetProjectPath(), "Assets", "Images", "BigPowerUp.png"))
                    );

                case PowerUpType.DoubleWidth:
                    return new DoubleWidthPowerUp(
                        new DynamicShape(position, new Vec2F(0.05f, 0.05f)),
                        new Image(Path.Combine(FileIO.GetProjectPath(), "Assets", "Images", "WidePowerUp.png"))
                    );

                case PowerUpType.DoubleSpeed:
                   return new DoubleSpeedPowerUp(
                        new DynamicShape(position, new Vec2F(0.05f, 0.05f)),
                        new Image(Path.Combine(FileIO.GetProjectPath(), "Assets", "Images", "SpeedPickUp.png"))
                    );

            }

            throw new ArgumentException("Invalid powerup type");
        }
    }
}