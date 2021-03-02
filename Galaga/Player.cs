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

        public Player(DynamicShape shape, IBaseImage image)
        {
            entity = new Entity(shape, image);
            this.shape = shape;
        }

        public void Render()
        {
            entity.RenderEntity();
        }

        public void Move()
        {
            shape.Move(shape.Direction);
        }
        public void SetMoveLeft(bool val)
        {
            if (val) {
                moveLeft = -MOVEMENT_SPEED;
            }
            else {
                moveLeft = 0.00f;
            }
            UpdateDirection();
        }
        public void SetMoveRight(bool val)
        {
            // TODO:set moveRight appropriately and call UpdateMovement()
            if (val) {
                moveRight = MOVEMENT_SPEED;
            } 
            else {
                moveRight = 0.00f;
            }
            UpdateDirection();
        }
        private void UpdateDirection() {
            // (moveLeft+moveRight), 0.0f
            shape.ChangeDirection(new Vec2F((moveLeft+moveRight), 0.0f));
        }

    }
}