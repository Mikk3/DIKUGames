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
using Breakout.Paddle;
using Breakout.Blocks;
using Breakout.States;

namespace Breakout {

    class Game : DIKUGame, IGameEventProcessor {

        private GameEventBus eventBus;

        private StateMachine stateMachine;

        public Game(WindowArgs windowArgs) : base(windowArgs) {
            // KeyHandler
            window.SetKeyEventHandler(KeyHandler);

            // State Machine
            stateMachine = new StateMachine();

            // Events
            eventBus = BreakoutBus.GetBus();
            eventBus.InitializeEventBus(new List<GameEventType> {
                GameEventType.WindowEvent,
                GameEventType.GameStateEvent,
                GameEventType.ControlEvent,
                GameEventType.TimedEvent
            });
            eventBus.Subscribe(GameEventType.GameStateEvent, stateMachine);
            eventBus.Subscribe(GameEventType.WindowEvent, this);
        }

        private void KeyHandler(KeyboardAction action, KeyboardKey key) {
            stateMachine.ActiveState.HandleKeyEvent(action, key);

        }

        public override void Render() {
            stateMachine.ActiveState.RenderState();
        }

        public override void Update() {
            eventBus.ProcessEvents();
            stateMachine.ActiveState.UpdateState();
        }

        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.WindowEvent && gameEvent.Message == "CLOSE_WINDOW") {
                window.CloseWindow();
            }
        }

    }

}