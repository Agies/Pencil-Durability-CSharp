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
        public void GivenASheetOfPaper_WhenShown_ThenAStringOfCharactersCanBeRead()
        {
            var result = _sut.Show();
            Assert.Equal("She sells sea shells", result);
        }
    }
}