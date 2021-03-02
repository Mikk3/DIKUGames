using DIKUArcade;
using DIKUArcade.Timers;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.EventBus;
using System.Collections.Generic;
using DIKUArcade.Physics;
namespace Galaga
{
    public class Game : IGameEventProcessor<object>
    {
        private GameEventBus<object> eventBus;
        private Player player;
        private Window window;
        private GameTimer gameTimer;
        private EntityContainer<Enemy> enemies;
        private EntityContainer<PlayerShot> playerShots;
        private IBaseImage playerShotImage;
        public Game() {

            window = new Window("Galaga", 500, 500);
            gameTimer = new GameTimer(30, 30);
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "Player.png")));

            // Events
            eventBus = new GameEventBus<object>();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent });
            window.RegisterEventBus(eventBus);
            eventBus.Subscribe(GameEventType.InputEvent, this);

            // Enemies
            var images = ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "BlueMonster.png"));
            const int numEnemies = 8;
            enemies = new EntityContainer<Enemy>(numEnemies);
            for (int i = 0; i < numEnemies; i++) {
                enemies.AddEntity(new Enemy(new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, 0.9f), new Vec2F(0.1f, 0.1f)), new ImageStride(80, images)));
            }

            // Player shooting
            playerShots = new EntityContainer<PlayerShot>();
            playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));

        }

        private void IterateShots() {
            playerShots.Iterate(shot => {

                shot.Shape.Move(new Vec2F(0.0f, 0.01f));

                if (shot.Shape.Position.Y > 1.0f) {
                    shot.DeleteEntity();
                }
                else {
                    enemies.Iterate(enemy => {
                        var data = CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), enemy.Shape);

                        if (data.Collision) {
                            shot.DeleteEntity();
                            enemy.DeleteEntity();

                        }

                    });
                }
            });
        }


        public void Run()
        {
            while (window.IsRunning())
            {
                gameTimer.MeasureTime();
                while (gameTimer.ShouldUpdate())
                {
                    window.PollEvents();
                    eventBus.ProcessEvents();
                    player.Move();
                    IterateShots();
                }

                if (gameTimer.ShouldRender())
                {
                    window.Clear();
                    player.Render();
                    enemies.RenderEntities();
                    playerShots.RenderEntities();
                    window.SwapBuffers();
                }



                if (gameTimer.ShouldReset())
                {
                    // this update happens once every second
                    window.Title = $"Galaga | (UPS,FPS): ({playerShots.CountEntities()},{ enemies.CountEntities()})";
                }
            }
        }

        public void KeyPress(string key)
        {
            // TODO: switch on key string and set the player's move direction
            switch (key)
            {
                case "KEY_LEFT":
                    player.SetMoveLeft(true);
                    break;
                case "KEY_RIGHT":
                    player.SetMoveRight(true);
                    break;
                default:
                    break;
            }
        }

        public void KeyRelease(string key)
        {
            switch (key)
            {
                case "KEY_LEFT":
                    player.SetMoveLeft(false);
                    break;
                case "KEY_RIGHT":
                    player.SetMoveRight(false);
                    break;
                case "KEY_ESCAPE":
                    window.CloseWindow();
                    break;
                case "KEY_SPACE":
                    //shooting logic
                    playerShots.AddEntity(new PlayerShot(player.GetTipPosition(), playerShotImage));
                    break;
                default:
                    break;
            }
            // TODO: Close window if escape is pressed
        }

        public void ProcessEvent(GameEventType type, GameEvent<object> gameEvent) {

            switch (gameEvent.Parameter1)
            {
                case "KEY_PRESS":
                    KeyPress(gameEvent.Message);
                    break;
                case "KEY_RELEASE":
                    KeyRelease(gameEvent.Message);
                    break;
                default:
                    break;
            }
        }
    }
}
