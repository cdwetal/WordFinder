# Word Finder+
Word Finder+ exercise for Synapse Health

## Project Structure
The Word Finder+ project contains three C#.NET projects:
1. **WordFinder** - The Word Finder+ class library. This project contains the main Word Finder+ code.
2. **WordFinder.Test** - The Word Finder+ unit test project. This project contains all of the unit tests for Word Finder+.
3. **WordFinder.Console** - The Word Finder+ console test application. This project contains a console application for testing Word Finder+. Set this project as the startup project in Visual Studio to run the console application.

## Using Word Finder+
> **_NOTE:_**  Because Word Finder+ targets .NET 6.0, the project solution must be built in Visual Studio 2022 or newer.

To use Word Finder+, simply reference the `WordFinder` project or the built `WordFinder.dll` file, create an instance of the `WordFinder` class, and call the `Search(char[,] characterMatrix, string[] wordList)` function. For example: 
``` c#
var characterMatrix = new char[,]
{
  {'A', 'Q', 'B', 'M'},
  {'N', 'C', 'A', 'T'},
  {'Y', 'D', 'T', 'R'},
  {'Z', 'B', 'J', 'L'},
};

var wordList = new string[]
{
  "cat",
  "bat",
  "rat",
};

var wf = new WordFinder.WordFinder();
var result = wf.Search(characterMatrix, wordList);
```
The result of the `Search` function is a `Dictionary<string, bool>` containing the words that were searched for and a boolean indicating if the word was found.

An awaitable version of the `Search` function is provided, if necessary, under the alias `SearchAsync`.

## Unit Tests
The `WordFinder.Test` project contains the following unit tests for the Word Finder+ code:
+ **TestSuccess_AllFound** - Tests the `Search` function with a list of words that are all contained in the character matrix.
+ **TestSuccess_AllFoundWithDuplicate** - Tests the `Search` function with a list of words that are all contained in the character matrix. Some words appear more than once in the word list.
+ **TestSuccess_NotAllFound** - Tests the `Search` function with a list of words where some are contained in the character matrix and some are not.
+ **TestSuccess_NotAllFoundWithDuplicate** - Tests the `Search` function with a list of words where some are contained in the character matrix and some are not. Some words appear more than once in the word list.
+ **TestException_InvalidCharacterMatrix_NotSquare** - Tests the `Search` function with an invalid character matrix that is not square.
+ **TestException_InvalidCharacterMatrix_NotEnglishCharacters** - Tests the `Search` function with an invalid character matrix that contains non-english characters.
+ **TestException_InvalidWordList_Empty** - Tests the `Search` function with an invalid word list that is empty.
+ **TestException_InvalidWordList_InvalidWordLengthShort** - Tests the `Search` function with an invalid word list that contains words that are too short.
+ **TestException_InvalidWordList_InvalidWordLengthLong** - Tests the `Search` function with an invalid word list that contains words that are too long.
+ **TestException_InvalidWordList_InvalidWordLengthShortLong** - Tests the `Search` function with an invalid word list that contains words that are too short and too long.
+ **TestException_InvalidWordList_InvalidCharactersNumbers** - Tests the `Search` function with an invalid word list that contains words that contain numbers.
+ **TestException_InvalidWordList_InvalidCharactersSymbols** - Tests the `Search` function with an invalid word list that contains words that contain symbols.
+ **TestException_InvalidWordList_InvalidCharactersNonEnglishLetters** - Tests the `Search` function with an invalid word list that contains words that contain non-english characters.
