using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using DIKUArcade.Timers;

namespace Breakout.PowerUps {

    public class ExtraLifePowerUp : PowerUp {

        public ExtraLifePowerUp(Shape shape, IBaseImage image) : base(shape, image) {

        }

        public override void Activate() {
            // Give extra life
            var giveLifeEvent = new GameEvent();
            giveLifeEvent.EventType = GameEventType.ControlEvent;
            giveLifeEvent.Message = "GIVE_LIFE";
            BreakoutBus.GetBus().RegisterEvent(giveLifeEvent);

            this.DeleteEntity();
        }

    }
}