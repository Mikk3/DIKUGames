using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System;

namespace Breakout.GameInfo {
    public class Lives : IGameEventProcessor {
        public int Value { get; private set; }
        private Text display;
        public Lives(Vec2F position, Vec2F extent) {
            Value = 3;
            display = new Text(Value.ToString(), position, extent);
            display.SetColor(new Vec3F(0.01f, 0.55f, 0.63f));
        }

        /// <summary>
        /// Render the lives text onto the game canvas
        /// </summary>
        public void RenderLives() {
            display.RenderText();
        }

        private void TakeLife() {
            if (Value > 1) {
                Value--;
                display.SetText(Value.ToString());
            } else {
                var gameEvent = new GameEvent();
                gameEvent.EventType = GameEventType.ControlEvent;
                gameEvent.Message = "LOST_GAME";
                BreakoutBus.GetBus().RegisterEvent(gameEvent);

            }
        }

        private void GiveLife() {
            Value++;
            display.SetText(Value.ToString());
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