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
using DIKUArcade.Timers;
using Breakout.GameInfo;
using Breakout.PowerUps;

namespace Breakout.States {
    public class GameRunning : IGameState, IGameEventProcessor {

        private static GameRunning instance;
        private Entity backgroundImage;
        private LevelData leveldata;
        private Player player;
        private Ball.Ball ball;
        private GameInfo.Score score;
        private Lives lives;
        private PowerUpController powerUpController;

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

            // Score
            score = new GameInfo.Score(new Vec2F(0.0f, 0.5f), new Vec2F(0.5f, 0.5f));
            BreakoutBus.GetBus().Subscribe(GameEventType.ControlEvent, score);
            BreakoutBus.GetBus().Subscribe(GameEventType.TimedEvent, score);


            //Player
            player = new Player(
                new DynamicShape(new Vec2F(0.50f, 0.035f), new Vec2F(0.224f, 0.044f)),
                new Image(Path.Combine("Assets", "Images", "player.png"))
            );
            BreakoutBus.GetBus().Subscribe(GameEventType.ControlEvent, player);
            BreakoutBus.GetBus().Subscribe(GameEventType.TimedEvent, player);

            // Ball
            ball = new Ball.Ball(
                new DynamicShape(new Vec2F(0.324f, 0.044f), new Vec2F(0.022f, 0.022f)),
                new Image(Path.Combine("Assets", "Images", "ball.png"))
            );
            BreakoutBus.GetBus().Subscribe(GameEventType.ControlEvent, ball);
            BreakoutBus.GetBus().Subscribe(GameEventType.TimedEvent, ball);

            // Lives
            lives = new Lives(new Vec2F(0.8f, 0.5f), new Vec2F(0.5f, 0.5f));
            BreakoutBus.GetBus().Subscribe(GameEventType.ControlEvent, lives);

            // Gameover
            BreakoutBus.GetBus().Subscribe(GameEventType.ControlEvent, this);

            // Powerup Controller
            powerUpController = new PowerUpController();
            BreakoutBus.GetBus().Subscribe(GameEventType.ControlEvent, powerUpController);


        }

        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            player.HandleMovement(action, key);

            if (action == KeyboardAction.KeyPress && key == KeyboardKey.Escape) {
                var closeEvent = new GameEvent();
                closeEvent.EventType = GameEventType.GameStateEvent;
                closeEvent.Message = "GAME_PAUSED";
                BreakoutBus.GetBus().RegisterEvent(closeEvent);
            }

            // Launch ball when 'Space' is pressed
            if (action == KeyboardAction.KeyPress && key == KeyboardKey.Space) {
                ball.Activate();
            }
        }

        public void RenderState() {
            backgroundImage.RenderEntity();
            leveldata.Blocks.RenderEntities();
            ball.RenderEntity();
            player.RenderEntity();
            score.RenderScore();
            lives.RenderLives();
            powerUpController.PowerUps.RenderEntities();


            if (leveldata.TimeLimit != null) {
                leveldata.TimeLimit.RenderTimeLimit();
            }
        }

        public void ResetState() {
            instance = null;
        }

        public void UpdateState() {
            Collisions.CheckBallCollisionsWithBlock(ball, leveldata.Blocks);
            ball.Move();

            Collisions.CheckBallCollisionWithPlayer(ball, player);
            player.Move();

            powerUpController.PowerUps.Iterate(x => {
                Collisions.CheckPowerUpCollisionWithPlayer(x, player);
            });

            powerUpController.MovePowerUps();

            leveldata.CheckLevelOver();

            if (leveldata.TimeLimit != null) {
                leveldata.TimeLimit.Tick();
            }
        }

        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.ControlEvent) {


                if (gameEvent.Message == "LOST_GAME") {
                    var newEvent = new GameEvent();
                    newEvent.EventType = GameEventType.GameStateEvent;
                    newEvent.Message = "GAME_OVER";
                    newEvent.IntArg1 = score.Value;
                    newEvent.StringArg1 = Boolean.FalseString;
                    BreakoutBus.GetBus().RegisterEvent(newEvent);
                }

                if (gameEvent.Message == "WON_GAME") {
                    var newEvent = new GameEvent();
                    newEvent.EventType = GameEventType.GameStateEvent;
                    newEvent.Message = "GAME_OVER";
                    newEvent.IntArg1 = score.Value;
                    newEvent.StringArg1 = Boolean.TrueString;
                    BreakoutBus.GetBus().RegisterEvent(newEvent);
                }

            }
        }
    }
}