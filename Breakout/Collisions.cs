using Breakout.Blocks;
using Breakout.Paddle;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Math;
using DIKUArcade.Physics;

namespace Breakout
{
    public class Collisions {


        public static void CheckBallCollisionsWithBlock(Ball.Ball ball, EntityContainer<Block> blocks) {
            blocks.Iterate(block => {
                var data = CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), block.Shape.AsDynamicShape());
                if (data.Collision) {
                    block.OnHit();
                    ball.CollideWithBlock(block, data);
                }
            });
        }

        public static void CheckBallCollisionWithPlayer(Ball.Ball ball, Player player) {
            var data = CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), player.Shape);
            if (data.Collision) {
                ball.CollideWithPlayer(player);
            }
        }
    }

}