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

namespace Breakout {

    class Game : DIKUGame, IGameEventProcessor {

        private GameEventBus eventBus;

        private Entity backgroundImage;

        private EntityContainer<Block> blocks;

        private LevelData leveldata;

        private Player player;


        public Game(WindowArgs windowArgs) : base(windowArgs) {
            // KeyHandler
            window.SetKeyEventHandler(KeyHandler);

            // Events
            eventBus = new GameEventBus();
            eventBus.InitializeEventBus(new List<GameEventType> {
                GameEventType.WindowEvent,
                GameEventType.GameStateEvent
            });
            eventBus.Subscribe(GameEventType.WindowEvent, this);

            // Background
            backgroundImage = new Entity(
                new StationaryShape(new Vec2F(0f, 0f), new Vec2F(1f, 1f)),
                new Image(Path.Combine("Assets", "Images", "SpaceBackground.png"))
            );

            // Level creation
            leveldata = new LevelData("level3");

            // Setup player
            player = new Player(
                new DynamicShape(new Vec2F(0.50f, 0.035f), new Vec2F(0.224f, 0.044f)),
                new Image(Path.Combine("Assets", "Images", "player.png"))
            );

        }

        private void KeyHandler(KeyboardAction action, KeyboardKey key) {
            player.HandleMovement(action, key);

            if (action == KeyboardAction.KeyPress && key == KeyboardKey.Escape) {
                var closeEvent = new GameEvent();
                closeEvent.EventType = GameEventType.WindowEvent;
                closeEvent.Message = "CLOSE_WINDOW";
                eventBus.RegisterEvent(closeEvent);
            }

            if (action == KeyboardAction.KeyPress && key == KeyboardKey.D) {
                leveldata.Blocks.Iterate(x => {
                    x.OnHit();
                });
            }


        }

        public override void Render() {
            backgroundImage.RenderEntity();
            leveldata.Blocks.RenderEntities();
            player.RenderEntity();

        }

        public override void Update() {
            eventBus.ProcessEvents();
            player.Move();
        }

        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.WindowEvent && gameEvent.Message == "CLOSE_WINDOW") {
                window.CloseWindow();
            }
        }
    }

}