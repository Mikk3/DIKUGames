using NUnit.Framework;
using DIKUArcade;
using DIKUArcade.State;
using DIKUArcade.EventBus;
using Galaga;
using Galaga.GalagaStates;
using System.Collections.Generic;

namespace GalagaTest {
    [TestFixture]
    public class StateMachineTesting {
        
        private StateMachine stateMachine;
        private GameEventBus<object> eventBus;
        private Window window;

        [SetUp]
        public void InitiateStateMachine() {
            DIKUArcade.Window.CreateOpenGLContext();
            window = new Window("Galaga", 500, 500);


            eventBus = GalagaBus.GetBus();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.GameStateEvent, GameEventType.InputEvent });
            window.RegisterEventBus(eventBus);

            stateMachine = new StateMachine();
            eventBus.Subscribe(GameEventType.GameStateEvent, stateMachine);
            
            
            // Here you should:
            // (1) Initialize a GalagaBus with proper GameEventTypes
            // (2) Instantiate the StateMachine
            // (3) Subscribe the GalagaBus to proper GameEventTypes
            // and GameEventProcessors

            }
        [Test]
        public void TestInitialState() {
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
        }

        [Test]
        public void TestEventGamePaused() {
            GalagaBus.GetBus().RegisterEvent(
                GameEventFactory<object>.CreateGameEventForAllProcessors(
                    GameEventType.GameStateEvent,
                    this,
                    "CHANGE_STATE",
                    "GAME_PAUSED", ""));

            GalagaBus.GetBus().ProcessEventsSequentially();
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<GamePaused>());
        }
    }
}