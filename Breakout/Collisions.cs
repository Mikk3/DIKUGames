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
                var data = CollisionDetection.Aabb(ball.shape, block.Shape);
                if (data.Collision) {
                    block.OnHit();
                    ball.CollideWithBlock(block, data.CollisionDir);
                    System.Console.WriteLine(data.CollisionDir);
                    System.Console.WriteLine(ball.Shape.AsDynamicShape().Direction * data.DirectionFactor);
                }
            });
        }

        public static void CheckBallCollisionWithPlayer(Ball.Ball ball, Player player) {
            var data = CollisionDetection.Aabb(ball.shape, player.Shape);
            if (data.Collision) {
                ball.CollideWithPlayer(player);
            }
        }
    }

}