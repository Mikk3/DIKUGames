using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.Blocks {

    public class PowerUpBlock : Block {

        public PowerUpBlock(DynamicShape shape, IBaseImage image) : base(shape, image) {
            base.Health = 1;
            base.Value = 5;

        }

        public override void OnHit() {
            base.Health--;
            if (base.Health <= 0) {
                OnDestroy();
            }
        }

        public override void OnDestroy() {
            // Create Powerup
            var createEvent = new GameEvent();
            createEvent.EventType = GameEventType.ControlEvent;
            createEvent.Message = "CREATE_POWERUP";
            createEvent.ObjectArg1 = Shape.Position;
            BreakoutBus.GetBus().RegisterEvent(createEvent);

            // Change player score

            var scoreEvent = new GameEvent();
            scoreEvent.EventType = GameEventType.ControlEvent;
            scoreEvent.Message = "ADD_SCORE";
            scoreEvent.IntArg1 = Value;
            BreakoutBus.GetBus().RegisterEvent(scoreEvent);

            this.DeleteEntity();
        }
    }

}