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

        /// <summary>
        /// Moves the ball and call helper function check if its touching boundary
        /// </summary>
        public void Move() {
            if (isActive) {
                checkBoundary();
                Shape.Move();
            } else {
                shape.Position = new Vec2F(0.5f,0.085f);
            }
        }

        /// <summary>
        /// Changes the ball direction
        /// </summary>
        public void ChangeDirection(Vec2F dir) {
            shape.Direction = dir;
        }

        /// <summary>
        /// Change direction based on the ball and players position
        /// </summary>
        /// <param name="player">The player object that was hit by the ball</param>
        public void CollideWithPlayer(Player player) {
            if (shape.Position.X < player.Shape.Position.X + (0.5 * player.Shape.Extent.X)) {
                ChangeDirection(new Vec2F(Math.Abs(shape.Direction.X) * (-1), shape.Direction.Y * -1));
            } else {
                ChangeDirection(new Vec2F(Math.Abs(shape.Direction.X), shape.Direction.Y * -1));
            }
        }

        /// <summary>
        /// Change direction based on the ball and blocks position
        /// </summary>
        /// <param name="block">The block object that was hit by the ball</param>
        /// <param name="data">Collision data</param>
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

        /// <summary>
        /// Activates the ball movement
        /// </summary>
        public void Activate() {
            if (!isActive) {
                shape.Direction = new Vec2F(0.005f, 0.01f);
                isActive = true;
            }
        }


        private void IncreaseSize() {
            this.shape.Extent = new Vec2F(this.shape.Extent.X*2f, this.shape.Extent.Y*2f);
            this.shape.Position = new Vec2F(
                this.shape.Position.X + 0.5f * (this.shape.Extent.X * (this.shape.Position.X/Math.Abs(this.shape.Position.X))),
                this.shape.Position.Y + 0.5f * (this.shape.Extent.Y * (this.shape.Position.Y/Math.Abs(this.shape.Position.Y))));
            doubleSizeEnabled = true;
        }
        private void DecreaseSize() {
            this.shape.Extent = new Vec2F(0.022f, 0.022f);
            doubleSizeEnabled = false;

        }

        private void IncreaseSpeed() {
            shape.Direction = shape.Direction * 2;
            doubleSpeedEnabled = true;
        }

        private void DecreaseSpeed() {
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