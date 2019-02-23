using System.Reflection.Metadata.Ecma335;
using Moq;
using PencilDurability.Console;
using Xunit;

namespace PencilDurability.Tests
{
    public class PencilTests
    {
        private readonly Pencil _sut;
        private Mock<ISurface> _surfaceMoq;

        public PencilTests()
        {
            _surfaceMoq = new Mock<ISurface>(MockBehavior.Strict);
            _sut = new Pencil();
        }

        [Fact]
        public void GivenAPencilAndPaper_WhenInstructedToWriteASingleCharacterOfAString_ThenThePencilWillWriteOnThePaper()
        {
            _surfaceMoq.Setup(s => s.Write(It.Is<char>(text => text == 'S')));
            _sut.WriteOn("S", _surfaceMoq.Object);
            _surfaceMoq.Verify(s => s.Write(It.Is<char>(text => text == 'S')), Times.Once);
        }

        [Fact]
        public void GivenAPencilAndPaper_WhenInstructedToWriteAStringOfCharacters_ThenThePencilWillWriteOnThePaper()
        {
            var sequence = new MockSequence();
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == 'S')));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == 'h')));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == 'e')));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == ' ')));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == 's')));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == 'e')));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == 'l')));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == 'l')));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == 's')));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == ' ')));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == 's')));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == 'e')));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == 'a')));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == ' ')));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == 's')));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == 'h')));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == 'e')));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == 'l')));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == 'l')));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == 's')));
            _sut.WriteOn("She sells sea shells", _surfaceMoq.Object);
        }

        [Fact]
        public void GivenAPencilAndPaper_WhenInstructedToAnEmptyString_ThenThePencilWillNotWrite()
        {
            _sut.WriteOn("", _surfaceMoq.Object);
            _surfaceMoq.Verify(s => s.Write(It.IsAny<char>()), Times.Never);
        }
    }
}