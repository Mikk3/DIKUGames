using NUnit.Framework;
using Breakout.States;
using Breakout;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using System.Collections.Generic;
using System;

namespace BreakoutTests.TestGameStates {

    public class StateMachineTest {

        private StateMachine stateMachine;
        private GameEventBus eventBus;

        public StateMachineTest() {
            eventBus = new GameEventBus();
            eventBus.InitializeEventBus(new List<GameEventType>() {
                GameEventType.GameStateEvent,
                GameEventType.ControlEvent,
                GameEventType.TimedEvent
            });
        }

        [SetUp]
        public void InitializeStateMachine() {
            Window.CreateOpenGLContext();
            stateMachine = new StateMachine();

            eventBus.Subscribe(GameEventType.GameStateEvent, stateMachine);
        }

        [Test]
        public void TestInitializeState() {
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
        }

        [Test]
        public void TestChangeStateToGameRunning() {
            var gameEvent = new GameEvent();
            gameEvent.EventType = GameEventType.GameStateEvent;
            gameEvent.Message = "GAME_RUNNING";
            eventBus.RegisterEvent(gameEvent);
            eventBus.ProcessEvents();

            Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameRunning>());
        }

        [Test]
        public void TestChangeStateToGameOver() {
            var gameEvent = new GameEvent();
            gameEvent.EventType = GameEventType.GameStateEvent;
            gameEvent.Message = "GAME_OVER";
            gameEvent.IntArg1 = 100;
            gameEvent.StringArg1 = Boolean.FalseString;
            eventBus.RegisterEvent(gameEvent);
            eventBus.ProcessEvents();

            Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameOver>());
        }

    }
}