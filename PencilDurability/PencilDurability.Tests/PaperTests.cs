using PencilDurability.Console;
using Xunit;

namespace PencilDurability.Tests
{
    public class PaperTests
    {
        private Paper _sut;

        [Fact]
        public void GivenAnEmptySheetOfPaper_WhenWrittenTo_ThenAStringOfCharactersCanBeRead()
        {
            _sut = new Paper("She sells sea shells");
            var result = _sut.Show();
            Assert.Equal("She sells sea shells", result);
        }
        
        [Fact]
        public void GivenASheetOfPaper_WhenWrittenOn_ThenAStringWillBeAppendedToThePaper()
        {
            _sut = new Paper("She sells sea shells");
            _sut.Write(' ');
            var result = _sut.Show();
            Assert.Equal("She sells sea shells ", result);
        }
        
        [Fact]
        public void GivenASheetOfPaper_WhenWrittenOnMultipleTime_ThenAStringWillBeAppendedToThePaper()
        {
            _sut = new Paper("She sells sea shells");
            _sut.Write(' ');
            _sut.Write('d');
            var result = _sut.Show();
            Assert.Equal("She sells sea shells d", result);
        }

        [Fact]
        public void GivenASheetOfPaper_WhenFirstCreatedIsEmpty_ThenShowWillBeEmpty()
        {
            _sut = new Paper();
            var result = _sut.Show();
            Assert.Equal("", result);
        }

        [Fact]
        public void GivenASheetOfPaperWithWritingOnIt_WhenEraseIsCalled_ThenTheCharacterGiveAtThePositionIsReplacedWithASpace()
        {
            _sut = new Paper("How much wood would a woodchuck chuck if a woodchuck could chuck wood?");
            _sut.Erase(_sut.Show().Length - 7);
            Assert.Equal("How much wood would a woodchuck chuck if a woodchuck could chuc  wood?", _sut.Show());
        }

        [Fact]
        public void
            GivenASheetOfPaperWithWritingOnIt_WhenReplaceIsCalled_ThenThenCharacterAtThePositionShouldBeSwapped()
        {
            _sut = new Paper("An       a day keeps the doctor away");
            _sut.Replace('B', 4);
            Assert.Equal("An  B    a day keeps the doctor away", _sut.Show());
        }
        
        [Fact]
        public void
            GivenASheetOfPaperWithWritingOnIt_WhenReplaceIsCalledPastTheCurrentPosition_ThenThenTheAdditionalCharactersAreAdded()
        {
            _sut = new Paper("An");
            _sut.Replace('B', 10);
            Assert.Equal("An        B", _sut.Show());
        }

        [Fact]
        public void GivenASheetOfPaper_WhenWriteIsCalledWithAPosition_ThenTheCharacterIsWrittenInThePosition()
        {
            _sut = new Paper();
            _sut.Write('L', 12);
            Assert.Equal("            L", _sut.Show());
        }
    }
}