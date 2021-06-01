using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.Blocks {

    public abstract class Block : Entity {
        public int Health { get; internal set; }
        public int Value { get; internal set; }

        public Block(Shape shape, IBaseImage image) : base(shape, image) {

        }

        /// <summary>
        /// The behavior of the block when it is hit
        /// </summary>
        public abstract void OnHit();


        /// <summary>
        /// The behavior of the block when it is destroyed
        /// </summary>
        public abstract void OnDestroy();

    }

}