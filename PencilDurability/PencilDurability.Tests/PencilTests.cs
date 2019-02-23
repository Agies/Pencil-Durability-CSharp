using System.Reflection.Metadata.Ecma335;
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
        public void
            GivenAPencilAndPaper_WhenInstructedToWriteASingleCharacterOfAString_ThenThePencilWillWriteOnThePaper()
        {
            var surfaceMoq = new Mock<ISurface>();
            _sut.WriteOn("S", surfaceMoq.Object);
            surfaceMoq.Verify(s => s.Write(It.Is<char>(text => text == 'S')), Times.Once);
        }

        [Fact]
        public void GivenAPencilAndPaper_WhenInstructedToWriteAStringOfCharacters_ThenThePencilWillWriteOnThePaper()
        {
            var surfaceMoq = new Mock<ISurface>(MockBehavior.Strict);
            var sequence = new MockSequence();
            surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == 'S')));
            surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == 'h')));
            surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == 'e')));
            surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == ' ')));
            surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == 's')));
            surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == 'e')));
            surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == 'l')));
            surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == 'l')));
            surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == 's')));
            surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == ' ')));
            surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == 's')));
            surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == 'e')));
            surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == 'a')));
            surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == ' ')));
            surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == 's')));
            surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == 'h')));
            surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == 'e')));
            surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == 'l')));
            surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == 'l')));
            surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == 's')));
            _sut.WriteOn("She sells sea shells", surfaceMoq.Object);
        }
    }
}