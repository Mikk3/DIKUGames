using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.GameInfo {
    public class Lives : IGameEventProcessor {
        private int lives;
        private Text display;
        public Lives(Vec2F position, Vec2F extent) {
            lives = 3;
            display = new Text(lives.ToString(), position, extent);
            display.SetColor(new Vec3F(0.01f, 0.55f, 0.63f));
        }

        public void RenderLives() {
            display.RenderText();
        }

        private void TakeLife() {
            if (lives > 1) {
                lives--;
                display.SetText(lives.ToString());
            } else {
                var gameEvent = new GameEvent();
                gameEvent.EventType = GameEventType.ControlEvent;
                gameEvent.Message = "LOST_GAME";
                BreakoutBus.GetBus().RegisterEvent(gameEvent);

                System.Console.WriteLine("INFO: Player is out of lives");
            }
        }

        private void GiveLife() {
            lives++;
        }

        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.ControlEvent) {
                if (gameEvent.Message == "TAKE_LIFE") {
                    TakeLife();
                }
                if (gameEvent.Message == "GIVE_LIFE") {
                    GiveLife();
                }
            }
        }
    }
}