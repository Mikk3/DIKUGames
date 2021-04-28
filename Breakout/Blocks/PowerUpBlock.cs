using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Blocks {

    public class PowerUpBlock : Block
    {

        public PowerUpBlock(DynamicShape shape, IBaseImage image) : base(shape, image) {
            base.Health = 2;
            base.Value = 5;

        }

        public override void OnHit()
        {
            base.Health--;
        }

        public override void OnDestroy()
        {
            // Create Powerup
            // Change player score using event?

            this.DeleteEntity();
        }
    }

}