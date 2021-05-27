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

namespace Breakout.Paddle {
    public class Player : Entity, IGameEventProcessor {

        private DynamicShape shape;
        private float moveLeft = 0.0f;
        private float moveRight = 0.0f;
        private bool DoubleWidthEnabled = false;
        private readonly float MOVEMENT_SPEED = 0.025f;

        public Player(DynamicShape shape, IBaseImage image) : base(shape, image) {
            this.shape = Shape.AsDynamicShape();
        }

        public void Move() {
            // Right boundary
            if (shape.Position.X > (0.975f - shape.Extent.X) && shape.Direction.X > 0.0f) {
                return;
            }
            // Left boundary
            if (shape.Position.X < 0.024f && shape.Direction.X < 0.0f) {

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

        private void UpdateDirection() {
            shape.ChangeDirection(new Vec2F((moveLeft + moveRight), 0.0f));
        }

        private void KeyPress(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Left:
                    SetMoveLeft(true);
                    break;
                case KeyboardKey.Right:
                    SetMoveRight(true);
                    break;
                default:
                    break;
            }
        }

        private void KeyRelease(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Left:
                    SetMoveLeft(false);
                    break;
                case KeyboardKey.Right:
                    SetMoveRight(false);
                    break;
                default:
                    break;
            }
        }

        public void HandleMovement(KeyboardAction action, KeyboardKey key) {
            switch (action) {
                case KeyboardAction.KeyPress:
                    KeyPress(key);
                    break;
                case KeyboardAction.KeyRelease:
                    KeyRelease(key);
                    break;
                default:
                    break;
            }
        }


        public void IncreaseWidth() {
            this.shape.Extent = new Vec2F(this.shape.Extent.X*2f, this.shape.Extent.Y);
            this.shape.Position = new Vec2F((this.shape.Position.X) - (this.shape.Extent.X * 0.5f), this.shape.Position.Y);
            DoubleWidthEnabled = true;

        }
        public void DecreaseWidth() {
            this.shape.Extent = new Vec2F(this.shape.Extent.X*0.5f, this.shape.Extent.Y);
            this.shape.Position = new Vec2F((this.shape.Position.X) + (this.shape.Extent.X * 0.25f), this.shape.Position.Y);
            DoubleWidthEnabled = false;

        }

        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.ControlEvent) {
                if (gameEvent.Message == "ENABLE_DOUBLE_WIDTH" && !DoubleWidthEnabled) {
                    IncreaseWidth();
                }
            }

            if (gameEvent.EventType == GameEventType.TimedEvent) {
                if (gameEvent.Message == "DISABLE_DOUBLE_WIDTH" && DoubleWidthEnabled) {
                    DecreaseWidth();
                }
            }
        }
    }
}