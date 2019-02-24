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

        public GameTests()
        {
            _mockOutput = new Mock<TextWriter>();
            _mockInput = new Mock<TextReader>();
            _sut = new Game(_mockOutput.Object, _mockInput.Object);
        }

        [Fact]
        public void GivenAGame_WhenStarted_ThenShouldPrintIntro()
        {
            _sut.Start();
            _mockOutput.Verify(t => t.Write("You see a simple sheet of paper sitting on a desk. A pencil sits across the paper. There appear to be words written on the paper.\nWhat would you like to do?\n1) Read the paper\n2) Look at pencil"), Times.Once);
        }
    }
}