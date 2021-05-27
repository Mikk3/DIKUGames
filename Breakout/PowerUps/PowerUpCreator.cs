using System;
using Breakout.PowerUps;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using System.IO;

namespace Breakout.Powerups

{
    public class PowerUpCreator {
        public static Entity Create (PowerUpType type, Vec2F position) {
           switch (type)
            {
                case PowerUpType.DoubleScore:
                    return new DoubleScorePowerUp(
                        new DynamicShape(position, new Vec2F(0.05f, 0.05f)),
                        new Image(Path.Combine("Assets", "Images", "DoublePointPowerUp.png"))
                    );

                case PowerUpType.ExtraLife:
                    throw new NotImplementedException();

                case PowerUpType.ExtraBall:
                    throw new NotImplementedException();

                case PowerUpType.DoubleWidth:
                    throw new NotImplementedException();

                case PowerUpType.DoubleSpeed:
                   throw new NotImplementedException();

            }
            throw new ArgumentException("String did not match any GameStateTypes");
        }
    }
}