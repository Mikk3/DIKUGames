using System.IO;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Events;
using System.Collections.Generic;
using System;
using Breakout.Levels;
using Breakout.Paddle;
using Breakout.Blocks;
using DIKUArcade.Physics;

namespace Breakout.Ball {
    public class Ball : Entity {

        public bool IsActive = false;

        public float speed;

        private DynamicShape shape;

        public Ball(DynamicShape shape, IBaseImage image) : base(shape, image) {
            this.shape = base.Shape.AsDynamicShape();
            shape.Direction = new Vec2F(0.005f, 0.01f);
        }

        public void Move() {
            if (IsActive) {
                checkBoundary();
                Shape.Move();
            } else {
                shape.Position = new Vec2F(0.5f,0.085f);
            }
        }

        public void SetMoveLeft(bool val) {
        }

        public void ChangeDirection(Vec2F dir) {
            shape.Direction = dir;
        }

        public void CollideWithPlayer(Player player) {
            if (shape.Position.X < player.Shape.Position.X + (0.5 * player.Shape.Extent.X)) {
                ChangeDirection(new Vec2F(Math.Abs(shape.Direction.X) * (-1), shape.Direction.Y * -1));
            } else {
                ChangeDirection(new Vec2F(Math.Abs(shape.Direction.X), shape.Direction.Y * -1));
            }
        }


        public void CollideWithBlock(Block block, CollisionData data) {

            switch (data.CollisionDir) {
                case (CollisionDirection.CollisionDirDown) :
                    ChangeDirection(new Vec2F(shape.Direction.X, shape.Direction.Y * (-1) ));
                    break;
                case (CollisionDirection.CollisionDirUp) :
                    ChangeDirection(new Vec2F(shape.Direction.X, shape.Direction.Y * (-1)));
                    break;
                case (CollisionDirection.CollisionDirLeft) :
                    ChangeDirection(new Vec2F(shape.Direction.X * (-1), shape.Direction.Y));
                    break;
                case (CollisionDirection.CollisionDirRight) :
                    ChangeDirection(new Vec2F(shape.Direction.X * (-1), shape.Direction.Y));
                    break;
            }
        }


        private void checkBoundary() {
            // Right Boundary
            if (shape.Position.X >= 1f - shape.Extent.X) {
                shape.Direction.X = shape.Direction.X * (-1);
            }
            // Left boundary
            if (shape.Position.X <= 0f) {
                shape.Direction.X = shape.Direction.X * (-1);
            }
            // Upper Boundary
            if (shape.Position.Y >= 1f - shape.Extent.Y) {
                shape.Direction.Y = shape.Direction.Y * (-1);
            }
        }
    }
}