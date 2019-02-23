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
        public void GivenASheetOfPaper_WhenFirstCreatedIsEmpty_ThenShowWillBeEmpty()
        {
            _sut = new Paper();
            var result = _sut.Show();
            Assert.Equal("", result);
        }
    }
}