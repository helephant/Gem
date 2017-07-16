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
        public void HonorificShouldNotBeIncludedInGem()
        {
            var reading = "お茶[ちゃ]";
            var furigana = new Furigana(reading);

            Assert.That(furigana.Reading, Is.EqualTo(reading));
            Assert.That(furigana.Hiragana, Is.EqualTo("おちゃ"));
            Assert.That(furigana.Expression, Is.EqualTo("お茶"));
            Assert.That(furigana.ReadingHtml, Is.EqualTo("お<ruby><rb>茶</rb><rt>ちゃ</rt></ruby>"));
        }

        [Test]
        public void HonorificInMiddleOfPhrase()
        {
            var reading = "東京[とうきょう] お急行[きゅうこう]";
            var furigana = new Furigana(reading);

            Assert.That(furigana.Reading, Is.EqualTo(reading));
        }

        [Test]
        public void HonorificInMiddleOfWord()
        {
            // shouldn't happen, but I still don't want it to crash
            var reading = "東京[とうきょう]お急行[きゅうこう]";
            var furigana = new Furigana(reading);

            Assert.That(furigana.Reading, Is.EqualTo(reading));
        }

        [Test]
        public void HonorificAtEndOfWord()
        {
            // shouldn't happen, but I still don't want it to crash
            var reading = "茶[ちゃ]お";
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
            var reading = "あの[] 人[ひと]";
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
        public void PreserveASpaceBetweenSegments()
        {
            var reading = "東京[とうきょう] 急行[きゅうこう]";
            var furigana = new Furigana(reading);

            Assert.That(furigana.Reading, Is.EqualTo("東京[とうきょう] 急行[きゅうこう]"));
            Assert.That(furigana.Expression, Is.EqualTo("東京 急行"));
            Assert.That(furigana.Hiragana, Is.EqualTo("とうきょう きゅうこう"));
            Assert.That(furigana.ReadingHtml, Is.EqualTo("<ruby><rb>東京</rb><rt>とうきょう</rt></ruby> <ruby><rb>急行</rb><rt>きゅうこう</rt></ruby>"));
        }

        [Test]
        public void IgnoreMultipleSpacesBetweenSegments()
        {
            var reading = "東京[とうきょう]    急行[きゅうこう]";
            var furigana = new Furigana(reading);

            Assert.That(furigana.Reading, Is.EqualTo("東京[とうきょう] 急行[きゅうこう]"));
        }

        [Test]
        public void LastCharacterInReadingIsSpace()
        {
            var reading = "東京[とうきょう] 急行[きゅうこう] ";
            var furigana = new Furigana(reading);

            Assert.That(furigana.Reading, Is.EqualTo("東京[とうきょう] 急行[きゅうこう] "));
        }

        [Test]
        public void PreserveSpaceWhenHonorificIsInMiddleOfReading()
        {
            var reading = "東京[とうきょう] お茶[ちゃ]";
            var furigana = new Furigana(reading);

            Assert.That(furigana.Reading, Is.EqualTo(reading));
            Assert.That(furigana.Hiragana, Is.EqualTo("とうきょう おちゃ"));
            Assert.That(furigana.ReadingHtml, Is.EqualTo("<ruby><rb>東京</rb><rt>とうきょう</rt></ruby> お<ruby><rb>茶</rb><rt>ちゃ</rt></ruby>"));
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

        [Test]
        public void EmptyReading()
        {
            var reading = "";
            var formatter = new Furigana(reading);

            Assert.That(formatter.Reading, Is.EqualTo(""));
            Assert.That(formatter.Expression, Is.EqualTo(""));
            Assert.That(formatter.Hiragana, Is.EqualTo(""));
            Assert.That(formatter.ReadingHtml, Is.EqualTo(""));
        }

        [Test]
        public void NullReading()
        {
            var formatter = new Furigana(null);

            Assert.That(formatter.Reading, Is.EqualTo(""));
            Assert.That(formatter.Expression, Is.EqualTo(""));
            Assert.That(formatter.Hiragana, Is.EqualTo(""));
            Assert.That(formatter.ReadingHtml, Is.EqualTo(""));
        }
    }
}
