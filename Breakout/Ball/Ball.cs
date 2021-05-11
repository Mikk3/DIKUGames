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

namespace Breakout.Ball {
    public class Ball : Entity {

        private DynamicShape shape;
        private bool isActive = false;

        private Vec2F speed;

        public Ball(DynamicShape shape, IBaseImage image) : base(shape, image) {
            this.shape = Shape.AsDynamicShape();
            speed = new Vec2F(0.015f, 0.015f);
            shape.Direction = speed;
        }

        public void Move() {
            shape.Position = shape.Position + speed;
            checkBoundary();
        }

        public void SetMoveLeft(bool val) {
        }

        public void ChangeDirection(Vec2F newSpeed) {
            speed = newSpeed;
        }

        public void CollideWithPlayer(Player player) {
            ChangeDirection(new Vec2F(speed.X, speed.Y * (-1)));
        }


        public void CollideWithBlock(Block block) {
            ChangeDirection(new Vec2F(speed.X, speed.Y * (-1)));
        }


        private void checkBoundary() {
            // Right Boundary
            if (shape.Position.X >= 1f - shape.Extent.X) {
                speed.X = speed.X * (-1);
            }
            // Left boundary
            if (shape.Position.X <= 0f) {
                speed.X = speed.X * (-1);
            }
            // Upper Boundary
            if (shape.Position.Y >= 1f - shape.Extent.Y) {
                speed.Y = speed.Y * (-1);
            }
            // Temp lower Boundary
            if (shape.Position.Y <= 0f) {
                speed.Y = speed.Y * (-1);
            }
        }
    }
}