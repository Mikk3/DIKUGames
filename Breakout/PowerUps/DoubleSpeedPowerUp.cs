using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using DIKUArcade.Timers;

namespace Breakout.PowerUps {

    public class DoubleSpeedPowerUp : PowerUp {

        public DoubleSpeedPowerUp(Shape shape, IBaseImage image) : base(shape, image) {

        }

        public override void Activate() {
            // Enable double speed
            var enableEvent = new GameEvent();
            enableEvent.EventType = GameEventType.ControlEvent;
            enableEvent.Message = "ENABLE_DOUBLE_SPEED";
            BreakoutBus.GetBus().RegisterEvent(enableEvent);

            // Disable double speed after x seconds
            var disableEvent = new GameEvent();
            disableEvent.EventType = GameEventType.TimedEvent;
            disableEvent.Message = "DISABLE_DOUBLE_SPEED";
            BreakoutBus.GetBus().RegisterTimedEvent(disableEvent, TimePeriod.NewMilliseconds(15000));

            this.DeleteEntity();
        }

    }
}