using Moq;
using PencilDurability.Console;
using Xunit;

namespace PencilDurability.Tests
{
    public interface IMockPaper: IViewable, ISurface
    {
        
    }
    
    public class PencilTests
    {
        private Pencil _sut;
        private readonly Mock<ISurface> _surfaceMoq;
        private readonly Mock<ISurface> _forgivingMoq;
        private readonly Mock<IMockPaper> _paperMock;

        public PencilTests()
        {
            _paperMock = new Mock<IMockPaper>(MockBehavior.Strict);
            _surfaceMoq = new Mock<ISurface>(MockBehavior.Strict);
            _forgivingMoq = new Mock<ISurface>();
            _sut = new Pencil();
        }

        [Fact]
        public void GivenAPencilAndPaper_WhenInstructedToWriteASingleCharacterOfAString_ThenThePencilWillWriteOnThePaper()
        {
            _surfaceMoq.Setup(s => s.Write('S', null));
            _sut.WriteOn("S", _surfaceMoq.Object);
            _surfaceMoq.Verify(s => s.Write('S', null), Times.Once);
        }

        [Fact]
        public void GivenAPencilAndPaper_WhenInstructedToWriteAStringOfCharacters_ThenThePencilWillWriteOnThePaper()
        {
            var sequence = new MockSequence();
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('S', null));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('h', null));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('e', null));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write(' ', null));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('s', null));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('e', null));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('l', null));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('l', null));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('s', null));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write(' ', null));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('s', null));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('e', null));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('a', null));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write(' ', null));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('s', null));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('h', null));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('e', null));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('l', null));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('l', null));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('s', null));
            _sut.WriteOn("She sells sea shells", _surfaceMoq.Object);
        }

        [Fact]
        public void GivenAPencilAndPaper_WhenInstructedToWriteAnEmptyString_ThenThePencilWillNotWrite()
        {
            _sut.WriteOn("", _surfaceMoq.Object);
            _surfaceMoq.Verify(s => s.Write(It.IsAny<char>(), null), Times.Never);
        }
        
        [Fact]
        public void GivenAPencilAndPaper_WhenInstructedToWriteANull_ThenThePencilWillNotWrite()
        {
            _sut.WriteOn(null, _surfaceMoq.Object);
            _surfaceMoq.Verify(s => s.Write(It.IsAny<char>(), null), Times.Never);
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
            _surfaceMoq.Setup(s => s.Write('s', null));
            _sut.WriteOn("s", _surfaceMoq.Object);
            Assert.Equal(99u, _sut.Durability);
        }
        
        [Fact]
        public void GiveAPencil_WhenWhitespaceCharactersAreWritten_ThenTheDurabilityWillNotReduce()
        {
            _surfaceMoq.Setup(s => s.Write('\t', null));
            _surfaceMoq.Setup(s => s.Write('\n', null));
            _surfaceMoq.Setup(s => s.Write(' ', null));
            _sut.WriteOn("\t\n ", _surfaceMoq.Object);
            Assert.Equal(100u, _sut.Durability);
        }
        
        [Fact]
        public void GiveAPencil_WhenAnUpperCaseCharacterIsWritten_ThenTheDurabilityWillReduceByTwo()
        {
            _surfaceMoq.Setup(s => s.Write('S', null));
            _sut.WriteOn("S", _surfaceMoq.Object);
            Assert.Equal(98u, _sut.Durability);
        }
        
        [Fact]
        public void GiveAPencil_WhenTheDurabilityIsZero_ThenOnlySpacesWillBeWritten()
        {
            _sut = new Pencil(4);
            var sequence = new MockSequence();
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('T', null));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('e', null));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('x', null));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write(' ', null));
            _sut.WriteOn("Text", _surfaceMoq.Object);
        }
        
        [Fact]
        public void GiveAPencil_WhenTheDurabilityIsZero_ThenTheDurabilityCannotBeReducedMore()
        {
            _sut = new Pencil(4);
            var sequence = new MockSequence();
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('T', null));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('e', null));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('x', null));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write(' ', null));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write(' ', null));
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
            _paperMock.Setup(t => t.Show()).Returns("Food");
            var sequence = new MockSequence();
            _paperMock.InSequence(sequence).Setup(s => s.Erase(3));
            _paperMock.InSequence(sequence).Setup(s => s.Erase(2));
            _paperMock.InSequence(sequence).Setup(s => s.Erase(1));
            _paperMock.InSequence(sequence).Setup(s => s.Erase(0));
            _sut.EraseOn("Food", _paperMock.Object);
            _paperMock.Verify(t => t.Erase(3), Times.Once);
            _paperMock.Verify(t => t.Erase(2), Times.Once);
            _paperMock.Verify(t => t.Erase(1), Times.Once);
            _paperMock.Verify(t => t.Erase(0), Times.Once);
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
            _paperMock.Setup(t => t.Show()).Returns("T");
            _paperMock.Setup(t => t.Erase(0));
            _sut.EraseOn("T", _paperMock.Object);
            Assert.Equal(19u, _sut.EraserDurability);
        }
        
        [Fact]
        public void GivenAPencil_WhenEraseIsCalledWithMultipleCharacters_ThenTheDurabilityWillGoDownByOneForEachCharacter()
        {
            _paperMock.Setup(t => t.Show()).Returns("Time");
            var sequence = new MockSequence();
            _paperMock.InSequence(sequence).Setup(s => s.Erase(3));
            _paperMock.InSequence(sequence).Setup(s => s.Erase(2));
            _paperMock.InSequence(sequence).Setup(s => s.Erase(1));
            _paperMock.InSequence(sequence).Setup(s => s.Erase(0));
            _sut.EraseOn("Time", _paperMock.Object);
            Assert.Equal(16u, _sut.EraserDurability);
        }
        
        [Fact]
        public void GivenAPencil_WhenEraseIsCalledWithWhitespaceCharacters_ThenTheDurabilityWillNotGoDownByOneForEachCharacter()
        {
            _paperMock.Setup(t => t.Show()).Returns("\nTime\t ");
            var sequence = new MockSequence();
            _paperMock.InSequence(sequence).Setup(s => s.Erase(6));
            _paperMock.InSequence(sequence).Setup(s => s.Erase(5));
            _paperMock.InSequence(sequence).Setup(s => s.Erase(4));
            _paperMock.InSequence(sequence).Setup(s => s.Erase(3));
            _paperMock.InSequence(sequence).Setup(s => s.Erase(2));
            _paperMock.InSequence(sequence).Setup(s => s.Erase(1));
            _paperMock.InSequence(sequence).Setup(s => s.Erase(0));
            _sut.EraseOn("\nTime\t ", _paperMock.Object);
            Assert.Equal(16u, _sut.EraserDurability);
        }

        [Fact]
        public void GivenAPencil_WhenEraseIsCalledWithAStringThatDoesntExistOnThePage_ThenNothingWillHappen()
        {
            _paperMock.Setup(t => t.Show()).Returns("Nope no instruments here");
            _sut.EraseOn("Flute ", _paperMock.Object);
            Assert.Equal(20u, _sut.EraserDurability);
        }
        
        [Fact]
        public void GivenAPencil_WhenEraseIsCalledWithNoDurability_ThenNothingWillHappen()
        {
            _sut = new Pencil(eraserDurability: 0);
            _paperMock.Setup(t => t.Show()).Returns("here");
            _sut.EraseOn("here", _paperMock.Object);
            _paperMock.Verify(t => t.Erase(0), Times.Never);
            _paperMock.Verify(t => t.Erase(1), Times.Never);
            _paperMock.Verify(t => t.Erase(2), Times.Never);
            _paperMock.Verify(t => t.Erase(3), Times.Never);
        }
        
        [Fact]
        public void GivenAPencil_WhenEraseIsCalledWithLowDurability_ThenOnlyPartialIsErased()
        {
            _sut = new Pencil(eraserDurability: 2);
            _paperMock.Setup(t => t.Show()).Returns("here");
            var sequence = new MockSequence();
            _paperMock.InSequence(sequence).Setup(s => s.Erase(3));
            _paperMock.InSequence(sequence).Setup(s => s.Erase(2));
            _sut.EraseOn("here", _paperMock.Object);
            _paperMock.Verify(t => t.Erase(0), Times.Never);
            _paperMock.Verify(t => t.Erase(1), Times.Never);
        }

        [Fact]
        public void GivenAPencil_WhenToldToEditAtAPositionWithText_ThenTheTextWillFillTheSpace()
        {
            _paperMock.Setup(t => t.Show()).Returns("           ");
            _paperMock.Setup(t => t.Write('T', 10));
            _sut.EditOn(10, "T", _paperMock.Object);
            _paperMock.Verify(t => t.Write('T', 10));
        }
        
        [Fact]
        public void GivenAPencil_WhenToldToEditAtAPositionWithAnyText_ThenTheTextWillFillTheSpace()
        {
            _paperMock.Setup(t => t.Show()).Returns("               ");
            _paperMock.Setup(t => t.Write('T', 10));
            _paperMock.Setup(t => t.Write('e', 11));
            _paperMock.Setup(t => t.Write('x', 12));
            _paperMock.Setup(t => t.Write('t', 13));
            _sut.EditOn(10, "Text", _paperMock.Object);
            _paperMock.Verify(t => t.Write('T', 10));
            _paperMock.Verify(t => t.Write('e', 11));
            _paperMock.Verify(t => t.Write('x', 12));
            _paperMock.Verify(t => t.Write('t', 13));
        }
        
        [Fact]
        public void GivenAPencil_WhenToldToEditAtAPositionWithAnyTextAndThereIsACollision_ThenTheTextWillBeAnAtSymbol()
        {
            _paperMock.Setup(t => t.Show()).Returns("          F  d");
            _paperMock.Setup(t => t.Write('@', 10));
            _paperMock.Setup(t => t.Write('e', 11));
            _paperMock.Setup(t => t.Write('x', 12));
            _paperMock.Setup(t => t.Write('@', 13));
            _sut.EditOn(10, "Text", _paperMock.Object);
            _paperMock.Verify(t => t.Write('@', 10));
            _paperMock.Verify(t => t.Write('e', 11));
            _paperMock.Verify(t => t.Write('x', 12));
            _paperMock.Verify(t => t.Write('@', 13));
        }
    }
}