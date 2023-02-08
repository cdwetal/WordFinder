namespace WordFinder
{
    public class WordFinder
    {
        private int _minWordLen;
        private int _maxWordLen;

        public int MinWordLen => _minWordLen;
        public int MaxWordLen => _maxWordLen;

        private enum Direction
        {
            Horizontal = 0,
            Vertical = 1
        }

        public WordFinder(int minWordLen = 3, int maxWordLen = 10)
        {
            _minWordLen = minWordLen;
            _maxWordLen = maxWordLen;
        }

        private bool IsLetter(char x) => (x >= 'A' && x <= 'Z') || (x >= 'a' && x <= 'z');
        private bool IsLetters(string x) => x.All(y => IsLetter(y));

        public Dictionary<string, bool> Search(char[,] characterMatrix, string[] wordList)
        {
            // Validate character characterMatrix
            // - Must be non-null
            // - Must be square
            // - Must only contain english characters
            if (characterMatrix == null) throw new ArgumentNullException("Invalid character matrix: character matrix must not be null.");
            if (characterMatrix.GetLength(0) != characterMatrix.GetLength(1)) throw new ArgumentException("Invalid character matrix: character matrix must be square.");
            if (characterMatrix.Cast<char>().Any(x => !IsLetter(x))) throw new ArgumentException("Invalid character matrix: character matrix must only contain english characters (a-z, A-Z).");

            // Validate word list
            // - Must be non-null
            // - Must be non-empty
            // - Must contain wordList that are greater than the character minimum and not greater than the character maximum defined above
            // - Must contain only english characters
            if (wordList == null) throw new ArgumentNullException("Invalid word list: word list must not be null.");
            if (wordList.Length == 0) throw new ArgumentException("Invalid word list: word list must not be empty.");
            if (wordList.Any(x => x.Length < _minWordLen || x.Length > _maxWordLen)) throw new ArgumentException($"Invalid word list: word list: words must be greater than {_minWordLen - 1} characters and not greater than {_maxWordLen} characters.");
            if (wordList.Any(x => !IsLetters(x))) throw new ArgumentException("Invalid word list: word list must only contain english words.");

            var result = new Dictionary<string, bool>();

            // Filter out any wordList that are greater than the largest dimension
            // of the characterMatrix. These wordList can never exist in the character characterMatrix.
            var maxValidWordLen = characterMatrix.GetLength(1) > characterMatrix.GetLength(0) ? characterMatrix.GetLength(1) : characterMatrix.GetLength(0);

            var validWords = wordList.Where(x => x.Length <= maxValidWordLen).Distinct();
            var invalidWords = wordList.Where(x => x.Length > maxValidWordLen).Distinct();

            // Check valid wordList.
            foreach (var word in validWords)
            {
                var found = false;

                // Look for the first character of the word in the characterMatrix.
                for (int row = 0; row < characterMatrix.GetLength(0); row++)
                {
                    for (int col = 0; col < characterMatrix.GetLength(1); col++)
                    {
                        if (char.ToUpperInvariant(characterMatrix[row, col]) == char.ToUpperInvariant(word[0]))
                        {
                            // Word will not be found if there are not enough characters
                            // to the right or below the current character to make the word.
                            if (!found && characterMatrix.GetLength(0) - row >= word.Length) 
                                found |= Search(characterMatrix, word, Direction.Vertical, row, col);
                            if (!found && characterMatrix.GetLength(1) - col >= word.Length) 
                                found |= Search(characterMatrix, word, Direction.Horizontal, row, col);
                        }
                        if (found) break; // Break if word was found.
                    }
                    if (found) break; // Break if word was found.
                }

                result.Add(word, found);
            }

            // Add entries in result for invalid wordList.
            foreach (var word in invalidWords)
            {
                result.Add(word, false);
            }

            return result;
        }

        public Task<Dictionary<string, bool>> SearchAsync(char[,] matrix, string[] words) => Task.Run(() => Search(matrix, words));

        private bool Search(char[,] characterMatrix, string word, Direction dir, int currRow, int currCol, string currWord = "")
        {
            currWord += characterMatrix[currRow, currCol];

            // Termination conditions
            if (word.Equals(currWord, StringComparison.OrdinalIgnoreCase)) return true;
            if (!word.StartsWith(currWord, StringComparison.OrdinalIgnoreCase) || word.Length == currWord.Length) return false;

            // Recursion
            if (dir == Direction.Vertical)
                return Search(characterMatrix, word, dir, currRow + 1, currCol, currWord);
            else
                return Search(characterMatrix, word, dir, currRow, currCol + 1, currWord);
        }
    }
}
