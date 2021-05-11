using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;

namespace Breakout.Blocks {

    public class HardenedBlock : Block {

        public HardenedBlock(DynamicShape shape, IBaseImage image) : base(shape, image) {
            base.Health = 4;
            base.Value = 10;
        }

        public override void OnHit() {
            base.Health--;
            if (base.Health <= 0) {
                OnDestroy();
            }
        }

        public override void OnDestroy() {
            // Change player score using event?
            var gameEvent = new GameEvent();
            gameEvent.EventType = GameEventType.ControlEvent;
            gameEvent.Message = "ADD_SCORE";
            gameEvent.IntArg1 = Value;
            BreakoutBus.GetBus().RegisterEvent(gameEvent);

            this.DeleteEntity();
        }
    }

}