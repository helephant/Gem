using System.Collections.Generic;
using Gem.Segments;

namespace Gem
{
    internal class FuriganaParser
    {
        private readonly string _furigana;

        public FuriganaParser(string furigana)
        {
            _furigana = furigana;
        }

        public IEnumerable<ISegment> Parse()
        {
            var segments = new List<ISegment>();

            var currentIndex = 0;
            var characters = _furigana.ToCharArray();

            var currentBase = string.Empty;
            var currentFurigana = string.Empty;
            var parsingBaseSection = true;

            while (currentIndex < characters.Length)
            {
                if (characters[currentIndex] == '[')
                {
                    parsingBaseSection = false;
                }
                else if (characters[currentIndex] == ']' || 
                    (characters[currentIndex] == ' ') || characters[currentIndex] == 'お')
                {
                    if(!string.IsNullOrEmpty(currentBase))
                        segments.Add(GetSegment(currentBase, currentFurigana));
                    currentBase = string.Empty;
                    currentFurigana = string.Empty;
                    parsingBaseSection = true;

                    if (characters[currentIndex] == 'お')
                        segments.Add(new HonorificSegment());
                    else if (characters[currentIndex] == ' ')
                    {
                        segments.Add(new SpaceSegment());
                        while (!IsLastSpaceInSegment(characters, currentIndex))
                            currentIndex++;
                    }
                    
                }
                else if (!parsingBaseSection)
                    currentFurigana += characters[currentIndex];
                else
                    currentBase += characters[currentIndex];

                currentIndex++;
            }

            if (!string.IsNullOrEmpty(currentBase))
                segments.Add(GetSegment(currentBase, currentFurigana));

            

            return segments;
        }

        private bool IsLastSpaceInSegment(char[] characters, int currentIndex)
        {
            return currentIndex >= (characters.Length - 1) || 
                (characters[currentIndex] == ' '  && characters[currentIndex + 1] != ' ');
        }

        private ISegment GetSegment(string currentBase, string currentFurigana)
        {
            if(!string.IsNullOrEmpty(currentBase) && string.IsNullOrEmpty(currentBase.Trim()))
                return new SpaceSegment();
            if (string.IsNullOrEmpty(currentFurigana))
                return new UndecoratedSegment(currentBase);
            return new FuriganaSegment(currentBase, currentFurigana);
        }
    }
}