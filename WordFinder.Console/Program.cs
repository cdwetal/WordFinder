﻿using System.Diagnostics;
using System.Text;

var characterMatrix = new char[,]
{
    { 'U', 'S', 'I', 'M', 'M', 'L', 'E', 'C', 'J', 'J', 'Q', 'T', 'J', 'Q', 'C', 'P', 'N', 'Z', 'T', 'Z' },
    { 'V', 'T', 'O', 'O', 'T', 'G', 'R', 'C', 'B', 'A', 'W', 'A', 'Y', 'U', 'W', 'S', 'J', 'W', 'U', 'A' },
    { 'F', 'U', 'D', 'D', 'P', 'D', 'E', 'O', 'L', 'B', 'R', 'O', 'C', 'K', 'G', 'K', 'W', 'V', 'C', 'Z' },
    { 'C', 'G', 'D', 'Q', 'O', 'S', 'T', 'S', 'H', 'U', 'R', 'N', 'I', 'C', 'E', 'Y', 'Y', 'F', 'O', 'U' },
    { 'F', 'U', 'E', 'V', 'E', 'L', 'J', 'F', 'V', 'I', 'Q', 'I', 'X', 'V', 'C', 'M', 'F', 'G', 'L', 'G' },
    { 'Z', 'K', 'S', 'M', 'S', 'T', 'M', 'B', 'S', 'K', 'E', 'N', 'W', 'R', 'D', 'T', 'V', 'Y', 'D', 'T' },
    { 'J', 'U', 'Z', 'U', 'U', 'F', 'F', 'P', 'W', 'M', 'D', 'T', 'C', 'O', 'W', 'U', 'G', 'I', 'S', 'U' },
    { 'M', 'N', 'G', 'N', 'F', 'S', 'E', 'J', 'F', 'A', 'K', 'E', 'S', 'L', 'S', 'L', 'X', 'J', 'D', 'C' },
    { 'U', 'X', 'M', 'O', 'S', 'F', 'B', 'I', 'Q', 'O', 'H', 'R', 'O', 'I', 'Y', 'T', 'B', 'B', 'Y', 'Z' },
    { 'V', 'C', 'C', 'C', 'J', 'Z', 'K', 'R', 'D', 'W', 'F', 'F', 'I', 'A', 'M', 'F', 'J', 'P', 'G', 'O' },
    { 'J', 'T', 'F', 'R', 'D', 'R', 'T', 'H', 'W', 'S', 'U', 'A', 'N', 'W', 'I', 'N', 'D', 'O', 'I', 'W' },
    { 'Q', 'L', 'P', 'D', 'C', 'X', 'J', 'E', 'O', 'T', 'N', 'C', 'S', 'C', 'H', 'I', 'L', 'L', 'R', 'H' },
    { 'S', 'N', 'O', 'W', 'F', 'O', 'H', 'P', 'Y', 'R', 'I', 'E', 'T', 'E', 'K', 'Z', 'X', 'D', 'M', 'N' },
    { 'N', 'E', 'J', 'U', 'Q', 'E', 'Y', 'L', 'Y', 'G', 'P', 'O', 'A', 'B', 'G', 'R', 'J', 'P', 'Q', 'V' },
    { 'T', 'Y', 'I', 'V', 'L', 'G', 'K', 'B', 'K', 'R', 'X', 'L', 'N', 'N', 'I', 'L', 'M', 'W', 'T', 'K' },
    { 'A', 'S', 'U', 'T', 'P', 'B', 'D', 'D', 'W', 'Z', 'K', 'T', 'C', 'U', 'C', 'J', 'T', 'Y', 'M', 'D' },
    { 'Z', 'G', 'O', 'K', 'B', 'K', 'B', 'A', 'E', 'M', 'N', 'U', 'E', 'T', 'W', 'D', 'G', 'C', 'J', 'J' },
    { 'E', 'Y', 'E', 'H', 'G', 'I', 'A', 'W', 'Q', 'E', 'O', 'T', 'H', 'Y', 'M', 'Z', 'F', 'Y', 'U', 'Z' },
    { 'H', 'V', 'T', 'P', 'A', 'B', 'G', 'U', 'F', 'N', 'U', 'B', 'G', 'Y', 'K', 'G', 'N', 'L', 'Y', 'S' },
    { 'K', 'X', 'H', 'Z', 'J', 'I', 'L', 'K', 'N', 'C', 'L', 'A', 'S', 'S', 'B', 'F', 'V', 'A', 'X', 'A' }
};

var wordList = new string[]
{
    "cold",
    "wind",
    "snow",
    "chill",
    "class",
    "interface",
    "instance",
    "way",
    "rock",
    "red",
    "blue"
};

try
{
    Console.WriteLine($"Attempting to find these words {string.Join(", ", wordList)} in the following matrix: ");
    Console.WriteLine(new string('-', (characterMatrix.GetLength(1) * 4) + 1));
    for (int row = 0; row < characterMatrix.GetLength(0); row++)
    {
        var line = "|";
        for (int col = 0; col < characterMatrix.GetLength(1); col++)
        {
            line += $" {characterMatrix[row, col]} |";
        }
        Console.WriteLine(line);
    }
    Console.WriteLine(new string('-', (characterMatrix.GetLength(1) * 4) + 1));

    Console.WriteLine("\nResult:");

    var wf = new WordFinder.WordFinder();

    var sw = new Stopwatch();
    sw.Start();
    var result = wf.Search(characterMatrix, wordList);
    sw.Stop();

    var sb = new StringBuilder();
    var shortBar = new string('-', wf.MaxWordLen + 4);

    sb.AppendLine($"|{shortBar}|---------|");
    sb.AppendLine($"| {string.Format($"{{0,-{wf.MaxWordLen + 3}}}", "Word")}| {string.Format("{0,-8}", "Found")}|");
    sb.AppendLine($"|{shortBar}|---------|");
    foreach (var word in result.Keys)
    {
        sb.AppendLine($"|{string.Format($"{{0,{wf.MaxWordLen + 3}}}", word)} |{string.Format("{0, 8}", result[word].ToString())} |");
    }
    sb.Append($"|{shortBar}|---------|");

    Console.WriteLine(sb.ToString());
    Console.WriteLine($"Time elapsed: {sw.ElapsedMilliseconds}ms");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

Console.ReadKey();