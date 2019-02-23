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
            _sut = new Paper();
        }

        [Fact]
        public void GivenAnEmptySheetOfPaper_WhenWrittenTo_ThenAStringOfCharactersCanBeRead()
        {
            foreach (var text in "She sells sea shells")
            {
                _sut.Write(text);
            }
            var result = _sut.Show();
            Assert.Equal("She sells sea shells", result);
        }
        
        [Fact]
        public void GivenASheetOfPaper_WhenWrittenOn_ThenAStringWillBeAppendedToThePaper()
        {
            foreach (var text in "She sells sea shells")
            {
                _sut.Write(text);
            }
            _sut.Write(' ');
            var result = _sut.Show();
            Assert.Equal("She sells sea shells ", result);
        }

        [Fact]
        public void GivenASheetOfPaper_WhenFirstCreatedIsEmpty_ThenShowWillBeEmpty()
        {
            var result = _sut.Show();
            Assert.Equal("", result);
        }
    }
}