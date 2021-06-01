using Breakout.Blocks;
using Breakout.Paddle;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using Breakout.PowerUps;

namespace Breakout
{
    public class Collisions {

        /// <summary>
        /// Call blocks OnHit method and balls CollideWithBlock method on collision.
        /// </summary>
        /// <param name="blocks">The EntityContainer that contains blocks</param>
        /// <param name="player">The player object</param>
        public static void CheckBallCollisionsWithBlock(Ball.Ball ball, EntityContainer<Block> blocks) {
            blocks.Iterate(block => {
                var data = CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), block.Shape.AsDynamicShape());
                if (data.Collision) {
                    block.OnHit();
                    ball.CollideWithBlock(block, data);
                }
            });
        }

        /// <summary>
        /// Call balls CollideWithPlayer method if it collides with the player
        /// </summary>
        /// <param name="ball">The ball object</param>
        /// <param name="player">The player object</param>
        public static void CheckBallCollisionWithPlayer(Ball.Ball ball, Player player) {
            var data = CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), player.Shape);
            if (data.Collision) {
                ball.CollideWithPlayer(player);
            }
        }

        /// <summary>
        /// Activates the powerup that collide with the player
        /// </summary>
        /// <param name="powerUp">The power up object</param>
        /// <param name="player">The player object</param>
        public static void CheckPowerUpCollisionWithPlayer(PowerUp powerUp, Player player) {
            var data = CollisionDetection.Aabb(powerUp.Shape.AsDynamicShape(), player.Shape);
            if (data.Collision) {
                powerUp.Activate();
            }
        }
    }
}