using DIKUArcade.Entities;

using DIKUArcade.Graphics;

namespace Breakout.Blocks {

    public class UnbreakableBlock : Block {

        public UnbreakableBlock(DynamicShape shape, IBaseImage image) : base(shape, image) {
            base.Health = 1;
            base.Value = 0;
        }

        public override void OnHit() {
            // Nothing, its unbreakable
        }

        public override void OnDestroy() {
            this.DeleteEntity();
        }
    }

}