using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.EventBus;
using Galaga;
using System.IO;
using System;


namespace DIKUArcade.State {
    public class GamePaused : IGameState {

        private static GamePaused instance = null;
        private Entity backGroundImage;
        private Text[] menuButtons;
        private int activeMenuButton;
        private int maxMenuButtons;

        public GamePaused() {
            InitializeGameState();
        }

        public static GamePaused GetInstance() {
            return GamePaused.instance ?? (GamePaused.instance = new GamePaused());
        }

        public void GameLoop() {

        }

        public void InitializeGameState() {
            backGroundImage = new Entity(
                new StationaryShape(new Vec2F(0f, 0f), new Vec2F(1f, 1f)),
                new Image(Path.Combine("Assets", "Images", "TitleImage.png"))
            );

            menuButtons = new Text[] {
                new Text("Continue", new Vec2F(0.22f, 0.3f), new Vec2F(0.4f, 0.4f)),
                new Text("Main Menu", new Vec2F(0.22f, 0.20f), new Vec2F(0.4f, 0.4f))
            };

            menuButtons[0].SetColor(new Vec3I(229, 192, 20));
            menuButtons[1].SetColor(new Vec3I(255, 255, 255));

            activeMenuButton = 0;
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
                            // Destroy game session
                            GameRunning.DeleteInstance();
                            GamePaused.instance = null;

                            GalagaBus.GetBus().RegisterEvent(
                                GameEventFactory<object>.CreateGameEventForAllProcessors(
                                    GameEventType.GameStateEvent,
                                    this,
                                    "CHANGE_STATE",
                                    "MAIN_MENU", "")
                            );
                        }
                        break;

                }
            }
        }
    }
}