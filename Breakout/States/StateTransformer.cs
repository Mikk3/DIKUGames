using System;

namespace Breakout.States

{
    public class StateTransformer {
        public static GameStateType TransformStringToState (string state) {
           switch (state)
            {
                case "GAME_RUNNING":
                    return GameStateType.GameRunning;
                case "GAME_PAUSED":
                    return GameStateType.GamePaused;
                case "MAIN_MENU":
                    return GameStateType.MainMenu;
                case "GAME_OVER":
                    return GameStateType.GameOver;
            }
            throw new ArgumentException("String did not match any GameStateTypes");
        }
        public static string TransformStateToString (GameStateType state) {
           switch (state)
            {
                case GameStateType.GameRunning:
                    return "GAME_RUNNING";
                case GameStateType.GamePaused:
                    return "GAME_PAUSED";
                case GameStateType.MainMenu:
                    return "MAIN_MENU";
                case GameStateType.GameOver:
                    return "GAME_OVER";
            }
            throw new ArgumentException("GameStateType did not match any string");
        }

    }

}