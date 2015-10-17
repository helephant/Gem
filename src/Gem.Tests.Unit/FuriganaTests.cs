using System.Diagnostics;
using NUnit.Framework;

namespace Gem.Tests.Unit
{
    [TestFixture]
    public class FuriganaTests
    {
        [Test]
        public void SingleGemThatSpansEntireWord()
        {
            var reading = "動物[どうぶつ]";
            var furigana = new Furigana(reading);

            Assert.That(furigana.Reading, Is.EqualTo(reading));
        }

        [Test]
        public void SingleGemInMiddleOfWord()
        {
            var reading = "新[あたら]しい";
            var furigana = new Furigana(reading);

            Assert.That(furigana.Reading, Is.EqualTo(reading));
        }

        [Test]
        public void MultipleGemsInsideWord()
        {
            var reading = "黒[くろ]熊[くま]";
            var furigana = new Furigana(reading);

            Assert.That(furigana.Reading, Is.EqualTo(reading));
        }

        [Test]
        public void ReadingsWithoutGemsDoNotChange()
        {
            var reading = "ライオン";
            var furigana = new Furigana(reading);

            Assert.That(furigana.Reading, Is.EqualTo(reading));
        }

        [Test]
        public void SpaceCanBeDelimiterBetweenFuriganaSegments()
        {
            var reading = "あの 人[ひと]";
            var furigana = new Furigana(reading);

            Assert.That(furigana.Reading, Is.EqualTo("あの 人[ひと]"));
        }

        [Test]
        public void IgnoreEmptyFuriganaSection()
        {
            var reading = "あの[]人[ひと]";
            var furigana = new Furigana(reading);

            Assert.That(furigana.Reading, Is.EqualTo("あの 人[ひと]"));
        }

        [Test]
        public void IgnoreFuriganaWithOnlyWhitespace()
        {
            var reading = "あの[ ]人[ひと]";
            var furigana = new Furigana(reading);

            Assert.That(furigana.Reading, Is.EqualTo("あの 人[ひと]"));
        }

        [Test]
        public void FuriganaToExpression()
        {
            var reading = "動物[どうぶつ]";
            var furigana = new Furigana(reading);

            Assert.That(furigana.Expression, Is.EqualTo("動物"));
        }

        [Test]
        public void FuriganaToHiragana()
        {
            var reading = "新[あたら]しい";
            var furigana = new Furigana(reading);

            Assert.That(furigana.Hiragana, Is.EqualTo("あたらしい"));
        }

        [Test]
        public void FuriganaToHtmlRuby()
        {
            var reading = "新[あたら]しい";
            var result = "<ruby><rb>新</rb><rt>あたら</rt></ruby>しい";
            var formatter = new Furigana(reading);

            Assert.That(formatter.ReadingHtml, Is.EqualTo(result));
        }
    }
}
