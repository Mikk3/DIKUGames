using Breakout.States;
using NUnit.Framework;

namespace BreakoutTests.TestGameStates
{
    public class StateTransformerTest
    {

        [Test]
        public void TestTransformStateToStringMainMenu() {
            var result = StateTransformer.TransformStateToString(
                GameStateType.MainMenu);

            Assert.That(result, Is.EqualTo("MAIN_MENU"));
        }

        [Test]
        public void TestTransformStateToStringGamePaused() {
            var result = StateTransformer.TransformStateToString(
                GameStateType.GamePaused);

            Assert.That(result, Is.EqualTo("GAME_PAUSED"));
        }

        [Test]
        public void TestTransformStateToStringGameRunning() {
            var result = StateTransformer.TransformStateToString(
                GameStateType.GameRunning);

            Assert.That(result, Is.EqualTo("GAME_RUNNING"));
        }

        [Test]
        public void TestTransformStringToStateMainMenu() {
            var result = StateTransformer.TransformStringToState("MAIN_MENU");

            Assert.That(result, Is.EqualTo(GameStateType.MainMenu));
        }

        [Test]
        public void TestTransformStringToStateGamePaused() {
            var result = StateTransformer.TransformStringToState("GAME_PAUSED");

            Assert.That(result, Is.EqualTo(GameStateType.GamePaused));
        }

        [Test]
        public void TestTransformStringToStateGameRunning() {
            var result = StateTransformer.TransformStringToState("GAME_RUNNING");

            Assert.That(result, Is.EqualTo(GameStateType.GameRunning));
        }

    }
}