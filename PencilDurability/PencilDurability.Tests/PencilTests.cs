using Moq;
using PencilDurability.Console;
using Xunit;

namespace PencilDurability.Tests
{
    public interface IMockPaper : ISurface
    {
    }

    public class PencilTests
    {
        private Pencil _sut;
        private readonly Mock<ISurface> _surfaceMoq;
        private readonly Mock<ISurface> _forgivingMoq;

        public PencilTests()
        {
            _surfaceMoq = new Mock<ISurface>(MockBehavior.Strict);
            _forgivingMoq = new Mock<ISurface>();
            _sut = new Pencil();
        }

        [Fact]
        public void
            GivenAPencilAndPaper_WhenInstructedToWriteASingleCharacterOfAString_ThenThePencilWillWriteOnThePaper()
        {
            _surfaceMoq.Setup(s => s.Show()).Returns("");
            _surfaceMoq.Setup(s => s.Write('S', 0));
            _sut.WriteOn("S", _surfaceMoq.Object);
            _surfaceMoq.Verify(s => s.Write('S', 0), Times.Once);
        }

        [Fact]
        public void GivenAPencilAndPaper_WhenInstructedToWriteAStringOfCharacters_ThenThePencilWillWriteOnThePaper()
        {
            _surfaceMoq.Setup(s => s.Show()).Returns("");
            var sequence = new MockSequence();
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('S', 0));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('h', 1));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('e', 2));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write(' ', 3));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('s', 4));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('e', 5));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('l', 6));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('l', 7));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('s', 8));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write(' ', 9));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('s', 10));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('e', 11));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('a', 12));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write(' ', 13));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('s', 14));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('h', 15));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('e', 16));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('l', 17));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('l', 18));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('s', 19));
            _sut.WriteOn("She sells sea shells", _surfaceMoq.Object);
        }

        [Fact]
        public void GivenAPencilAndPaper_WhenInstructedToWriteAnEmptyString_ThenThePencilWillNotWrite()
        {
            _surfaceMoq.Setup(s => s.Show()).Returns("");
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
            Assert.Equal("I have nothing on which to write!",
                Assert.Throws<NothingToWriteOnException>(() => _sut.WriteOn("Something", null)).Message);
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
            _surfaceMoq.Setup(t => t.Show()).Returns("");
            _surfaceMoq.Setup(s => s.Write('s', 0));
            _sut.WriteOn("s", _surfaceMoq.Object);
            Assert.Equal(99u, _sut.Durability);
        }

        [Fact]
        public void GiveAPencil_WhenWhitespaceCharactersAreWritten_ThenTheDurabilityWillNotReduce()
        {
            _surfaceMoq.Setup(t => t.Show()).Returns("");
            _surfaceMoq.Setup(s => s.Write('\t', 0));
            _surfaceMoq.Setup(s => s.Write('\n', 1));
            _surfaceMoq.Setup(s => s.Write(' ', 2));
            _sut.WriteOn("\t\n ", _surfaceMoq.Object);
            Assert.Equal(100u, _sut.Durability);
        }

        [Fact]
        public void GiveAPencil_WhenAnUpperCaseCharacterIsWritten_ThenTheDurabilityWillReduceByTwo()
        {
            _surfaceMoq.Setup(t => t.Show()).Returns("");
            _surfaceMoq.Setup(s => s.Write('S', 0));
            _sut.WriteOn("S", _surfaceMoq.Object);
            Assert.Equal(98u, _sut.Durability);
        }

        [Fact]
        public void GiveAPencil_WhenTheDurabilityIsZero_ThenOnlySpacesWillBeWritten()
        {
            _surfaceMoq.Setup(t => t.Show()).Returns("");
            _sut = new Pencil(4);
            var sequence = new MockSequence();
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('T', 0));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('e', 1));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('x', 2));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write(' ', 3));
            _sut.WriteOn("Text", _surfaceMoq.Object);
        }

        [Fact]
        public void GiveAPencil_WhenTheDurabilityIsZero_ThenTheDurabilityCannotBeReducedMore()
        {
            _surfaceMoq.Setup(t => t.Show()).Returns("");
            _sut = new Pencil(4);
            var sequence = new MockSequence();
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('T', 0));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('e', 1));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write('x', 2));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write(' ', 3));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Write(' ', 4));
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
            _forgivingMoq.Setup(t => t.Show()).Returns("");
            _sut.WriteOn("Climbing is cool", _forgivingMoq.Object);
            _sut.Sharpen();
            Assert.Equal(100u, _sut.Durability);
        }

        [Fact]
        public void GivenAPencil_WhenSharpened_ThenTheLengthOfThePencilIsReduced()
        {
            _forgivingMoq.Setup(t => t.Show()).Returns("");
            _sut.WriteOn("Games Done Quick", _forgivingMoq.Object);
            _sut.Sharpen();
            Assert.Equal(19u, _sut.Length);
        }

        [Fact]
        public void GivenAPencil_WhenSharpenedIsCalledMultipleTime_ThenTheLengthOfThePencilIsReducedEachTime()
        {
            _forgivingMoq.Setup(t => t.Show()).Returns("");
            _sut.WriteOn("Games Done Quick", _forgivingMoq.Object);
            _sut.Sharpen();
            _sut.Sharpen();
            _sut.Sharpen();
            Assert.Equal(17u, _sut.Length);
        }

        [Fact]
        public void GivenAPencil_WhenSharpenedWithZeroLength_ThenThePencilsDurabilityRemainsUntouched()
        {
            _forgivingMoq.Setup(t => t.Show()).Returns("");
            _sut = new Pencil(length: 0);
            _sut.WriteOn("Games Done Quick", _forgivingMoq.Object);
            _sut.Sharpen();
            Assert.Equal(83u, _sut.Durability);
        }

        [Fact]
        public void GivenAPencil_WhenSharpenedWithZeroLength_ThenThePencilsLengthDoesNotGoNegative()
        {
            _forgivingMoq.Setup(t => t.Show()).Returns("");
            _sut = new Pencil(length: 0);
            _sut.WriteOn("Stuff", _forgivingMoq.Object);
            _sut.Sharpen();
            _sut.Sharpen();
            Assert.Equal(0u, _sut.Length);
        }

        [Fact]
        public void GivenAPencil_WhenInstructedToErase_ThenThePencilWillCallEraseOnTheSurface()
        {
            _surfaceMoq.Setup(t => t.Show()).Returns("Food");
            var sequence = new MockSequence();
            _surfaceMoq.InSequence(sequence).Setup(s => s.Erase(3));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Erase(2));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Erase(1));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Erase(0));
            _sut.EraseOn("Food", _surfaceMoq.Object);
            _surfaceMoq.Verify(t => t.Erase(3), Times.Once);
            _surfaceMoq.Verify(t => t.Erase(2), Times.Once);
            _surfaceMoq.Verify(t => t.Erase(1), Times.Once);
            _surfaceMoq.Verify(t => t.Erase(0), Times.Once);
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
            _surfaceMoq.Setup(t => t.Show()).Returns("T");
            _surfaceMoq.Setup(t => t.Erase(0));
            _sut.EraseOn("T", _surfaceMoq.Object);
            Assert.Equal(19u, _sut.EraserDurability);
        }

        [Fact]
        public void
            GivenAPencil_WhenEraseIsCalledWithMultipleCharacters_ThenTheDurabilityWillGoDownByOneForEachCharacter()
        {
            _surfaceMoq.Setup(t => t.Show()).Returns("Time");
            var sequence = new MockSequence();
            _surfaceMoq.InSequence(sequence).Setup(s => s.Erase(3));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Erase(2));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Erase(1));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Erase(0));
            _sut.EraseOn("Time", _surfaceMoq.Object);
            Assert.Equal(16u, _sut.EraserDurability);
        }

        [Fact]
        public void
            GivenAPencil_WhenEraseIsCalledWithWhitespaceCharacters_ThenTheDurabilityWillNotGoDownByOneForEachCharacter()
        {
            _surfaceMoq.Setup(t => t.Show()).Returns("\nTime\t ");
            var sequence = new MockSequence();
            _surfaceMoq.InSequence(sequence).Setup(s => s.Erase(6));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Erase(5));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Erase(4));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Erase(3));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Erase(2));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Erase(1));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Erase(0));
            _sut.EraseOn("\nTime\t ", _surfaceMoq.Object);
            Assert.Equal(16u, _sut.EraserDurability);
        }

        [Fact]
        public void GivenAPencil_WhenEraseIsCalledWithAStringThatDoesntExistOnThePage_ThenNothingWillHappen()
        {
            _surfaceMoq.Setup(t => t.Show()).Returns("Nope no instruments here");
            _sut.EraseOn("Flute ", _surfaceMoq.Object);
            Assert.Equal(20u, _sut.EraserDurability);
        }

        [Fact]
        public void GivenAPencil_WhenEraseIsCalledWithNoDurability_ThenNothingWillHappen()
        {
            _sut = new Pencil(eraserDurability: 0);
            _surfaceMoq.Setup(t => t.Show()).Returns("here");
            _sut.EraseOn("here", _surfaceMoq.Object);
            _surfaceMoq.Verify(t => t.Erase(0), Times.Never);
            _surfaceMoq.Verify(t => t.Erase(1), Times.Never);
            _surfaceMoq.Verify(t => t.Erase(2), Times.Never);
            _surfaceMoq.Verify(t => t.Erase(3), Times.Never);
        }

        [Fact]
        public void GivenAPencil_WhenEraseIsCalledWithLowDurability_ThenOnlyPartialIsErased()
        {
            _sut = new Pencil(eraserDurability: 2);
            _surfaceMoq.Setup(t => t.Show()).Returns("here");
            var sequence = new MockSequence();
            _surfaceMoq.InSequence(sequence).Setup(s => s.Erase(3));
            _surfaceMoq.InSequence(sequence).Setup(s => s.Erase(2));
            _sut.EraseOn("here", _surfaceMoq.Object);
            _surfaceMoq.Verify(t => t.Erase(0), Times.Never);
            _surfaceMoq.Verify(t => t.Erase(1), Times.Never);
        }

        [Fact]
        public void GivenAPencil_WhenToldToEditAtAPositionWithText_ThenTheTextWillFillTheSpace()
        {
            _surfaceMoq.Setup(t => t.Show()).Returns("           ");
            _surfaceMoq.Setup(t => t.Write('T', 10));
            _sut.WriteOn("T", _surfaceMoq.Object, 10);
            _surfaceMoq.Verify(t => t.Write('T', 10));
        }

        [Fact]
        public void GivenAPencil_WhenToldToEditAtAPositionWithAnyText_ThenTheTextWillFillTheSpace()
        {
            _surfaceMoq.Setup(t => t.Show()).Returns("               ");
            _surfaceMoq.Setup(t => t.Write('T', 10));
            _surfaceMoq.Setup(t => t.Write('e', 11));
            _surfaceMoq.Setup(t => t.Write('x', 12));
            _surfaceMoq.Setup(t => t.Write('t', 13));
            _sut.WriteOn("Text", _surfaceMoq.Object, 10);
            _surfaceMoq.Verify(t => t.Write('T', 10));
            _surfaceMoq.Verify(t => t.Write('e', 11));
            _surfaceMoq.Verify(t => t.Write('x', 12));
            _surfaceMoq.Verify(t => t.Write('t', 13));
        }

        [Fact]
        public void GivenAPencil_WhenToldToEditAtAPositionWithAnyTextAndThereIsACollision_ThenTheTextWillBeAnAtSymbol()
        {
            _surfaceMoq.Setup(t => t.Show()).Returns("          F  d");
            _surfaceMoq.Setup(t => t.Write('@', 10));
            _surfaceMoq.Setup(t => t.Write('e', 11));
            _surfaceMoq.Setup(t => t.Write('x', 12));
            _surfaceMoq.Setup(t => t.Write('@', 13));
            _sut.WriteOn("Text", _surfaceMoq.Object, 10);
            _surfaceMoq.Verify(t => t.Write('@', 10));
            _surfaceMoq.Verify(t => t.Write('e', 11));
            _surfaceMoq.Verify(t => t.Write('x', 12));
            _surfaceMoq.Verify(t => t.Write('@', 13));
        }

        [Fact]
        public void
            GivenAPencilWithDurability_WhenToldToWriteAtAPositionAndDurabilityRunsOut_ThenTheTextRemainingTextWillBeLeftAlone()
        {
            _sut = new Pencil(durability: 3);
            _surfaceMoq.Setup(t => t.Show()).Returns("          F  d");
            _surfaceMoq.Setup(t => t.Write('@', 10));
            _surfaceMoq.Setup(t => t.Write('e', 11));
            _surfaceMoq.Setup(t => t.Write('x', 12));
            _surfaceMoq.Setup(t => t.Write('d', 13));
            _sut.WriteOn("Text", _surfaceMoq.Object, 10);
            _surfaceMoq.Verify(t => t.Write('@', 10));
            _surfaceMoq.Verify(t => t.Write('e', 11));
            _surfaceMoq.Verify(t => t.Write('x', 12));
            _surfaceMoq.Verify(t => t.Write('@', 13), Times.Never);
        }
    }
}