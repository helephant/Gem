# Gem

Gem is little .NET and javascript library for working with Japanese furigana.

Japanese writing is made up of two phonetic scripts: hiragana for Japanese words and katakana for loan words. There is also a logographic script called kanji, where a single character represents an idea or a word. All three of these scripts are used in Japanese text.

Since kanji characters aren't phonetic, some Japanese texts show the pronunciation above the kanji characters. This is called furigana. It is often used for text aimed at children, but is very helpful for anyone learning the language. The furigana helpers are called gems. 

![](https://github.com/helephant/Gem/blob/master/docs/yotsuba.jpg)

The Gem library makes it easier to work with furigana text, particularly on web pages. 
* Display the HTML required to view furigana in the browser using the [HTML ruby syntax](http://www.w3.org/International/articles/ruby/)
* Extract phonetic hiragana text from Japanese text written in the gem syntax
* Extract the plain text without any furigana from Japanese text written in the gem syntax 

## The Gem Syntax
The Gem library uses a simple syntax to encode furigana information in Japanese text. The text to be displayed in the furigana gem is simply included in square brackets following the character or group of characters it relates to. 

For example:
```
 新[あたら]しい
 ```

Will create text that looks like this:

![](https://github.com/helephant/Gem/blob/master/docs/atarashi.png)

If you have a furigana section with a mix of hiragana and kanji (like あの人), you can add a space to mark the where to begin the text that furigana should appear above. 

For example:
```
 あの 人[ひと]
 ```

 Will create text that looks like this:

![](https://github.com/helephant/Gem/blob/master/docs/anohito-example.png)

Once you have created a furigana object, it is possible to extract:
* The reading - the Japanese text, with any gem information included in the Gem syntax (ie. 新[あたら]しい)
* The expression - the Japanese text without any furigana gems (ie. 新しい)
* Hiragana - the text with any Kanji converted to the phonetic hiragana (ie. あたらしい)
* Ruby Html - HTML syntax required to display the expression with the hiragana gems in the browser (ie. <ruby><rb>新</rb><rt>あたら</rt></ruby>しい)

## Using the .NET library

Gem has a [nuget package](https://www.nuget.org/packages/Gem/) for .NET, so you can install it into your project using the following command:
```
Install-Package Gem
```

Then you can create .NET furigana objects. Pass in the Japanese text with the extra furigana information encoded in Gem syntax. 
```
var furigana = new Furigana("新[あたら]しい");
```

Then you can generate the reading, expression, hiragana or ruby HTML.
```
var furigana = new Furigana("新[あたら]しい");

Debug.WriteLine(furigana.Reading);
Debug.WriteLine(furigana.Expression);
Debug.WriteLine(furigana.Hiragana);
Debug.WriteLine(furigana.ReadingHtml);
```

## Using the javascript library

Gem also has an [npm package](https://www.npmjs.com/package/gem-furigana), so you can install it into your project using the following command:
```
npm install gem-furigana
```

Then you can create Javascript furigana objects. Pass in the Japanese text with the extra furigana information encoded in Gem syntax. 
```
const Furigana = require("gem-furigana");
var furigana = new Furigana("新[あたら]しい");
```

Then you can generate the reading, expression, hiragana or ruby HTML.
```
console.log(furigana.Reading);
console.log(furigana.Expression);
console.log(furigana.Hiragana);
console.log(furigana.ReadingHtml);
```
