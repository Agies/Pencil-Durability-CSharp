using System.IO;
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
        private const string LookAtPaper = "You look at the simple sheet of paper and read the text written.\n\n";

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
            _mockInput.Setup(t => t.ReadLine()).Returns("1");
            _mockInput.Setup(t => t.ReadLine()).Returns("Q");
            _sut.Start();
            _mockOutput.Verify(t => t.WriteLine(IntroText), Times.AtLeast(1));
        }

        [Fact]
        public void GivenAGame_WhenStarted_ThenShouldPromptForAnswer()
        {
            _mockInput.Setup(t => t.ReadLine()).Returns("1");
            _mockInput.Setup(t => t.ReadLine()).Returns("Q");
            _sut.Start();
            _mockInput.Verify(t => t.ReadLine());
        }

        [Fact]
        public void GivenAGameIsStarted_WhenAnAnswerIsOne_ThenShouldReadSurface()
        {
            MockSequence sequence = new MockSequence();
            _mockInput.InSequence(sequence).Setup(t => t.ReadLine()).Returns("1");
            _mockSurface.Setup(t => t.Show()).Returns("Hello");
            _mockInput.InSequence(sequence).Setup(t => t.ReadLine()).Returns("Q");
            _sut.Start();
            _mockSurface.Verify(t => t.Show());
            _mockOutput.Verify(t => t.WriteLine(string.Format(Game.Reading, "Hello")));
        }

        [Fact]
        public void GivenAGameIsStarted_WhenAnAnswerIsTwo_ThenShouldExaminePencil()
        {
            _mockInput.Setup(t => t.ReadLine()).Returns("2");
            _mockDevice.Setup(t => t.Examine()).Returns("Hello");
            _sut.Start();
            _mockDevice.Verify(t => t.Examine());
            _mockOutput.Verify(t => t.WriteLine(string.Format(Game.Examine, "Hello")));
        }

        [Fact]
        public void GivenAGameIsStarted_WhenAnAnswerIsNotOneOrTwo_ThenItShouldRepeatTheQuestion()
        {
            MockSequence sequence = new MockSequence();
            _mockInput.InSequence(sequence).Setup(t => t.ReadLine()).Returns("3");
            _mockInput.InSequence(sequence).Setup(t => t.ReadLine()).Returns("2");
            _sut.Start();
            _mockOutput.Verify(t => t.WriteLine(IntroText), Times.Exactly(2));
        }

        [Fact]
        public void GivenAGameIsStarted_WhenAnAnswerIsOne_ThenTheUserShouldBePromptedForTheNextAction()
        {
            var sequence = new MockSequence();
            _mockInput.InSequence(sequence).Setup(t => t.ReadLine()).Returns("1");
            _mockInput.InSequence(sequence).Setup(t => t.ReadLine()).Returns("Q");
            _sut.Start();
            _mockInput.Verify(t => t.ReadLine(), Times.Exactly(2));
        }

        [Fact]
        public void GivenAGameIsStarted_WhenAnAnswerIsOne_ThenTheUserShouldBeAskedForTheNextAction()
        {
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
            var sequence = new MockSequence();
            _mockInput.InSequence(sequence).Setup(t => t.ReadLine()).Returns("1");
            _mockInput.InSequence(sequence).Setup(t => t.ReadLine()).Returns("3");
            _mockInput.InSequence(sequence).Setup(t => t.ReadLine()).Returns("Q");
            _sut.Start();
            _mockOutput.Verify(t => t.WriteLine(IntroText), Times.Exactly(3));
        }

        [Fact]
        public void
            GivenAGameIsStarted_WhenAnAnswerIsTwo_ThenTheUserShouldBePromptedForAnActionWithThePencil()
        {
            var sequence = new MockSequence();
            _mockInput.InSequence(sequence).Setup(t => t.ReadLine()).Returns("2");
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
        public void GivenAGameIsStarted_WhenAnAnswerIs2Then4_ThenTheGameWillReturnToIntro()
        {
            var sequence = new MockSequence();
            _mockInput.InSequence(sequence).Setup(t => t.ReadLine()).Returns("2");
            _mockInput.InSequence(sequence).Setup(t => t.ReadLine()).Returns("4");
            _mockInput.InSequence(sequence).Setup(t => t.ReadLine()).Returns("Q");
            _sut.Start();
            _mockOutput.Verify(t => t.WriteLine(IntroText), Times.Exactly(2));
        }
        
        [Fact]
        public void GivenAGameIsStarted_WhenAnAnswerIs2ThenQ_ThenTheGameWillQuit()
        {
            var sequence = new MockSequence();
            _mockInput.InSequence(sequence).Setup(t => t.ReadLine()).Returns("2");
            _mockInput.InSequence(sequence).Setup(t => t.ReadLine()).Returns("Q");
            _sut.Start();
            _mockOutput.Verify(t => t.WriteLine(Game.Exit), Times.Exactly(1));
        }
        
    }
}