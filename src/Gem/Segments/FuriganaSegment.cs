namespace Gem.Segments
{
    internal class FuriganaSegment : ISegment
    {
        private readonly string _baseText;
        private readonly string _furigana;

        public FuriganaSegment(string baseText, string furigana)
        {
            _baseText = baseText;
            _furigana = furigana;
        }

        public string Expression => _baseText;
        public string Hiragana => _furigana;
        public virtual string Reading => $"{_baseText}[{_furigana}]";
        public string ReadingHtml => $"<ruby><rb>{_baseText}</rb><rt>{_furigana}</rt></ruby>";
    }
}