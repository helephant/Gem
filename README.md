# Gem

Gem is little library for working with Japanese furigana.

Japanese writing is made up of two phonetic scripts: hiragana for Japanese words and katakana for loan words. There is also a logographic script called kanji, where a single character represents an idea or a word. All three of these scripts are used in Japanese text.

Since kanji characters aren't phonetic, some Japanese texts show the pronunciation above the kanji characters. This is called furigana. It is often used for text aimed at children, but is very helpful for anyone learning the language. The furigana helpers are called gems. 

The Gem library makes it easier to work with furigana text, particularly on web pages. 
* Display the HTML required to view furigana in the browser using the HTML ruby syntax
* Extract phonetic hiragana text from Japanese text written in the gem syntax
* Extract the plain text without any furigana from Japanese text written in the gem syntax 

## The Gem Syntax
The Gem library uses a simple syntax to encode furigana information in Japanese text. The text to be displayed in the furigana gem is simply included in square brackets following the character or group of characters it relates to. 

For example:
```
 新[あたら]しい
 ```

Will create text that looks like this:
()

## Using the library

Create a new furigana object. Pass in the Japanese text with the extra furigana information encoded in Gem syntax. 
```
var furigana = new Furigana("新[あたら]しい");
```

Then generate:
* The reading - the Japanese text, with any gem information included in the Gem syntax (ie. 新[あたら]しい)
* The expression - the Japanese text without any furigana gems (ie. 新しい)
* Hiragana - the text with any Kanji converted to the phonetic hiragana (ie. あたらしい)
* Ruby Html - HTML syntax required to display the expression with the hiragana gems in the browser (ie. <ruby><rb>新</rb><rt>あたら</rt></ruby>しい)

```
var furigana = new Furigana("新[あたら]しい");

Debug.WriteLine(furigana.Reading);
Debug.WriteLine(furigana.Expression);
Debug.WriteLine(furigana.Hiragana);
Debug.WriteLine(furigana.ReadingHtml);
```



