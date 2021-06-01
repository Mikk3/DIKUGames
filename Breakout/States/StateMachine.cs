using System;
using System.IO;
using DIKUArcade.Events;
using DIKUArcade.State;
using DIKUArcade.Timers;

namespace Breakout.States {
    public class StateMachine : IGameEventProcessor {
        public IGameState ActiveState { get; private set; }
        public StateMachine() {
            ActiveState = new MainMenu();
        }

        /// <summary>
        /// Given a statetype switches the active state
        /// </summary>
        /// <param name="stateType">Gamestate type</param>
        /// <param name="intArg">Optional int argument</param>
        /// <param name="stringArg">Optional string argument</param>
        private void SwitchState(GameStateType stateType, int intArg = 0, string stringArg = "") {
            switch (stateType) {
                case GameStateType.GameRunning:
                    ActiveState = GameRunning.GetInstance();
                    StaticTimer.ResumeTimer();
                    return;
                case GameStateType.GamePaused:
                    ActiveState = new GamePaused();
                    StaticTimer.PauseTimer();
                    return;
                case GameStateType.MainMenu:
                    ActiveState = new MainMenu();
                    StaticTimer.RestartTimer();
                    return;
                case GameStateType.GameOver:
                    ActiveState = new GameOver(intArg, stringArg);
                    StaticTimer.RestartTimer();
                    return;
            }

            throw new ArgumentException("Invalid state type");

        }
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.GameStateEvent) {
                switch (gameEvent.Message) {
                    case "GAME_RUNNING":
                        SwitchState(StateTransformer.TransformStringToState(gameEvent.Message));
                        break;
                    case "GAME_PAUSED":
                        SwitchState(StateTransformer.TransformStringToState(gameEvent.Message));
                        break;
                    case "MAIN_MENU":
                        SwitchState(StateTransformer.TransformStringToState(gameEvent.Message));
                        break;
                    case "GAME_OVER":
                        SwitchState(StateTransformer.TransformStringToState(gameEvent.Message), gameEvent.IntArg1, gameEvent.StringArg1);
                        break;
                }
            }
        }
    }
}