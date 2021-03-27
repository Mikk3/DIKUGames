using DIKUArcade;
using DIKUArcade.Timers;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.EventBus;
using System.Collections.Generic;
using DIKUArcade.Physics;
using Galaga.Squadron;
using Galaga.GalagaStates;

namespace Galaga {
    public class Game : IGameEventProcessor<object> {
        private GameEventBus<object> eventBus;
        private StateMachine stateMachine;
        private Window window;
        private GameTimer gameTimer;

        public Game() {
            // Game Window
            window = new Window("Galaga", 500, 500);
            window.RegisterEventBus(eventBus);

            // State Machine
            stateMachine = new StateMachine();

            // Events
            eventBus = GalagaBus.GetBus();
            window.RegisterEventBus(eventBus);

            eventBus.InitializeEventBus(new List<GameEventType> { 
                GameEventType.GameStateEvent, 
                GameEventType.InputEvent,
                GameEventType.WindowEvent
            });

            eventBus.Subscribe(GameEventType.InputEvent, stateMachine);
            eventBus.Subscribe(GameEventType.GameStateEvent, stateMachine);
            eventBus.Subscribe(GameEventType.WindowEvent, this);

            // Timer
            gameTimer = new GameTimer(60, 60);

        }

        public void Run() {
            while (window.IsRunning()) {
                gameTimer.MeasureTime();

                while (gameTimer.ShouldUpdate()) {
                    window.PollEvents();
                    GalagaBus.GetBus().ProcessEvents();
                    stateMachine.ActiveState.UpdateGameLogic();
                }

                if (gameTimer.ShouldRender()) {
                    window.Clear();
                    stateMachine.ActiveState.RenderState();
                    window.SwapBuffers();
                }

                if (gameTimer.ShouldReset()) {
                    // this update happens once every second
                    window.Title = $"Galaga | (UPS,FPS): ({gameTimer.CapturedUpdates},{ gameTimer.CapturedFrames})";
                }
            }
        }

        public void ProcessEvent(GameEventType type, GameEvent<object> gameEvent) {

            if (type == GameEventType.WindowEvent) {
                if (gameEvent.Message == "CLOSE_WINDOW") {
                    window.CloseWindow();
                }
            }

        }
        
    }
}