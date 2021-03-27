using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.EventBus;
using Galaga;
using System.IO;
using System;

namespace DIKUArcade.State {
    public class MainMenu : IGameState {

        private static MainMenu instance = null;
        private Entity backGroundImage;
        private Text[] menuButtons;
        private int activeMenuButton;
        private int maxMenuButtons;

        public MainMenu() {
            InitializeGameState();
        }

        public static MainMenu GetInstance() {
            return MainMenu.instance ?? (MainMenu.instance = new MainMenu());
        }

        public void GameLoop() {

        }
        
        public void InitializeGameState() {
            backGroundImage = new Entity(
                new StationaryShape(new Vec2F(0f, 0f), new Vec2F(1f, 1f)),
                new Image(Path.Combine("Assets", "Images", "TitleImage.png"))
            );

            menuButtons = new Text[] {
                new Text("New Game", new Vec2F(0.22f, 0.3f), new Vec2F(0.4f, 0.4f)),
                new Text("Quit", new Vec2F(0.22f, 0.20f), new Vec2F(0.4f, 0.4f))
            };

            menuButtons[0].SetColor(new Vec3I(229, 192, 20));
            menuButtons[1].SetColor(new Vec3I(255, 255, 255));

            activeMenuButton = 0;

            // 229, 192, 20
        }

        public void UpdateGameLogic() {

        }

        public void RenderState() {
            backGroundImage.RenderEntity();

            menuButtons[0].RenderText();
            menuButtons[1].RenderText();

        }

        public void HandleKeyEvent(string keyValue, string keyAction) {
            if (keyAction == "KEY_PRESS") {
                switch (keyValue) {
                    case "KEY_UP":
                        activeMenuButton = 0;
                        menuButtons[1].SetColor(new Vec3I(255, 255, 255));
                        menuButtons[0].SetColor(new Vec3I(229, 192, 20));
                        break;

                    case "KEY_DOWN":
                        activeMenuButton = 1;
                        menuButtons[0].SetColor(new Vec3I(255, 255, 255));
                        menuButtons[1].SetColor(new Vec3I(229, 192, 20));
                        break;
                        
                    case "KEY_ENTER":
                        if (activeMenuButton == 0) {
                            GalagaBus.GetBus().RegisterEvent(
                                GameEventFactory<object>.CreateGameEventForAllProcessors(
                                    GameEventType.GameStateEvent,
                                    this,
                                    "CHANGE_STATE",
                                    "GAME_RUNNING", "")
                            );

                        } else {
                            GalagaBus.GetBus().RegisterEvent(
                                GameEventFactory<object>.CreateGameEventForAllProcessors(
                                    GameEventType.WindowEvent,
                                    this,
                                    "CLOSE_WINDOW",
                                    "", "")
                            );
                        }
                        break;

                }
            }
        }
    }
}