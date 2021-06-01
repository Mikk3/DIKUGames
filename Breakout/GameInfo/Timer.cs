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
    public class Timer {
        public int TimeLimit;
        private Text display;
        private long lastTickTime;


        public Timer(Vec2F position, Vec2F extent, int timeLimit) {
            this.TimeLimit = timeLimit;
            display = new Text(TimeLimit.ToString(), position, extent);
            display.SetColor(new Vec3F(0.01f, 0.55f, 0.63f));
            lastTickTime = StaticTimer.GetElapsedMilliseconds();
        }

        /// <summary>
        /// Render the time limit text onto the game canvas
        /// </summary>
        public void RenderTimeLimit() {
            display.RenderText();
        }

        private void CountDown() {
            if (TimeLimit > 0) {
                TimeLimit -= 1;
                display.SetText(TimeLimit.ToString());
            } else {
                var gameEvent = new GameEvent();
                gameEvent.EventType = GameEventType.ControlEvent;
                gameEvent.Message = "LOST_GAME";
                BreakoutBus.GetBus().RegisterEvent(gameEvent);

                System.Console.WriteLine("INFO: Time limit reached");
            }
        }

        /// <summary>
        /// Makes the timer countdown if one second has passed since last.
        /// </summary>
        /// <remarks>
        /// Can be called multiple times but will only countdown when one second has passed.
        /// </remarks>
        public void Tick() {
            if ((lastTickTime + 1000) < StaticTimer.GetElapsedMilliseconds()) {
                CountDown();
                lastTickTime = StaticTimer.GetElapsedMilliseconds();
            }
        }
    }
}