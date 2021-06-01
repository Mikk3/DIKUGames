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
using Breakout.Blocks;
using DIKUArcade.Timers;

namespace Breakout.GameInfo {
    public class Score : IGameEventProcessor {
        public int Value { get; private set; }
        private Text display;
        private bool doubleScore = false;


        public Score(Vec2F position, Vec2F extent) {
            Value = 0;
            display = new Text(Value.ToString(), position, extent);
            display.SetColor(new Vec3F(0.01f, 0.55f, 0.63f));
        }

        private void AddScore(int num) {
            if (doubleScore) {
                num *= 2;
            }

            Value += num;
            display.SetText(Value.ToString());
        }

        /// <summary>
        /// Render the score text onto the game canvas
        /// </summary>
        public void RenderScore() {
            display.RenderText();
        }

        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.ControlEvent) {
                if (gameEvent.Message == "ADD_SCORE") {
                    AddScore(gameEvent.IntArg1);
                }
                if (gameEvent.Message == "ENABLE_DOUBLE_SCORE" && doubleScore == false) {
                    doubleScore = true;
                }
            }

            if (gameEvent.EventType == GameEventType.TimedEvent) {
                if (gameEvent.Message == "DISABLE_DOUBLE_SCORE") {
                    doubleScore = false;
                }
            }
        }
    }
}