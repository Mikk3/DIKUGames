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
        public int score { get; private set; }
        private Text display;
        private bool doubleScore = false;


        public Score(Vec2F position, Vec2F extent) {
            score = 0;
            display = new Text(score.ToString(), position, extent);
            display.SetColor(new Vec3F(0.01f, 0.55f, 0.63f));
        }
        private void AddScore(int num) {
            if (doubleScore) {
                num *= 2;
            }

            score += num;
            display.SetText(score.ToString());
        }

        public void RenderScore() {
            display.RenderText();
        }

        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.ControlEvent) {
                if (gameEvent.Message == "ADD_SCORE") {
                    AddScore(gameEvent.IntArg1);
                }
                if (gameEvent.Message == "ENABLE_DOUBLE_SCORE") {
                    System.Console.WriteLine("double score enabled");
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