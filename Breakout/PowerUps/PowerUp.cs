using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using OpenTK.Graphics.OpenGL;

namespace Breakout.PowerUps {

    public abstract class PowerUp : Entity {

        public PowerUp(Shape shape, IBaseImage image) : base(shape, image) {
            // All Powerups move downwards
            base.Shape.AsDynamicShape().Direction = new Vec2F(0.0f, -0.005f);
        }

        /// <summary>
        /// Activates the effect of the power up
        /// </summary>
        public abstract void Activate();
    }
}
