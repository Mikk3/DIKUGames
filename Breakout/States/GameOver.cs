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
        private Text button;
        private Text display;
        private bool won;



        public GameOver(int score, string info) {
            backgroundImage = new Entity(
                new StationaryShape(new Vec2F(0f, 0f), new Vec2F(1f, 1f)),
                new Image(Path.Combine("Assets", "Images", "SpaceBackground.png"))
            );
            display = new Text("Score: " + score.ToString(), new Vec2F(0.32f, 0.1f), new Vec2F(0.5f, 0.5f));
            display.SetColor(new Vec3F(0.01f,0.55f, 0.63f));
            won = bool.Parse(info);

        }

        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            if (action == KeyboardAction.KeyPress && key == KeyboardKey.Enter) {
               // Return to MainMenu
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


        }

        public void ResetState() {

        }

        public void UpdateState() {

        }
    }
}