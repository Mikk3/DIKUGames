using DIKUArcade.Events;

namespace Breakout
{
    public class BreakoutBus {

        private static GameEventBus eventBus;

        /// <summary>
        /// Creates or return the already created GameEventBus
        /// </summary>
        /// <returns>An instance of GameEventBus</returns>
        public static GameEventBus GetBus() {
            return BreakoutBus.eventBus ?? (BreakoutBus.eventBus = new GameEventBus());
        }

        private BreakoutBus() {}
    }
}