using Galaga;
using NUnit.Framework;
using Galaga.GalagaStates;

namespace GalagaTest {
    public class TestStateTransformer {

        [Test]
        public void TestTransformRunningToState() {
            var result = StateTransformer.TranformStringToState("GAME_RUNNING");
            Assert.AreEqual(GameStateType.GameRunning, result);
        }

        [Test]
        public void TestTransformPausedToString() {
            var result = StateTransformer.TranformStringToState("GAME_PAUSED");
            Assert.AreEqual(GameStateType.GamePaused, result);
        }

        [Test]
        public void TestTransformMenuToState() {
            var result = StateTransformer.TranformStringToState("MAIN_MENU");
           Assert.AreEqual(GameStateType.MainMenu, result);
        }

        [Test]
        public void TestTransformRunningToString() {
            var result = StateTransformer.TranformStateToString(GameStateType.GameRunning);
            Assert.AreEqual("GAME_RUNNING", result);
        }

        [Test]
        public void TestTransformPausedToState() {
            var result = StateTransformer.TranformStateToString(GameStateType.GamePaused);
            Assert.AreEqual("GAME_PAUSED", result);
        }

        [Test]
        public void TestTransformMainMenuToString() {
            var result = StateTransformer.TranformStateToString(GameStateType.MainMenu);
            Assert.AreEqual("MAIN_MENU", result);
        }

        
    }
}