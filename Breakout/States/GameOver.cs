using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;
using OpenTK.Graphics.ES11;

namespace Breakout.States
{
    public class GameOver : IGameState {

        private Entity backgroundImage;
        private Text display;
        private bool won;
        private Text[] menuButtons;
        private int activeMenuButton;



        public GameOver(int score, string info) {
            backgroundImage = new Entity(
                new StationaryShape(new Vec2F(0f, 0f), new Vec2F(1f, 1f)),
                new Image(Path.Combine("Assets", "Images", "SpaceBackground.png"))
            );
            display = new Text("Score: " + score.ToString(), new Vec2F(0.32f, 0.1f), new Vec2F(0.5f, 0.5f));
            display.SetColor(new Vec3F(0.01f,0.55f, 0.63f));
            won = bool.Parse(info);

            // Menu buttons
            menuButtons = new Text[] {
                new Text("Main Menu", new Vec2F(0.32f, 0.0f), new Vec2F(0.4f, 0.4f)),
                new Text("Quit", new Vec2F(0.32f, -0.10f), new Vec2F(0.4f, 0.4f))
            };

            menuButtons[0].SetColor(new Vec3I(229, 192, 20));
            menuButtons[1].SetColor(new Vec3I(255, 255, 255));

            activeMenuButton = 0;

        }

        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            if (action == KeyboardAction.KeyPress) {
                switch (key) {
                    case KeyboardKey.Up:
                        activeMenuButton = 0;
                        menuButtons[1].SetColor(new Vec3I(255, 255, 255));
                        menuButtons[0].SetColor(new Vec3I(229, 192, 20));
                        break;

                    case KeyboardKey.Down:
                        activeMenuButton = 1;
                        menuButtons[0].SetColor(new Vec3I(255, 255, 255));
                        menuButtons[1].SetColor(new Vec3I(229, 192, 20));
                        break;

                    case KeyboardKey.Enter:
                        var gameEvent = new GameEvent();
                        if (activeMenuButton == 0) {
                            GameRunning.DeleteInstance();
                            gameEvent.EventType = GameEventType.GameStateEvent;
                            gameEvent.Message = "MAIN_MENU";
                        } else {
                            gameEvent.EventType = GameEventType.WindowEvent;
                            gameEvent.Message = "CLOSE_WINDOW";
                        }
                        BreakoutBus.GetBus().RegisterEvent(gameEvent);
                        break;

                }
            }
        }

        public void RenderState() {
            backgroundImage.RenderEntity();
            if (won) {
                var winText = new Text("You Won!", new Vec2F(0.32f, 0.2f), new Vec2F(0.5f, 0.5f));
                winText.SetColor(new Vec3F(0.01f,0.55f, 0.63f));
                winText.RenderText();
                display.RenderText();
            } else {
                var lostText = new Text("Game over!", new Vec2F(0.32f, 0.2f), new Vec2F(0.5f, 0.5f));
                lostText.SetColor(new Vec3F(0.01f,0.55f, 0.63f));
                lostText.RenderText();
                display.RenderText();
            }

            foreach (var button in menuButtons)
            {
                button.RenderText();
            }


        }

        public void ResetState() {

        }

        public void UpdateState() {

        }
    }
}