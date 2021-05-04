using System;
using System.IO;
using DIKUArcade.Events;
using DIKUArcade.State;




namespace Breakout.States {
    public class StateMachine : IGameEventProcessor {
        public IGameState ActiveState { get; private set; }
        public StateMachine() {
            ActiveState = new MainMenu();
        }

        private void SwitchState(GameStateType stateType) {
            switch (stateType) {

                case GameStateType.GameRunning:
                    ActiveState = GameRunning.GetInstance();
                    return;
                case GameStateType.GamePaused:
                    ActiveState = new GamePaused();
                    return;
                case GameStateType.MainMenu:
                    ActiveState = new MainMenu();
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
                }
            }
        }
    }
}