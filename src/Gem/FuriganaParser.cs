using System.Collections.Generic;

namespace Gem
{
    internal class FuriganaParser
    {
        private readonly string _furigana;

        public FuriganaParser(string furigana)
        {
            _furigana = furigana;
        }

        public IEnumerable<FuriganaSegment> Parse()
        {
            var segments = new List<FuriganaSegment>();

            var e = _furigana.GetEnumerator();
            var currentBase = string.Empty;
            var currentFurigana = string.Empty;
            while (e.MoveNext())
            {

                if (e.Current == '[')
                {
                    if (e.MoveNext())
                        currentFurigana += e.Current;
                }
                else if (e.Current == ']')
                {
                    segments.Add(new FuriganaSegment(currentBase, currentFurigana));
                    currentBase = string.Empty;
                    currentFurigana = string.Empty;
                }
                else if (currentFurigana != string.Empty)
                    currentFurigana += e.Current;
                else
                    currentBase += e.Current;
            }

            if (!string.IsNullOrEmpty(currentBase))
                segments.Add(new FuriganaSegment(currentBase, currentFurigana));

            return segments;
        }
    }
}