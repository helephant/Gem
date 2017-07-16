using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gem.Segments;

namespace Gem
{
    public class Furigana
    {
        private readonly IEnumerable<ISegment> _segments;

        public Furigana(string reading)
        {
            _segments = reading != null ? 
                new FuriganaParser(reading).Parse() : 
                Enumerable.Empty<ISegment>();
        }

        public string Expression => Concat(x => x.Expression);
        public string Hiragana => Concat(x => x.Hiragana);
        public string Reading => Concat(x => x.Reading);
        public string ReadingHtml => Concat(x => x.ReadingHtml);

        private string Concat(Func<ISegment, string> f)
        {
            // probably premature optimization, to be honest
            var stringBuilder = new StringBuilder();
            foreach (var text in _segments.Select(f))
                stringBuilder.Append(text);
            return stringBuilder.ToString();
        }
    }
}
