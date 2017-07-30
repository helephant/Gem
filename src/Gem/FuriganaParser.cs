using System.Collections.Generic;
using Gem.Segments;

namespace Gem
{
    internal class FuriganaParser
    {
        private readonly string _furigana;
        private string _currentBase;
        private string _currentFurigana;
        private bool _parsingBaseSection;
        private List<ISegment> _segments;

        public FuriganaParser(string furigana)
        {
            _furigana = furigana;
        }

        public IEnumerable<ISegment> Parse()
        {
            _segments = new List<ISegment>();

            var currentIndex = 0;
            var characters = _furigana.ToCharArray();
            NextSegment();

            while (currentIndex < characters.Length)
            {
                if (characters[currentIndex] == '[')
                {
                    _parsingBaseSection = false;
                }
                else if (characters[currentIndex] == ']')
                {
                    NextSegment();
                }
                else if (IsLastCharacterInBlock(characters, currentIndex) && _parsingBaseSection)
                {
                    _currentBase += characters[currentIndex];
                    NextSegment();
                }
                else if (!_parsingBaseSection)
                    _currentFurigana += characters[currentIndex];
                else
                    _currentBase += characters[currentIndex];

                currentIndex++;
            }

            NextSegment();

            return _segments;
        }

        private void NextSegment()
        {
            if (!string.IsNullOrEmpty(_currentBase))
                _segments.Add(GetSegment(_currentBase, _currentFurigana));
            _currentBase = string.Empty;
            _currentFurigana = string.Empty;
            _parsingBaseSection = true;
        }

        private bool IsLastCharacterInBlock(char[] characters, int currentIndex)
        {
            return currentIndex >= (characters.Length - 1) || 
                (IsKanji(characters[currentIndex]) != IsKanji(characters[currentIndex + 1]) && characters[currentIndex + 1] != '[');
        }

        private bool IsKanji(char character)
        {
            return character >= 0x4e00 && character <= 0x9faf;
        }

        private ISegment GetSegment(string currentBase, string currentFurigana)
        {
            if (string.IsNullOrEmpty(currentFurigana?.Trim()))
                return new UndecoratedSegment(currentBase);
            return new FuriganaSegment(currentBase, currentFurigana);
        }

    }
}