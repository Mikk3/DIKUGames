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
using DIKUArcade.Events.Generic;

namespace Breakout.Ball {
    public class Ball : Entity, IGameEventProcessor {
        private bool isActive;

        private DynamicShape shape;

        private bool doubleSizeEnabled = false;
        private bool doubleSpeedEnabled = false;

        public Ball(DynamicShape shape, IBaseImage image) : base(shape, image) {
            isActive = false;
            this.shape = base.Shape.AsDynamicShape();
            shape.Direction = new Vec2F(0.005f, 0.01f);
        }

        public void Move() {
            if (isActive) {
                checkBoundary();
                Shape.Move();
            } else {
                shape.Position = new Vec2F(0.5f,0.085f);
            }
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

            // Reset and take life if ball reaches lower boundary.
            if (shape.Position.Y <= 0f) {
                isActive = false;
                var gameEvent = new GameEvent();
                gameEvent.EventType = GameEventType.ControlEvent;
                gameEvent.Message = "TAKE_LIFE";
                BreakoutBus.GetBus().RegisterEvent(gameEvent);
            }
        }

        public void Activate() {
            if (!isActive) {
                shape.Direction = new Vec2F(0.005f, 0.01f);
                isActive = true;
            }
        }


        public void IncreaseSize() {
            this.shape.Extent = new Vec2F(this.shape.Extent.X*2f, this.shape.Extent.Y*2f);
            this.shape.Position = new Vec2F(
                this.shape.Position.X + 0.5f * (this.shape.Extent.X * (this.shape.Position.X/Math.Abs(this.shape.Position.X))),
                this.shape.Position.Y + 0.5f * (this.shape.Extent.Y * (this.shape.Position.Y/Math.Abs(this.shape.Position.Y))));
            doubleSizeEnabled = true;
        }
        public void DecreaseSize() {
            this.shape.Extent = new Vec2F(0.022f, 0.022f);
            doubleSizeEnabled = false;

        }

        public void IncreaseSpeed() {
            shape.Direction = shape.Direction * 2;
            doubleSpeedEnabled = true;
        }

        public void DecreaseSpeed() {
           shape.Direction = shape.Direction / 2;
           doubleSpeedEnabled = false;
        }

        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.ControlEvent) {
                if (gameEvent.Message == "ENABLE_DOUBLE_SIZE" && !doubleSizeEnabled) {
                    IncreaseSize();
                }
                if (gameEvent.Message == "ENABLE_DOUBLE_SPEED" && !doubleSpeedEnabled) {
                    IncreaseSpeed();
                }
            }

            if (gameEvent.EventType == GameEventType.TimedEvent) {
                if (gameEvent.Message == "DISABLE_DOUBLE_SIZE" && doubleSizeEnabled) {
                    DecreaseSize();
                }
                if (gameEvent.Message == "DISABLE_DOUBLE_SPEED" && doubleSpeedEnabled) {
                    DecreaseSpeed();
                }
            }
        }
    }
}