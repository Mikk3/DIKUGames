using System;

namespace Breakout.States

{
    public class StateTransformer {

        /// <summary>
        /// Given a string returns the corresponding gamestate type
        /// </summary>
        /// <param name="state">State name</param>
        /// <returns>Corresponding gamestate type</returns>
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

        /// <summary>
        /// Given a gamestatetype returns the corresponding string.
        /// </summary>
        /// <param name="state">State name</param>
        /// <returns>Corresponding gamestatetype as a string</returns>
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