using System;
using Breakout.PowerUps;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using System.IO;

namespace Breakout.Powerups

{
    public class PowerUpCreator {
        public static PowerUp Create (PowerUpType type, Vec2F position) {
           switch (type)
            {
                case PowerUpType.DoubleScore:
                    return new DoubleScorePowerUp(
                        new DynamicShape(position, new Vec2F(0.05f, 0.05f)),
                        new Image(Path.Combine("Assets", "Images", "DoublePointPowerUp.png"))
                    );

                case PowerUpType.ExtraLife:
                    return new ExtraLifePowerUp(
                        new DynamicShape(position, new Vec2F(0.05f, 0.05f)),
                        new Image(Path.Combine("Assets", "Images", "LifePickUp.png"))
                    );

                case PowerUpType.DoubleSize:
                    return new DoubleSizePowerUp(
                        new DynamicShape(position, new Vec2F(0.05f, 0.05f)),
                        new Image(Path.Combine("Assets", "Images", "BigPowerUp.png"))
                    );

                case PowerUpType.DoubleWidth:
                    return new DoubleWidthPowerUp(
                        new DynamicShape(position, new Vec2F(0.05f, 0.05f)),
                        new Image(Path.Combine("Assets", "Images", "WidePowerUp.png"))
                    );

                case PowerUpType.DoubleSpeed:
                   return new DoubleSpeedPowerUp(
                        new DynamicShape(position, new Vec2F(0.05f, 0.05f)),
                        new Image(Path.Combine("Assets", "Images", "SpeedPickUp.png"))
                    );

            }
            throw new ArgumentException("Invalid state type");
        }
    }
}