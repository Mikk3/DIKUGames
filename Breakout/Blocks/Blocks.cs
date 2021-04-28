using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Blocks {

    public abstract class Block : Entity {
        public int Health { get; internal set; }
        public int Value { get; internal set; }

        public Block(DynamicShape shape, IBaseImage image) : base(shape, image) {

        }

        public abstract void OnHit();

        public abstract void OnDestroy();

    }

}