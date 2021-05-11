using System;
using System.Collections.Generic;
using System.IO;
using Breakout.Blocks;
using Breakout.Levels;
using Breakout.Paddle;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;
using Breakout.Ball;
using Breakout;

namespace Breakout.States {
    public class GameRunning : IGameState {

        private static GameRunning instance;

        private Entity backgroundImage;
        private LevelData leveldata;
        private Player player;
        private Ball.Ball ball;

        public static GameRunning GetInstance() {
            return instance ?? (instance = new GameRunning());
        }

        public static GameRunning DeleteInstance() {
            return instance = null;
        }

        public GameRunning() {
            // Background
            backgroundImage = new Entity(
                new StationaryShape(new Vec2F(0f, 0f), new Vec2F(1f, 1f)),
                new Image(Path.Combine("Assets", "Images", "SpaceBackground.png"))
            );

            // Level creation
            leveldata = new LevelData();


            //Player
            player = new Player(
                new DynamicShape(new Vec2F(0.50f, 0.035f), new Vec2F(0.224f, 0.044f)),
                new Image(Path.Combine("Assets", "Images", "player.png"))
            );

            // Ball
            /*ball = new Ball.Ball(
                new DynamicShape(new Vec2F(0.324f, 0.044f), new Vec2F(0.045f, 0.045f)),
                new Image(Path.Combine("Assets", "Images", "ball.png"))
            );*/

            ball = new Ball.Ball(
                new DynamicShape(new Vec2F(0.324f, 0.044f), new Vec2F(0.070f, 0.070f)),
                new Image(Path.Combine("Assets", "Images", "orange-block.png"))
            );

        }


        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            player.HandleMovement(action, key);

            if (action == KeyboardAction.KeyPress && key == KeyboardKey.Escape) {
                var closeEvent = new GameEvent();
                closeEvent.EventType = GameEventType.GameStateEvent;
                closeEvent.Message = "GAME_PAUSED";
                BreakoutBus.GetBus().RegisterEvent(closeEvent);
            }

            // Temporary implementation to damage block on 'D' key press
            if (action == KeyboardAction.KeyPress && key == KeyboardKey.D) {
                leveldata.Blocks.Iterate(x => {
                    x.OnHit();
                });
            }

            // Temporary implementation to change level 'L' key press
            if (action == KeyboardAction.KeyPress && key == KeyboardKey.L) {
                leveldata.NextLevel();
            }

        }

        public void RenderState() {
            backgroundImage.RenderEntity();
            leveldata.Blocks.RenderEntities();
            ball.RenderEntity();
            player.RenderEntity();
        }

        public void ResetState() {
            instance = null;
        }

        public void UpdateState() {
            player.Move();
            ball.Move();
            Collisions.CheckBallCollisionWithPlayer(ball, player);
            Collisions.CheckBallCollisionsWithBlock(ball, leveldata.Blocks);
        }

    }
}