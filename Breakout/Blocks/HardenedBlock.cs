using System;
using System.Reflection;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using System.IO;
namespace Breakout.Blocks {

    public class HardenedBlock : Block {

        public IBaseImage alterImage;

        public HardenedBlock(DynamicShape shape, IBaseImage image, IBaseImage damagedImage) : base(shape, image) {
            base.Health = 2;
            base.Value = 10;
            this.alterImage = damagedImage;

        }

        public override void OnHit() {
            base.Health--;
            base.Image = alterImage;

            if (base.Health <= 0) {
                OnDestroy();
            }
        }

        public override void OnDestroy() {
            // Change player score using event
            var gameEvent = new GameEvent();
            gameEvent.EventType = GameEventType.ControlEvent;
            gameEvent.Message = "ADD_SCORE";
            gameEvent.IntArg1 = Value;
            BreakoutBus.GetBus().RegisterEvent(gameEvent);

            this.DeleteEntity();
        }
    }

}