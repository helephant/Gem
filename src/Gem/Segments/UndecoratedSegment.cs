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
        public string Hiragana => Expression;
        public virtual string Reading => Expression;
        public string ReadingHtml => _baseText;
    }
}