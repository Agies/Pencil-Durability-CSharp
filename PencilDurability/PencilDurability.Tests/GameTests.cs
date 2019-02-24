using System.IO;
using Moq;
using PencilDurability.Console;
using Xunit;

namespace PencilDurability.Tests
{
    public class GameTests
    {
        private readonly Game _sut;
        private readonly Mock<TextWriter> _mockOutput;
        private readonly Mock<TextReader> _mockInput;
        private readonly Mock<ISurface> _mockSurface;
        private readonly Mock<IDevice> _mockDevice;

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
            _sut.Start();
            _mockOutput.Verify(t => t.Write("You see a simple sheet of paper sitting on a desk. A pencil sits across the paper. There appear to be words written on the paper.\nWhat would you like to do?\n1) Read the paper\n2) Look at pencil"), Times.Once);
        }

        [Fact]
        public void GivenAGame_WhenStarted_ThenShouldPromptForAnswer()
        {
            _sut.Start();
            _mockInput.Verify(t => t.ReadLine());
        }

        [Fact]
        public void GivenAGameIsStarted_WhenAnAnswerIsOne_ThenShouldReadSurface()
        {
            _mockInput.Setup(t => t.ReadLine()).Returns("1");
            _mockSurface.Setup(t => t.Show()).Returns("Hello");
            _sut.Start();
            _mockSurface.Verify(t => t.Show());
            _mockOutput.Verify(t => t.Write("You look at the simple sheet of paper and read the text written.\n\nHello"));
        }
        
        [Fact]
        public void GivenAGameIsStarted_WhenAnAnswerIsTwo_ThenShouldExaminePencil()
        {
            _mockInput.Setup(t => t.ReadLine()).Returns("2");
            _mockDevice.Setup(t => t.Examine()).Returns("Hello");
            _sut.Start();
            _mockDevice.Verify(t => t.Examine());
            _mockOutput.Verify(t => t.Write("You look at what you now understand to be a magic pencil, its stats are revealed in your mind.\n\nHello"));
        }
    }
}