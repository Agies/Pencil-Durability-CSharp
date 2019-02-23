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
        
        [Fact]
        public void GivenASheetOfPaperWithWritingOnIt_WhenEraseIsCalled_ThenTheLastOccurenceOfTheTextIsRemoved()
        {
            var pencil = new Pencil();
            var paper = new Paper("How much wood would a woodchuck chuck if a woodchuck could chuck wood?");
            pencil.EraseOn("chuck", paper);
            Assert.Equal("How much wood would a woodchuck chuck if a woodchuck could       wood?", paper.Show());
        }
        
        [Fact]
        public void GivenASheetOfPaperWithWritingOnIt_WhenEraseIsCalledTwice_ThenTheLastOccurenceOfTheTextIsRemovedThenTheNextOccurence()
        {
            var pencil = new Pencil();
            var paper = new Paper("How much wood would a woodchuck chuck if a woodchuck could chuck wood?");
            pencil.EraseOn("chuck", paper);
            pencil.EraseOn("chuck", paper);
            Assert.Equal("How much wood would a woodchuck chuck if a wood      could       wood?", paper.Show());
        }
        
        [Fact]
        public void GivenASheetOfPaperWithWritingOnIt_WhenTheEraserIsLow_ThenPartialWordsAreErased()
        {
            var pencil = new Pencil(eraserDurability: 3);
            var paper = new Paper("Buffalo Bill");
            pencil.EraseOn("Bill", paper);
            Assert.Equal("Buffalo B   ", paper.Show());
        }

        [Fact]
        public void GivenASheetOfPaperWithWritingOnIt_WhenEditingIsCalled_ThenLettersOnThePageWillBeErased()
        {
            var pencil = new Pencil();
            var paper = new Paper("An       a day keeps the doctor away");
            pencil.EditOn(3, "artichoke", paper);
            Assert.Equal("An artich@k@ay keeps the doctor away", paper.Show());
        }
    }
}