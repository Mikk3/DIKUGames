using DIKUArcade.Math;
using DIKUArcade.Graphics;
using System;

namespace Galaga {
    public class Score {
        private int score;
        private Text display;
        public Score(Vec2F position, Vec2F extent) {
            score = 0;
            display = new Text(score.ToString(), position, extent);
            display.SetColor(new Vec3F(0.01f,0.55f, 0.63f));
        }

        public void AddPoint() {
            score++;
            display.SetText(score.ToString());
        }

        public void RenderScore() {
            display.RenderText();
        }

        public void MoveToCenter() {
            display.GetShape().Position = new Vec2F(0.4f, 0.1f);
            display.SetText("Score: " + score);
        }
    }
}