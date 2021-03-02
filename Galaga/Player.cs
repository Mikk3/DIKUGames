using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
namespace Galaga
{
    public class Player
    {
        private Entity entity;
        private DynamicShape shape;

        private float moveLeft = 0.0f;

        private float moveRight = 0.0f;

        private const float MOVEMENT_SPEED = 0.01f;

        public Player(DynamicShape shape, IBaseImage image) {
            entity = new Entity(shape, image);
            this.shape = shape;
        }

        public void Render() {
            entity.RenderEntity();
        }

        public void Move() {
            // Right boundary
            if (shape.Position.X > (0.99f - shape.Extent.X) && shape.Direction.X > 0.0f) {
                return;
            }

            // Left boundary
            if (shape.Position.X < 0.01f && shape.Direction.X < 0.0f) {
                return;
            }

            shape.Move(shape.Direction);
        }
        public void SetMoveLeft(bool val) {
            if (val) {
                moveLeft = -MOVEMENT_SPEED;
            } else {
                moveLeft = 0.00f;
            }
            UpdateDirection();
        }
        public void SetMoveRight(bool val) {
            if (val) {
                moveRight = MOVEMENT_SPEED;
            } else {
                moveRight = 0.00f;
            }
            UpdateDirection();
        }
        private void UpdateDirection()
        {
            shape.ChangeDirection(new Vec2F((moveLeft + moveRight), 0.0f));
        }

        public Vec2F GetTipPosition() {
            // returns the position of the tip of the player
            return shape.Position + new Vec2F(shape.Extent.X / 2, shape.Extent.Y);
        }

    }
}