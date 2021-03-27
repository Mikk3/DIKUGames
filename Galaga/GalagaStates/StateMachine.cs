using System;
using DIKUArcade.EventBus;
using DIKUArcade.State;


namespace Galaga.GalagaStates {
    public class StateMachine : IGameEventProcessor<object> {
        public IGameState ActiveState { get; private set; }
        public StateMachine() {
            GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);

            ActiveState = MainMenu.GetInstance();
        }

        
        private void SwitchState(GameStateType stateType) {
            switch (stateType) { 

                case GameStateType.GameRunning:
                    ActiveState = GameRunning.GetInstance();
                    return;
                case GameStateType.GamePaused:
                    ActiveState = GamePaused.GetInstance();
                    return;
                case GameStateType.MainMenu:
                    ActiveState = MainMenu.GetInstance();
                    return;
            }

            throw new ArgumentException("Invalid state type");

        }

        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
            ActiveState.HandleKeyEvent(gameEvent.Message, gameEvent.Parameter1);

            if (eventType == GameEventType.GameStateEvent) {
                switch (gameEvent.Parameter1) {
                    case "GAME_RUNNING":
                        SwitchState(StateTransformer.TranformStringToState(gameEvent.Parameter1));
                        break;
                    case "GAME_PAUSED":
                        SwitchState(StateTransformer.TranformStringToState(gameEvent.Parameter1));
                        break;
                    case "MAIN_MENU":
                        SwitchState(StateTransformer.TranformStringToState(gameEvent.Parameter1));
                        break;
                }
            }
        }
    }
}