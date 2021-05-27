using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using DIKUArcade.Timers;

namespace Breakout.PowerUps {

    public class DoubleWidthPowerUp : PowerUp {

        public DoubleWidthPowerUp(Shape shape, IBaseImage image) : base(shape, image) {

        }

        public override void Activate() {
            // Enable double width
            var enableEvent = new GameEvent();
            enableEvent.EventType = GameEventType.ControlEvent;
            enableEvent.Message = "ENABLE_DOUBLE_WIDTH";
            BreakoutBus.GetBus().RegisterEvent(enableEvent);

            // Disable double width after x seconds
            var disableEvent = new GameEvent();
            disableEvent.EventType = GameEventType.TimedEvent;
            disableEvent.Message = "DISABLE_DOUBLE_WIDTH";
            BreakoutBus.GetBus().RegisterTimedEvent(disableEvent, TimePeriod.NewMilliseconds(10000));
            this.DeleteEntity();
        }

    }
}