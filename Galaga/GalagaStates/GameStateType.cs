using System;

namespace Galaga.GalagaStates {
    public enum GameStateType
    {
        GameRunning,
        GamePaused,
        MainMenu
    }

    public class StateTransformer {

        public static GameStateType TranformStringToState(string state) {
            switch (state)
            {
                case "GAME_RUNNING":
                    return GameStateType.GameRunning;
                case "GAME_PAUSED":
                    return GameStateType.GamePaused;
                case "MAIN_MENU":
                    return GameStateType.MainMenu;
            }
            throw new ArgumentException("String did not match any GameStateTypes");
        }

        public static string TranformStateToString(GameStateType state) {
            switch (state) 
            {
                case GameStateType.GameRunning:
                    return "GAME_RUNNING";
                case GameStateType.GamePaused:
                    return "GAME_PAUSED";
                case GameStateType.MainMenu:
                    return "MAIN_MENU";
            }
            throw new ArgumentException("Argument is invalid");
        }
    }
}