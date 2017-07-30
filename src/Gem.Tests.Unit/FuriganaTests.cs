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
        public void NumberShouldNotBeIncludedInGem()
        {
            var reading = "9時[じ]";
            var furigana = new Furigana(reading);

            Assert.That(furigana.Reading, Is.EqualTo(reading));
            Assert.That(furigana.Hiragana, Is.EqualTo("9じ"));
            Assert.That(furigana.Expression, Is.EqualTo("9時"));
            Assert.That(furigana.ReadingHtml, Is.EqualTo("9<ruby><rb>時</rb><rt>じ</rt></ruby>"));
        }

        [Test]
        public void PunctuationShouldNotBeIncludedInGem()
        {
            var reading = "大[おお]きい。犬[いぬ]";
            var furigana = new Furigana(reading);

            Assert.That(furigana.Reading, Is.EqualTo(reading));
            Assert.That(furigana.Hiragana, Is.EqualTo("おおきい。いぬ"));
            Assert.That(furigana.Expression, Is.EqualTo("大きい。犬"));
            Assert.That(furigana.ReadingHtml, Is.EqualTo("<ruby><rb>大</rb><rt>おお</rt></ruby>きい。<ruby><rb>犬</rb><rt>いぬ</rt></ruby>"));
        }

        [Test]
        public void RomajiShouldNotBeIncludedInGem()
        {
            var reading = "Big犬[いぬ]";
            var furigana = new Furigana(reading);

            Assert.That(furigana.Reading, Is.EqualTo(reading));
            Assert.That(furigana.Hiragana, Is.EqualTo("Bigいぬ"));
            Assert.That(furigana.Expression, Is.EqualTo("Big犬"));
            Assert.That(furigana.ReadingHtml, Is.EqualTo("Big<ruby><rb>犬</rb><rt>いぬ</rt></ruby>"));
        }

        [Test]
        public void KatakanaShouldNotBeIncludedInGem()
        {
            var reading = "ローマ字[じ]";
            var furigana = new Furigana(reading);

            Assert.That(furigana.Reading, Is.EqualTo(reading));
            Assert.That(furigana.Hiragana, Is.EqualTo("ローマじ"));
            Assert.That(furigana.Expression, Is.EqualTo("ローマ字"));
            Assert.That(furigana.ReadingHtml, Is.EqualTo("ローマ<ruby><rb>字</rb><rt>じ</rt></ruby>"));
        }

        [Test]
        public void HiraganaShouldNotBeIncludedInGem()
        {
            var reading = "売[う]り場[ば]";
            var furigana = new Furigana(reading);

            Assert.That(furigana.Reading, Is.EqualTo(reading));
            Assert.That(furigana.Hiragana, Is.EqualTo("うりば"));
            Assert.That(furigana.Expression, Is.EqualTo("売り場"));
            Assert.That(furigana.ReadingHtml, Is.EqualTo("<ruby><rb>売</rb><rt>う</rt></ruby>り<ruby><rb>場</rb><rt>ば</rt></ruby>"));
        }

        [Test]
        public void OInFuriganaIsNotTreatedAsHonorific()
        {
            var reading = "起[お]きます";
            var furigana = new Furigana(reading);

            Assert.That(furigana.Reading, Is.EqualTo(reading));
            Assert.That(furigana.Hiragana, Is.EqualTo("おきます"));
            Assert.That(furigana.Expression, Is.EqualTo("起きます"));
            Assert.That(furigana.ReadingHtml, Is.EqualTo("<ruby><rb>起</rb><rt>お</rt></ruby>きます"));
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
            var reading = "あの[ ] 人[ひと]";
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
        public void PreserveMultipleSpacesBetweenSegments()
        {
            var reading = "東京[とうきょう]    急行[きゅうこう]";
            var furigana = new Furigana(reading);

            Assert.That(furigana.Reading, Is.EqualTo("東京[とうきょう]    急行[きゅうこう]"));
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
