using Moq;
using PencilDurability.Console;
using Xunit;

namespace PencilDurability.Tests
{
    public class PencilTests
    {
        private readonly Pencil _sut;

        public PencilTests()
        {
            _sut = new Pencil();
        }

        [Fact]
        public void GivenAPencilAndPaper_WhenInstructedToWriteASingleCharacterOfAString_ThenThePencilWillWriteOnThePaper()
        {
            var surfaceMoq = new Mock<ISurface>();
            _sut.WriteOn("S", surfaceMoq.Object);
            surfaceMoq.Verify(s => s.Write(It.Is<char>(text => text == 'S')), Times.Once);
        }
    }
}