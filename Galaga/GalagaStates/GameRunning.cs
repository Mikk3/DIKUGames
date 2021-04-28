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
using Galaga.GalagaStates;
using Galaga;

namespace DIKUArcade.State {
    public class GameRunning : IGameState {

        private static GameRunning instance = null;
        private Player player;
        private List<ISquadron> squadrons = new List<ISquadron>();
        private EntityContainer<PlayerShot> playerShots;
        private AnimationContainer enemyExplosions;
        private const int EXPLOSION_LENGTH_MS = 500;
        private Score score;
        private StaggeredSquadron staggered = new StaggeredSquadron();
        private TwoRowsSquadron twoRows = new TwoRowsSquadron();
        private SnakeSquadron snake = new SnakeSquadron();
        private List<Image> enemyStridesBlue;
        private List<Image> enemyStridesRed;
        private List<Image> explosionStrides;
        private IBaseImage playerShotImage;
        private bool gameOver = false;
        private long lastShotTime;



        public GameRunning() {
            InitializeGameState();
        }
        public static GameRunning GetInstance() {
            return GameRunning.instance ?? (GameRunning.instance = new GameRunning());
        }

        public static void DeleteInstance() {
            GameRunning.instance = null;
        }

        public void GameLoop() {
            if (!gameOver) {
                player.Move();
                IterateShots();
                IterateEnemies();

                // Squadrons movement
                staggered.Move();
                twoRows.Move();
                snake.Move();
            }
        }
        public void InitializeGameState() {
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "Player.png")));

            // Events
            var eventBus = GalagaBus.GetBus();
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
            enemyExplosions = new AnimationContainer(100);

            // Score
            score = new Score(new Vec2F(0.0f, 0.5f), new Vec2F(0.5f, 0.5f));

        }

        public void UpdateGameLogic() {
            GameLoop();
        }

        public void RenderState() {
            if (!gameOver) {
                playerShots.RenderEntities();
                player.Render();


                // Enemies rendering
                snake.Enemies.RenderEntities();
                staggered.Enemies.RenderEntities();
                twoRows.Enemies.RenderEntities();
                enemyExplosions.RenderAnimations();
            }

            score.RenderScore();

        }

        public void HandleKeyEvent(string keyValue, string keyAction) {
            // Player Movement
            if (keyAction == "KEY_PRESS" || keyAction == "KEY_RELEASE") {
                GalagaBus.GetBus().RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.InputEvent,
                        this,
                        keyAction,
                        keyValue, "")
                );
            }

            if (keyAction == "KEY_PRESS") {
                switch (keyValue) {

                    case "KEY_ESCAPE":
                        GalagaBus.GetBus().RegisterEvent(
                                GameEventFactory<object>.CreateGameEventForAllProcessors(
                                    GameEventType.GameStateEvent,
                                    this,
                                    "CHANGE_STATE",
                                    "GAME_PAUSED", "")
                            );
                        break;

                    case "KEY_SPACE":
                        if (MillisecondsSinceLastShot() > 100) {
                            lastShotTime = StaticTimer.GetElapsedMilliseconds();
                            playerShots.AddEntity(new PlayerShot(player.GetTipPosition(), playerShotImage));
                        }
                        break;
                }
            }
        }


        private void AddExplosion(Vec2F position, Vec2F extent) {
            enemyExplosions.AddAnimation(
                new StationaryShape(position, extent),
                EXPLOSION_LENGTH_MS,
                new ImageStride(EXPLOSION_LENGTH_MS / 8, explosionStrides)
            );
        }

        private void IterateShots() {
            // Using iterator since objects might have to be modified
            playerShots.Iterate(shot => {
                shot.Shape.Move();
                if (shot.Shape.Position.Y > 1.0f) {
                    shot.DeleteEntity();
                } else {
                    foreach (var squadron in squadrons) {
                        squadron.Enemies.Iterate(enemy => {
                            var data = CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), enemy.Shape);
                            if (data.Collision) {
                                shot.DeleteEntity();
                                enemy.Damage();
                                // changing image and speed
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
                foreach (var squadron in squadrons) {
                    squadron.CreateEnemies(enemyStridesBlue, enemyStridesRed);
                }
            }
        }

        private long MillisecondsSinceLastShot() {
            return StaticTimer.GetElapsedMilliseconds() - lastShotTime;
        }
    }
}