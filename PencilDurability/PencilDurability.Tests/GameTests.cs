using System;
using System.IO;
using System.Linq.Expressions;
using Moq;
using PencilDurability.Console;
using Xunit;

namespace PencilDurability.Tests
{
    public class GameTests
    {
        private Game _sut;
        private Mock<TextWriter> _mockOutput;
        private Mock<TextReader> _mockInput;
        private readonly Mock<ISurface> _mockSurface;
        private readonly Mock<IDevice> _mockDevice;

        private const string IntroText = Game.Intro;

        public GameTests()
        {
            _mockOutput = new Mock<TextWriter>();
            _mockInput = new Mock<TextReader>();
            _mockSurface = new Mock<ISurface>();
            _mockDevice = new Mock<IDevice>();
            _sut = new Game(_mockOutput.Object, _mockInput.Object, _mockSurface.Object, _mockDevice.Object);
        }

        [Fact]
        public void GivenAGame_WhenStarted_ThenShouldPrintIntro()
        {
            InSequenceAndQuit(_mockInput, t => t.ReadLine(), "1");
            _sut.Start();
            _mockOutput.Verify(t => t.WriteLine(IntroText), Times.AtLeast(1));
        }

        [Fact]
        public void GivenAGame_WhenStarted_ThenShouldPromptForAnswer()
        {
            InSequenceAndQuit(_mockInput, t => t.ReadLine(), "1");
            _sut.Start();
            _mockInput.Verify(t => t.ReadLine());
        }

        [Fact]
        public void GivenAGameIsStarted_WhenAnAnswerIsOne_ThenShouldReadSurface()
        {
            InSequenceAndQuit(_mockInput, t => t.ReadLine(), "1");
            _mockSurface.Setup(t => t.Show()).Returns("Hello");
            _sut.Start();
            _mockSurface.Verify(t => t.Show());
            _mockOutput.Verify(t => t.WriteLine(string.Format(Game.Reading, "Hello")));
        }

        [Fact]
        public void GivenAGameIsStarted_WhenAnAnswerIsTwo_ThenShouldExaminePencil()
        {
            InSequenceAndQuit(_mockInput, t => t.ReadLine(), "2");
            _mockDevice.Setup(t => t.Examine()).Returns("Hello");
            _sut.Start();
            _mockDevice.Verify(t => t.Examine());
            _mockOutput.Verify(t => t.WriteLine(string.Format(Game.Examine, "Hello")));
        }

        [Fact]
        public void GivenAGameIsStarted_WhenAnAnswerIsNotOneOrTwo_ThenItShouldRepeatTheQuestion()
        {
            InSequenceAndQuit(_mockInput, t => t.ReadLine(), "3");
            _sut.Start();
            _mockOutput.Verify(t => t.WriteLine(IntroText), Times.Exactly(2));
        }

        [Fact]
        public void GivenAGameIsStarted_WhenAnAnswerIsOne_ThenTheUserShouldBePromptedForTheNextAction()
        {
            InSequenceAndQuit(_mockInput, t => t.ReadLine(), "1");
            _sut.Start();
            _mockInput.Verify(t => t.ReadLine(), Times.Exactly(2));
        }

        [Fact]
        public void GivenAGameIsStarted_WhenAnAnswerIsOne_ThenTheUserShouldBeAskedForTheNextAction()
        {
            InSequenceAndQuit(_mockInput, t => t.ReadLine(), "1");
            var sequence = new MockSequence();
            _mockInput.InSequence(sequence).Setup(t => t.ReadLine()).Returns("1");
            _mockInput.InSequence(sequence).Setup(t => t.ReadLine()).Returns("Q");
            _sut.Start();
            _mockOutput.Verify(t => t.WriteLine(IntroText), Times.Exactly(2));
        }

        [Fact]
        public void
            GivenAGameIsStarted_WhenAnAnswerIsOne_ThenTheUserShouldBeAskedForTheNextActionAndActionsShouldRepeat()
        {
            InSequenceAndQuit(_mockInput, t => t.ReadLine(), "1", "3");
            _sut.Start();
            _mockOutput.Verify(t => t.WriteLine(IntroText), Times.Exactly(3));
        }

        [Fact]
        public void
            GivenAGameIsStarted_WhenAnAnswerIsTwo_ThenTheUserShouldBePromptedForAnActionWithThePencil()
        {
            InSequenceAndQuit(_mockInput, t => t.ReadLine(), "2");
            _sut.Start();
            _mockInput.Verify(t => t.ReadLine(), Times.Exactly(2));
        }

        [Fact]
        public void GivenAGameIsStarted_WhenAnAnswerIsOne_ThenOrderShouldByMaintained()
        {
            _mockInput = new Mock<TextReader>(MockBehavior.Strict);
            _mockOutput = new Mock<TextWriter>(MockBehavior.Strict);
            var sequence = new MockSequence();
            _mockOutput.InSequence(sequence).Setup(t => t.WriteLine(Game.Intro));
            _mockInput.InSequence(sequence).Setup(t => t.ReadLine()).Returns("1");
            _mockOutput.InSequence(sequence).Setup(t => t.WriteLine(string.Format(Game.Reading, "")));
            _mockOutput.InSequence(sequence).Setup(t => t.WriteLine(Game.Intro));
            _mockInput.InSequence(sequence).Setup(t => t.ReadLine()).Returns("Q");
            _mockOutput.InSequence(sequence).Setup(t => t.WriteLine(Game.Exit));
            _sut = new Game(_mockOutput.Object, _mockInput.Object, _mockSurface.Object, _mockDevice.Object);
            _sut.Start();
        }

        [Fact]
        public void GivenAGameIsStarted_WhenAnAnswerIsQ_ThenTheGameWillQuit()
        {
            _mockInput.Setup(t => t.ReadLine()).Returns("Q");
            _sut.Start();
            _mockOutput.Verify(t => t.WriteLine(IntroText), Times.Exactly(1));
            _mockOutput.Verify(t => t.WriteLine(Game.Exit), Times.Exactly(1));
        }

        [Fact]
        public void GivenAGameIsStarted_WhenAnAnswerIsq_ThenTheGameWillQuit()
        {
            _mockInput.Setup(t => t.ReadLine()).Returns("q");
            _sut.Start();
            _mockOutput.Verify(t => t.WriteLine(IntroText), Times.Exactly(1));
            _mockOutput.Verify(t => t.WriteLine(Game.Exit), Times.Exactly(1));
        }

        [Fact]
        public void GivenAGameIsStarted_WhenAnAnswerIs2Then2_ThenTheGameWillReturnToIntro()
        {
            InSequenceAndQuit(_mockInput, t => t.ReadLine(), "2", "2");
            _sut.Start();
            _mockOutput.Verify(t => t.WriteLine(IntroText), Times.Exactly(2));
        }

        [Fact]
        public void GivenAGameIsStarted_WhenAnAnswerIs2ThenQ_ThenTheGameWillQuit()
        {
            InSequenceAndQuit(_mockInput, t => t.ReadLine(), "2");
            _sut.Start();
            _mockOutput.Verify(t => t.WriteLine(Game.Exit), Times.Exactly(1));
        }
        
        [Fact]
        public void GivenAGameIsStarted_WhenAnAnswerIs2ThenWrite_ThenTheGameWillWrite()
        {
            InSequenceAndQuit(_mockInput, t => t.ReadLine(), "2", "write hello");
            _sut.Start();
            _mockDevice.Verify(d => d.WriteOn("hello", _mockSurface.Object));
        }

        [Fact]
        public void GivenAGameIsStarted_WhenAnAnswerIs2ThenNotAnAvailableChoice_ThenTheGameWillReprompt()
        {
            InSequenceAndQuit(_mockInput, t => t.ReadLine(), "2", "G");
            _sut.Start();
            _mockOutput.Verify(t => t.WriteLine(string.Format(Game.Examine, "")), Times.Exactly(2));
        }

        private static void InSequenceAndQuit<T>(Mock<T> mock, Expression<Func<T, string>> setup, params string[] args)
            where T : class
        {
            var sequence = new MockSequence();
            foreach (var arg in args)
            {
                mock.InSequence(sequence).Setup(setup).Returns(arg);
            }
            mock.InSequence(sequence).Setup(setup).Returns("Q");
        }
    }
}