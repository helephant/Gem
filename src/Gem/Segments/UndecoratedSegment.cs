using System;

namespace Gem.Segments
{
    internal class UndecoratedSegment : ISegment
    {
        private readonly string _baseText;

        public UndecoratedSegment(string baseText)
        {
            _baseText = baseText;
        }

        public string Expression => _baseText;
        public string Hiragana => _baseText;
        public virtual string Reading => _baseText;
        public string ReadingHtml => _baseText;
    }
}