using System;

namespace Gem
{
    internal class FuriganaSegment : IEquatable<FuriganaSegment>
    {
        private readonly string _baseText;
        private readonly string _furigana;

        public FuriganaSegment(string baseText, string furigana)
        {
            _baseText = baseText;
            if (!string.IsNullOrEmpty(furigana))
                _furigana = furigana;
        }

        public FuriganaSegment(string baseText)
        {
            _baseText = baseText;
        }

        public string BaseText
        {
            get { return _baseText; }
        }

        public string Furigana
        {
            get { return _furigana; }
        }

        public string Reading
        {
            get
            {
                if (HasFurigana)
                    return string.Format("{0}[{1}]", BaseText, Furigana);
                return BaseText;
            }
        }

        public bool HasFurigana
        {
            get { return !string.IsNullOrEmpty(Furigana); }
        }

        public string ReadingHtml
        {
            get
            {
                if (!string.IsNullOrEmpty(_furigana))
                    return string.Format("<ruby><rb>{0}</rb><rt>{1}</rt></ruby>", _baseText, _furigana);
                return _baseText;
            }
        }

        #region equality
        public bool Equals(FuriganaSegment other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(_baseText, other._baseText) && string.Equals(_furigana, other._furigana);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FuriganaSegment)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((_baseText != null ? _baseText.GetHashCode() : 0) * 397) ^ (_furigana != null ? _furigana.GetHashCode() : 0);
            }
        }
        #endregion
    }
}