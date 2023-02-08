﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WordFinder
{
    public class WordFinder
    {
        private int _minWordLen = 3;
        private int _maxWordLen = 10;

        private enum Direction
        {
            Horizontal = 0,
            Vertical = 1
        }

        public WordFinder() { }

        public WordFinder(int minWordLen, int maxWordLen)
        {
            _minWordLen = minWordLen;
            _maxWordLen = maxWordLen;
        }

        // Note: this method is faster than RegEx, and excludes
        // non-english characters (unlike char.IsLetter()). 
        private bool IsLetter(char x) => (x >= 'A' && x <= 'Z') || (x >= 'a' && x <= 'z');
        private bool IsLetters(string x) => x.All(y => IsLetter(y));

        public Dictionary<string, bool> Search(char[,] matrix, string[] words)
        {
            // Validate character matrix
            // - Must be non-null
            // - Must be square
            // - Must only contain english characters
            if (matrix == null) throw new ArgumentNullException("Invalid character matrix: matrix must not be null.");
            if (matrix.GetLength(0) != matrix.GetLength(1)) throw new ArgumentException("Invalid character matrix: matrix must be square.");
            if (matrix.Cast<char>().Any(x => !IsLetter(x))) throw new ArgumentException("Invalid character matrix: matrix must only contain english characters (a-z, A-Z).");

            // Validate word list
            // - Must be non-null
            // - Must be non-empty
            // - Must contain words that are greater than the character minimum and not greater than the character maximum defined above
            // - Must contain only english characters
            if (words == null) throw new ArgumentNullException("Invalid word list: word list must not be null.");
            if (words.Length == 0) throw new ArgumentException("Invalid word list: word list must not be empty.");
            if (words.Any(x => x.Length < _minWordLen || x.Length > _maxWordLen)) throw new ArgumentException($"Invalid word list: word list: words must be greater than {_minWordLen - 1} characters and not greater than {_maxWordLen} characters.");
            if (words.Any(x => !IsLetters(x))) throw new ArgumentException("Invalid word list: word list must only contain english words.");

            var result = new Dictionary<string, bool>();

            // Filter out any words that are greater than the largest dimension of the array.
            var maxValidWordLen = matrix.GetLength(1) > matrix.GetLength(0) ? matrix.GetLength(1) : matrix.GetLength(0);

            var validWords = words.Where(x => x.Length <= maxValidWordLen).Distinct().ToArray();
            var invalidWords = words.Where(x => x.Length > maxValidWordLen).Distinct().ToArray();

            // Check valid words.
            foreach (var word in validWords)
            {
                var found = false;

                for (int row = 0; row < matrix.GetLength(0); row++)
                {
                    for (int col = 0; col < matrix.GetLength(1); col++)
                    {
                        // Look for the first character of the word in the matrix.
                        if (char.ToUpperInvariant(matrix[row, col]) == char.ToUpperInvariant(word[0]))
                        {
                            // Word will not be found if there are not enough characters
                            // to the right or below the current character to make the word.
                            if (!found && matrix.GetLength(0) - row >= word.Length) 
                                found |= Search(matrix, word, Direction.Vertical, row, col);
                            if (!found && matrix.GetLength(1) - col >= word.Length) 
                                found |= Search(matrix, word, Direction.Horizontal, row, col);
                        }
                        if (found) break; // Continue if word was found.
                    }
                    if (found) break; // Continue if word was found.
                }

                result.Add(word, found);
            }

            // Notify the user that the invalid words could not be found.
            foreach (var word in invalidWords)
            {
                result.Add(word, false);
            }

            return result;
        }

        private bool Search(char[,] matrix, string word, Direction dir, int currRow, int currCol, string currWord = "")
        {
            currWord += matrix[currRow, currCol];

            // Termination conditions
            if (word.Equals(currWord, StringComparison.OrdinalIgnoreCase)) return true;
            if (!word.StartsWith(currWord, StringComparison.OrdinalIgnoreCase) || word.Length == currWord.Length) return false;

            // Recursion
            if (dir == Direction.Vertical)
                return Search(matrix, word, dir, currRow + 1, currCol, currWord);
            else
                return Search(matrix, word, dir, currRow, currCol + 1, currWord);
        }

        public string PrintResult(Dictionary<string, bool> result)
        {
            var sb = new StringBuilder();
            var shortBar = new string('-', _maxWordLen + 4);

            sb.AppendLine($"|{shortBar}|---------|");
            sb.AppendLine($"| {string.Format($"{{0,-{_maxWordLen + 3}}}", "Word")}| {string.Format("{0,-8}", "Found")}|");
            sb.AppendLine($"|{shortBar}|---------|");
            foreach (var word in result.Keys)
            {
                sb.AppendLine($"|{string.Format($"{{0,{_maxWordLen + 3}}}", word)} |{string.Format("{0, 8}", result[word].ToString())} |");
            }
            sb.Append($"|{shortBar}|---------|");

            return sb.ToString();
        }
    }
}