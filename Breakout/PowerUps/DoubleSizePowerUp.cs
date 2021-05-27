using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using DIKUArcade.Timers;

namespace Breakout.PowerUps {

    public class DoubleSizePowerUp : PowerUp {

        public DoubleSizePowerUp(Shape shape, IBaseImage image) : base(shape, image) {

        }

        public override void Activate() {
            // Enable double size
            var enableEvent = new GameEvent();
            enableEvent.EventType = GameEventType.ControlEvent;
            enableEvent.Message = "ENABLE_DOUBLE_SIZE";
            BreakoutBus.GetBus().RegisterEvent(enableEvent);

            // Disable double size after x seconds
            var disableEvent = new GameEvent();
            disableEvent.EventType = GameEventType.TimedEvent;
            disableEvent.Message = "DISABLE_DOUBLE_SIZE";
            BreakoutBus.GetBus().RegisterTimedEvent(disableEvent, TimePeriod.NewMilliseconds(8000));

            this.DeleteEntity();
        }

    }
}