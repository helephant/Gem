using System.Collections.Generic;

namespace Gem
{
    public class Furigana
    {
        private readonly IEnumerable<FuriganaSegment> _segments;

        public Furigana(string reading)
        {
            _segments = new FuriganaParser(reading).Parse();
        }

        public string Expression
        {
            get
            {
                var kanji = string.Empty;
                foreach (var segment in _segments)
                    kanji += segment.BaseText;
                return kanji;
            }
        }

        public string Hiragana
        {
            get
            {
                var hiragana = string.Empty;
                foreach (var segment in _segments)
                {
                    hiragana += segment.HasFurigana ?
                                    segment.Furigana : segment.BaseText;
                }
                return hiragana;
            }
        }

        public string Reading
        {
            get
            {
                string reading = string.Empty;
                foreach (var segment in _segments)
                {
                    reading += segment.Reading;
                }
                return reading.Trim();
            }
        }

        public string ReadingHtml
        {
            get
            {
                var html = string.Empty;
                foreach (var segment in _segments)
                {
                    html += segment.ReadingHtml;
                }
                return html;
            }
        }
    }
}
