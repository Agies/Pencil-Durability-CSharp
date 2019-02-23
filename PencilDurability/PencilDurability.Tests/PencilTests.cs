using Moq;
using PencilDurability.Console;
using Xunit;

namespace PencilDurability.Tests
{
    public interface IMockPaper: IErasable, IViewable
    {
        
    }
    
    public class PencilTests
    {
        private Pencil _sut;
        private readonly Mock<ISurface> _surfaceMoq;
        private readonly Mock<ISurface> _forgivingMoq;
        private readonly Mock<IMockPaper> _erasableMock;

        public PencilTests()
        {
            _erasableMock = new Mock<IMockPaper>(MockBehavior.Strict);
            _surfaceMoq = new Mock<ISurface>(MockBehavior.Strict);
            _forgivingMoq = new Mock<ISurface>();
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
        public void GivenAPencilAndPaper_WhenInstructedToWriteAnEmptyString_ThenThePencilWillNotWrite()
        {
            _sut.WriteOn("", _surfaceMoq.Object);
            _surfaceMoq.Verify(s => s.Write(It.IsAny<char>()), Times.Never);
        }
        
        [Fact]
        public void GivenAPencilAndPaper_WhenInstructedToWriteANull_ThenThePencilWillNotWrite()
        {
            _sut.WriteOn(null, _surfaceMoq.Object);
            _surfaceMoq.Verify(s => s.Write(It.IsAny<char>()), Times.Never);
        }

        [Fact]
        public void GivenAPencilAndANullSurface_WhenInstructedToWrite_ThenComplain()
        {
            Assert.Equal("I have nothing on which to write!", Assert.Throws<NothingToWriteOnException>(() => _sut.WriteOn("Something", null)).Message);
        }

        [Fact]
        public void GivenAPencil_WhenExamined_ThenTheDurabilityIsSeen()
        {
            Assert.Equal(100u, _sut.Durability);
        }

        [Fact]
        public void GivenAStrongerPencil_WhenComparedToOurStandardPencil_ThenTheStrongerPencilWillHaveMoreDurability()
        {
            var strongerPencil = new Pencil(4000);
            Assert.NotInRange(strongerPencil.Durability, uint.MinValue, _sut.Durability);
        }

        [Fact]
        public void GiveAPencil_WhenCharactersAreWritten_ThenTheDurabilityWillReduce()
        {
            _surfaceMoq.Setup(s => s.Write(It.Is<char>(text => text == 's')));
            _sut.WriteOn("s", _surfaceMoq.Object);
            Assert.Equal(99u, _sut.Durability);
        }
        
        [Fact]
        public void GiveAPencil_WhenWhitespaceCharactersAreWritten_ThenTheDurabilityWillNotReduce()
        {
            _surfaceMoq.Setup(s => s.Write(It.Is<char>(text => text == '\t')));
            _surfaceMoq.Setup(s => s.Write(It.Is<char>(text => text == '\n')));
            _surfaceMoq.Setup(s => s.Write(It.Is<char>(text => text == ' ')));
            _sut.WriteOn("\t\n ", _surfaceMoq.Object);
            Assert.Equal(100u, _sut.Durability);
        }
        
        [Fact]
        public void GiveAPencil_WhenAnUpperCaseCharacterIsWritten_ThenTheDurabilityWillReduceByTwo()
        {
            _surfaceMoq.Setup(s => s.Write(It.Is<char>(text => text == 'S')));
            _sut.WriteOn("S", _surfaceMoq.Object);
            Assert.Equal(98u, _sut.Durability);
        }
        
        [Fact]
        public void GiveAPencil_WhenTheDurabilityIsZero_ThenOnlySpacesWillBeWritten()
        {
            _sut = new Pencil(4);
            var sequence = new MockSequence();
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == 'T')));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == 'e')));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == 'x')));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == ' ')));
            _sut.WriteOn("Text", _surfaceMoq.Object);
        }
        
        [Fact]
        public void GiveAPencil_WhenTheDurabilityIsZero_ThenTheDurabilityCannotBeReducedMore()
        {
            _sut = new Pencil(4);
            var sequence = new MockSequence();
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == 'T')));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == 'e')));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == 'x')));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == ' ')));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write(It.Is<char>(text => text == ' ')));
            _sut.WriteOn("Texts", _surfaceMoq.Object);
            Assert.Equal(0u, _sut.Durability);
        }

        [Fact]
        public void GivenAPencil_WhenExamined_ThenTheLengthCanBeDetermined()
        {
            Assert.Equal(20u, _sut.Length);
        }
        
        [Fact]
        public void GivenAPencil_WhenCreated_ThenTTheyCanVaryInLength()
        {
            Assert.Equal(40u, new Pencil(length: 40u).Length);
        }
        
        [Fact]
        public void GivenAPencil_WhenSharpened_ThenTheInitialDurabilityIsRestored()
        {
            _sut.WriteOn("Climbing is cool", _forgivingMoq.Object);
            _sut.Sharpen();
            Assert.Equal(100u, _sut.Durability);
        }
        
        [Fact]
        public void GivenAPencil_WhenSharpened_ThenTheLengthOfThePencilIsReduced()
        {
            _sut.WriteOn("Games Done Quick", _forgivingMoq.Object);
            _sut.Sharpen();
            Assert.Equal(19u, _sut.Length);
        }
        
        [Fact]
        public void GivenAPencil_WhenSharpenedIsCalledMultipleTime_ThenTheLengthOfThePencilIsReducedEachTime()
        {
            _sut.WriteOn("Games Done Quick", _forgivingMoq.Object);
            _sut.Sharpen();
            _sut.Sharpen();
            _sut.Sharpen();
            Assert.Equal(17u, _sut.Length);
        }
        
        [Fact]
        public void GivenAPencil_WhenSharpenedWithZeroLength_ThenThePencilsDurabilityRemainsUntouched()
        {
            _sut = new Pencil(length: 0);
            _sut.WriteOn("Games Done Quick", _forgivingMoq.Object);
            _sut.Sharpen();
            Assert.Equal(83u, _sut.Durability);
        }
        
        [Fact]
        public void GivenAPencil_WhenSharpenedWithZeroLength_ThenThePencilsLengthDoesNotGoNegative()
        {
            _sut = new Pencil(length: 0);
            _sut.WriteOn("Stuff", _forgivingMoq.Object);
            _sut.Sharpen();
            _sut.Sharpen();
            Assert.Equal(0u, _sut.Length);
        }

        [Fact]
        public void GivenAPencil_WhenInstructedToErase_ThenThePencilWillCallEraseOnTheSurface()
        {
            _erasableMock.Setup(t => t.Show()).Returns("Food");
            var sequence = new MockSequence();
            _erasableMock.InSequence(sequence).Setup(s => s.Erase(3));
            _erasableMock.InSequence(sequence).Setup(s => s.Erase(2));
            _erasableMock.InSequence(sequence).Setup(s => s.Erase(1));
            _erasableMock.InSequence(sequence).Setup(s => s.Erase(0));
            _sut.EraseOn("Food", _erasableMock.Object);
            _erasableMock.Verify(t => t.Erase(3), Times.Once);
            _erasableMock.Verify(t => t.Erase(2), Times.Once);
            _erasableMock.Verify(t => t.Erase(1), Times.Once);
            _erasableMock.Verify(t => t.Erase(0), Times.Once);
        }

        [Fact]
        public void GivenAPencil_WhenCreatedTheEraserDurabilityCanBeSet_ThenWhenExaminedTheDurabilityCanBeDetermined()
        {
            _sut = new Pencil(eraserDurability: 30u);
            Assert.Equal(30u, _sut.EraserDurability);
        }

        [Fact]
        public void GivenAPencil_WhenEraseIsCalledWithOneCharacter_ThenTheDurabilityWillGoDownByOne()
        {
            _erasableMock.Setup(t => t.Show()).Returns("T");
            _erasableMock.Setup(t => t.Erase(0));
            _sut.EraseOn("T", _erasableMock.Object);
            Assert.Equal(19u, _sut.EraserDurability);
        }
        
        [Fact]
        public void GivenAPencil_WhenEraseIsCalledWithMultipleCharacters_ThenTheDurabilityWillGoDownByOneForEachCharacter()
        {
            _erasableMock.Setup(t => t.Show()).Returns("Time");
            var sequence = new MockSequence();
            _erasableMock.InSequence(sequence).Setup(s => s.Erase(3));
            _erasableMock.InSequence(sequence).Setup(s => s.Erase(2));
            _erasableMock.InSequence(sequence).Setup(s => s.Erase(1));
            _erasableMock.InSequence(sequence).Setup(s => s.Erase(0));
            _sut.EraseOn("Time", _erasableMock.Object);
            Assert.Equal(16u, _sut.EraserDurability);
        }
        
        [Fact]
        public void GivenAPencil_WhenEraseIsCalledWithWhitespaceCharacters_ThenTheDurabilityWillNotGoDownByOneForEachCharacter()
        {
            _erasableMock.Setup(t => t.Show()).Returns("\nTime\t ");
            var sequence = new MockSequence();
            _erasableMock.InSequence(sequence).Setup(s => s.Erase(6));
            _erasableMock.InSequence(sequence).Setup(s => s.Erase(5));
            _erasableMock.InSequence(sequence).Setup(s => s.Erase(4));
            _erasableMock.InSequence(sequence).Setup(s => s.Erase(3));
            _erasableMock.InSequence(sequence).Setup(s => s.Erase(2));
            _erasableMock.InSequence(sequence).Setup(s => s.Erase(1));
            _erasableMock.InSequence(sequence).Setup(s => s.Erase(0));
            _sut.EraseOn("\nTime\t ", _erasableMock.Object);
            Assert.Equal(16u, _sut.EraserDurability);
        }

        [Fact]
        public void GivenAPencil_WhenEraseIsCalledWithAStringThatDoesntExistOnThePage_ThenNothingWillHappen()
        {
            _erasableMock.Setup(t => t.Show()).Returns("Nope no instruments here");
            _sut.EraseOn("Flute ", _erasableMock.Object);
            Assert.Equal(20u, _sut.EraserDurability);
        }
        
        [Fact]
        public void GivenAPencil_WhenEraseIsCalledWithNoDurability_ThenNothingWillHappen()
        {
            _sut = new Pencil(eraserDurability: 0);
            _erasableMock.Setup(t => t.Show()).Returns("here");
            _sut.EraseOn("here", _erasableMock.Object);
            _erasableMock.Verify(t => t.Erase(0), Times.Never);
            _erasableMock.Verify(t => t.Erase(1), Times.Never);
            _erasableMock.Verify(t => t.Erase(2), Times.Never);
            _erasableMock.Verify(t => t.Erase(3), Times.Never);
        }
    }
}