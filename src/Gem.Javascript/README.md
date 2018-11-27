Gem is little library for working with Japanese furigana.

Japanese writing is made up of two phonetic scripts: hiragana for Japanese words and katakana for loan words. There is also a logographic script called kanji, where a single character represents an idea or a word. All three of these scripts are used in Japanese text.

Since kanji characters aren't phonetic, some Japanese texts show the pronunciation above the kanji characters. This is called furigana. It is often used for text aimed at children, but is very helpful for anyone learning the language. The furigana helpers are called gems. 

The Gem library makes it easier to work with furigana text, particularly on web pages. You can:
* Display the HTML required to view furigana in the browser using the [HTML ruby syntax](http://www.w3.org/International/articles/ruby/)
* Extract phonetic hiragana text from Japanese text written in the gem syntax
* Extract the plain text without any furigana from Japanese text written in the gem syntax 

## The Gem Syntax
The Gem library uses a simple syntax to encode furigana information in Japanese text. The text to be displayed in the furigana gem is simply included in square brackets following the character or group of characters it relates to. 

For example:
```
 新[あたら]しい
 ```


## Using the library

Gem is an [npm package](https://www.npmjs.com/package/gem-furigana), so you can install it into your project using the following command:
```
npm install gem-furigana
```

Then you can create furigana objects. Pass in the Japanese text with the extra furigana information encoded in Gem syntax. 
```
const Furigana = require("gem-furigana").Furigana;
var furigana = new Furigana("新[あたら]しい");
```

Then it's possible to generate:
* The reading - the Japanese text, with any gem information included in the Gem syntax (ie. 新[あたら]しい)
* The expression - the Japanese text without any furigana gems (ie. 新しい)
* Hiragana - the text with any Kanji converted to the phonetic hiragana (ie. あたらしい)
* Ruby Html - HTML syntax required to display the expression with the hiragana gems in the browser (ie. <ruby><rb>新</rb><rt>あたら</rt></ruby>しい)

```
const Furigana = require("gem-furigana").Furigana;
var furigana = new Furigana("新[あたら]しい");

console.log(furigana.Reading);		// 新[あたら]しい
console.log(furigana.Expression);	// 新しい
console.log(furigana.Hiragana);		// あたらしい
console.log(furigana.ReadingHtml);	// &lt;ruby&gt;&lt;rb&gt;新&lt;/rb&gt;&lt;rt&gt;あたら&lt;/rt&gt;&lt;/ruby&gt;しい
```