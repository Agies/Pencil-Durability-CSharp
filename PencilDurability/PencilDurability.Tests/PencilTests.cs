using System;
using System.Reflection.Metadata.Ecma335;
using Moq;
using PencilDurability.Console;
using Xunit;
using Xunit.Sdk;

namespace PencilDurability.Tests
{
    public class PencilTests
    {
        private readonly Pencil _sut;
        private Mock<ISurface> _surfaceMoq;

        public PencilTests()
        {
            _surfaceMoq = new Mock<ISurface>(MockBehavior.Strict);
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
            Assert.Equal(100, _sut.Durability);
        }

        [Fact]
        public void GivenAStrongerPencil_WhenComparedToOurStandardPencil_ThenTheStrongerPencilWillHaveMoreDurability()
        {
            var strongerPencil = new Pencil(4000);
            Assert.NotInRange(strongerPencil.Durability, int.MinValue, _sut.Durability);
        }

        [Fact]
        public void GiveAPencil_WhenCharactersAreWritten_ThenTheDurabilityWillReduce()
        {
            _surfaceMoq.Setup(s => s.Write(It.Is<char>(text => text == 's')));
            _sut.WriteOn("s", _surfaceMoq.Object);
            Assert.Equal(99, _sut.Durability);
        }
        
        [Fact]
        public void GiveAPencil_WhenWhitespaceCharactersAreWritten_ThenTheDurabilityWillNotReduce()
        {
            _surfaceMoq.Setup(s => s.Write(It.Is<char>(text => text == '\t')));
            _surfaceMoq.Setup(s => s.Write(It.Is<char>(text => text == '\n')));
            _surfaceMoq.Setup(s => s.Write(It.Is<char>(text => text == ' ')));
            _sut.WriteOn("\t\n ", _surfaceMoq.Object);
            Assert.Equal(100, _sut.Durability);
        }
        
        [Fact]
        public void GiveAPencil_WhenAnUpperCaseCharacterIsWritten_ThenTheDurabilityWillReduceByTwo()
        {
            _surfaceMoq.Setup(s => s.Write(It.Is<char>(text => text == 'S')));
            _sut.WriteOn("S", _surfaceMoq.Object);
            Assert.Equal(98, _sut.Durability);
        }
    }
}