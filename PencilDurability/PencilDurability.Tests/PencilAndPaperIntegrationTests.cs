using PencilDurability.Console;
using Xunit;

namespace PencilDurability.Tests
{
    public class PencilAndPaperIntegrationTests
    {
        [Fact]
        public void GivenAPenAndPaperWithInitialText_WhenThePencilWritesAString_ThenTheTextCanBeSeenOnThePaper()
        {
            var pencil = new Pencil();
            var paper = new Paper("She sells sea shells");
            pencil.WriteOn(" down by the sea shore", paper);
            Assert.Equal("She sells sea shells down by the sea shore", paper.Show());
        }
        
        [Fact]
        public void GivenAPenAndPaperWithInitialText_WhenThePencilErasesAString_ThenTheTextIsReplaceWithSpacesOnThePaper()
        {
            var pencil = new Pencil();
            var paper = new Paper("She sells sea shells");
            pencil.EraseOn("shells", paper);
            Assert.Equal("She sells sea       ", paper.Show());
        }
    }
}