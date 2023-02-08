using System.Runtime.CompilerServices;
using WordFinder;

namespace WordFinder.Test
{
    [TestClass]
    public class WordFinderTests
    {
        private WordFinder _wf;

        public WordFinderTests() 
        { 
            _wf = new WordFinder(); 
        }

        [TestMethod]
        public void TestSuccess_AllFound()
        {
            var result = _wf.Search(TestData.Matrix, TestData.WordsAllFound);
            Assert.IsTrue(AreEqual(result, TestData.WordsAllFoundResult));
        }

        [TestMethod]
        public void TestSuccess_AllFoundWithDuplicate()
        {
            var result = _wf.Search(TestData.Matrix, TestData.WordsAllFoundWithDuplicate);
            Assert.IsTrue(AreEqual(result, TestData.WordsAllFoundWithDuplicateResult));
        }

        [TestMethod]
        public void TestSuccess_NotAllFound()
        {
            var result = _wf.Search(TestData.Matrix, TestData.WordsNotAllFound);
            Assert.IsTrue(AreEqual(result, TestData.WordsNotAllFoundResult));
        }

        [TestMethod]
        public void TestSuccess1_NotAllFoundWithDuplicate()
        {         
            var result = _wf.Search(TestData.Matrix, TestData.WordsNotAllFoundWithDuplicate);
            Assert.IsTrue(AreEqual(result, TestData.WordsNotAllFoundWithDuplicateResult));
        }

        [TestMethod]
        public void TestException_InvalidCharacterMatrix_NotSquare()
        {
            var exceptionThrown = false;

            try
            {
                var result = _wf.Search(TestData.InvalidMatrix_NotSquare, TestData.WordsAllFound);
            }
            catch (ArgumentException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public void TestException_InvalidCharacterMatrix_NotEnglishCharacters()
        {
            var exceptionThrown = false;

            try
            {
                var result = _wf.Search(TestData.InvalidMatrix_NotEnglishCharacters, TestData.WordsAllFound);
            }
            catch (ArgumentException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public void TestException_InvalidWordList_Empty()
        {
            var exceptionThrown = false;

            try
            {
                var result = _wf.Search(TestData.Matrix, TestData.InvalidWordList_Empty);
            }
            catch (ArgumentException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public void TestException_InvalidWordList_InvalidWordLengthShort()
        {
            var exceptionThrown = false;

            try
            {
                var result = _wf.Search(TestData.Matrix, TestData.InvalidWordList_InvalidWordLengthShort);
            }
            catch (ArgumentException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public void TestException_InvalidWordList_InvalidWordLengthLong()
        {
            var exceptionThrown = false;

            try
            {
                var result = _wf.Search(TestData.Matrix, TestData.InvalidWordList_InvalidWordLengthLong);
            }
            catch (ArgumentException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public void TestException_InvalidWordList_InvalidWordLengthShortLong()
        {
            var exceptionThrown = false;

            try
            {
                var result = _wf.Search(TestData.Matrix, TestData.InvalidWordList_InvalidWordLengthShortLong);
            }
            catch (ArgumentException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public void TestException_InvalidWordList_InvalidCharactersNumbers()
        {
            var exceptionThrown = false;

            try
            {
                var result = _wf.Search(TestData.Matrix, TestData.InvalidWordList_InvalidCharactersNumbers);
            }
            catch (ArgumentException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public void TestException_InvalidWordList_InvalidCharactersSymbols()
        {
            var exceptionThrown = false;

            try
            {
                var result = _wf.Search(TestData.Matrix, TestData.InvalidWordList_InvalidCharactersSymbols);
            }
            catch (ArgumentException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public void TestException_InvalidWordList_InvalidCharactersNonEnglishLetters()
        {
            var exceptionThrown = false;

            try
            {
                var result = _wf.Search(TestData.Matrix, TestData.InvalidWordList_InvalidCharactersNonEnglishLetters);
            }
            catch (ArgumentException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }

        private bool AreEqual(Dictionary<string, bool> dict1, Dictionary<string, bool> dict2)
        {
            if (dict1.Count != dict2.Count) return false;
            if (dict1.Keys.Any(x => !dict2.ContainsKey(x))) return false;
            if (dict1.Keys.Any(x => dict1[x] != dict2[x])) return false;
            return true;
        }
    }
}