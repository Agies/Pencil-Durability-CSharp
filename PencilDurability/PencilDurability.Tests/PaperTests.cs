using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using PencilDurability.Console;
using Xunit;

namespace PencilDurability.Tests
{
    public class PaperTests
    {
        private Paper _sut;

        public PaperTests()
        {
            
        }

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
        public void GivenASheetOfPaper_WhenErasingNothing_NothingIsErased()
        {
            _sut = new Paper("b b b b b b b");
            _sut.Erase("");
            Assert.Equal("b b b b b b b", _sut.Show());
        }
        
        [Fact]
        public void GivenASheetOfPaper_WhenErasingNull_NothingIsErased()
        {
            _sut = new Paper("b b b b b b b");
            _sut.Erase(null);
            Assert.Equal("b b b b b b b", _sut.Show());
        }

        [Fact]
        public void GivenASheetOfPaperWithWritingOnIt_WhenEraseIsCalled_ThenTheLastOccurenceOfTheTextIsRemoved()
        {
            _sut = new Paper("How much wood would a woodchuck chuck if a woodchuck could chuck wood?");
            _sut.Erase("chuck");
            Assert.Equal("How much wood would a woodchuck chuck if a woodchuck could       wood?", _sut.Show());
        }
        
        [Fact]
        public void GivenASheetOfPaperWithWritingOnIt_WhenEraseIsCalledTwice_ThenTheLastOccurenceOfTheTextIsRemovedThenTheNextOccurence()
        {
            _sut = new Paper("How much wood would a woodchuck chuck if a woodchuck could chuck wood?");
            _sut.Erase("chuck");
            _sut.Erase("chuck");
            Assert.Equal("How much wood would a woodchuck chuck if a wood      could       wood?", _sut.Show());
        }
    }
}