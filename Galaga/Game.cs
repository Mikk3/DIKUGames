using DIKUArcade;
using DIKUArcade.Timers;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.EventBus;
using System.Collections.Generic;
using DIKUArcade.Physics;
using Galaga.Squadron;
using Galaga.MovementStrategy;
using Microsoft.VisualBasic;
using System;

namespace Galaga {
    public class Game : IGameEventProcessor<object>{
        private GameEventBus<object> eventBus;
        private Player player;
        private Window window;
        private GameTimer gameTimer;
        private List<ISquadron> squadrons = new List<ISquadron>();
        private EntityContainer<PlayerShot> playerShots;
        private AnimationContainer enemyExplosions;
        
        private const int EXPLOSION_LENGTH_MS = 500;
        private Score score;

        private StaggeredSquadron staggered = new StaggeredSquadron();
        private TwoRowsSquadron twoRows = new TwoRowsSquadron();
        private SnakeSquadron snake = new SnakeSquadron();
        private bool gameOver = false;

        private List<Image> enemyStridesBlue;
        private List<Image> enemyStridesRed;
        private List<Image> explosionStrides;
        private IBaseImage playerShotImage;
        
        public Game() {
            window = new Window("Galaga", 500, 500);
            gameTimer = new GameTimer(60, 60);
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "Player.png")));

            // Events
            eventBus = new GameEventBus<object>();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent });
            window.RegisterEventBus(eventBus);
            eventBus.Subscribe(GameEventType.InputEvent, this);
            eventBus.Subscribe(GameEventType.InputEvent, player);

            // Strides
            enemyStridesBlue = ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "BlueMonster.png"));
            enemyStridesRed = ImageStride.CreateStrides(2, Path.Combine("Assets", "Images", "RedMonster.png"));
            explosionStrides = ImageStride.CreateStrides(8, Path.Combine("Assets", "Images", "Explosion.png"));
            playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));

            // Enemies
            staggered.CreateEnemies(enemyStridesBlue, enemyStridesRed);
            twoRows.CreateEnemies(enemyStridesBlue, enemyStridesRed);
            snake.CreateEnemies(enemyStridesBlue, enemyStridesRed);

            squadrons.Add(staggered);
            squadrons.Add(twoRows);
            squadrons.Add(snake);

            // Player shooting
            playerShots = new EntityContainer<PlayerShot>();

            // Exploding enemies
            enemyExplosions = new AnimationContainer(500);

            // Score
            score = new Score(new Vec2F(0.0f, 0.5f), new Vec2F(0.5f, 0.5f));
            
        }

        public void AddExplosion(Vec2F position, Vec2F extent) {
            enemyExplosions.AddAnimation(
                new StationaryShape(position, extent),
                EXPLOSION_LENGTH_MS,
                new ImageStride(EXPLOSION_LENGTH_MS / 8, explosionStrides)
            );
        }

        private void IterateShots() {
            playerShots.Iterate(shot => {
                shot.Shape.Move();
                if (shot.Shape.Position.Y > 1.0f) {
                    shot.DeleteEntity();
                } else {
                    foreach (var squadron in squadrons)
                    {
                        squadron.Enemies.Iterate(enemy => {
                        var data = CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), enemy.Shape);
                        if (data.Collision) {
                            shot.DeleteEntity();
                            enemy.Damage();
                            enemy.Enrage();
                            
                            if (enemy.isDead()) {
                                score.AddPoint();
                                enemy.DeleteEntity();
                                AddExplosion(enemy.Shape.Position, enemy.Shape.Extent);
                            }
                        }
                    });
                    }
                }
            });
        }

        private void IterateEnemies() {
            foreach (var squadron in squadrons) {
                squadron.Enemies.Iterate(enemy => {
                    if (enemy.Shape.Position.Y < 0.0f) {
                        //Game over
                        gameOver = true;
                        score.MoveToCenter();

                    }
                });
            }

            var liveEnemies = 0;
            foreach (var squadron in squadrons) {
                liveEnemies += squadron.Enemies.CountEntities();
            }
            if (liveEnemies < 1) {
                Enemy.IncreaseDifficulty();

                foreach (var squadron in squadrons)
                {
                    squadron.CreateEnemies(enemyStridesBlue, enemyStridesRed);
                }
            }
        }
            

        public void Run() {
            while (window.IsRunning()) {
                gameTimer.MeasureTime();
                while (gameTimer.ShouldUpdate()) {
                    window.PollEvents();
                    eventBus.ProcessEvents();
                    
                    if (!gameOver)
                    {
                        player.Move();
                        IterateShots();
                        IterateEnemies();
                        // Enemies movement
                        staggered.Move();
                        twoRows.Move();
                        snake.Move();
                    }
                    
                    
                }
                if (gameTimer.ShouldRender()) {
                    window.Clear();
                    
                    if(!gameOver) {
                        playerShots.RenderEntities();
                        player.Render();
                        // Enemies rendering
                        snake.Enemies.RenderEntities();
                        staggered.Enemies.RenderEntities();
                        twoRows.Enemies.RenderEntities();
                        enemyExplosions.RenderAnimations();
                    }
                    

                    score.RenderScore();
                    
                    window.SwapBuffers();
                }
                if (gameTimer.ShouldReset()) {
                    // this update happens once every second
                    window.Title = $"Galaga | (UPS,FPS): ({gameTimer.CapturedUpdates},{ gameTimer.CapturedFrames})";
                }
            }
        }

        public void KeyRelease(string key) {
            switch (key) {
                case "KEY_ESCAPE":
                    window.CloseWindow();
                    break;
                case "KEY_SPACE":
                    playerShots.AddEntity(new PlayerShot(player.GetTipPosition(), playerShotImage));
                    break;
                default:
                    break;
            }
        }

        public void ProcessEvent(GameEventType type, GameEvent<object> gameEvent) {
            switch (gameEvent.Parameter1) {
                case "KEY_RELEASE":
                    KeyRelease(gameEvent.Message);
                    break;
                default:
                    break;
            }
        }
    }
}
