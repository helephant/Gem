function Furigana(reading) {
    var segments = ParseFurigana(reading || "");

    this.Reading = getReading();
    this.Expression = getExpression();
    this.Hiragana = getHiragana();
    this.ReadingHtml = getReadingHtml();

    function getReading() {
        var reading = "";
        for (var x=0; x<segments.length; x++) {
            reading += segments[x].Reading;
        }
        return reading.trim();
    }

    function getExpression() {
        var expression = "";
        for (var x=0; x<segments.length; x++)
            expression += segments[x].Expression;
        return expression;
    }

    function getHiragana() {
        var hiragana = "";
        for (var x=0; x<segments.length; x++) {
            hiragana += segments[x].Hiragana;
        }
        return hiragana;
    }

    function getReadingHtml() {
        var html = "";
        for (var x = 0; x < segments.length; x++) {
            html += segments[x].ReadingHtml;
        }
        return html;
    }
}

function FuriganaSegment(baseText, furigana) {
    this.Expression = baseText;
    this.Hiragana = furigana.trim();
    this.Reading = baseText + "[" + furigana + "]";
    this.ReadingHtml = "<ruby><rb>" + baseText + "</rb><rt>" + furigana + "</rt></ruby>";
}

function UndecoratedSegment(baseText) {
    this.Expression = baseText;
    this.Hiragana = baseText;
    this.Reading = baseText;
    this.ReadingHtml = baseText;
}

function ParseFurigana(reading) {
    var segments = [];

    var currentBase = "";
    var currentFurigana = "";
    var parsingBaseSection = true;
    var parsingHtml = false;

    var characters = reading.split('');
    
    while (characters.length > 0) {
        var current = characters.shift();

        if (current === '[') {
            parsingBaseSection = false;
        }
        else if (current === ']') {
            nextSegment();
        }
        else if (isLastCharacterInBlock(current, characters) && parsingBaseSection) {
            currentBase += current;
            nextSegment();
        }
        else if (!parsingBaseSection)
            currentFurigana += current;
        else
            currentBase += current;
    }

    nextSegment();

    function nextSegment() {
        if (currentBase)
            segments.push(getSegment(currentBase, currentFurigana));
        currentBase = "";
        currentFurigana = "";
        parsingBaseSection = true;
        parsingHtml = false;
    }

    function getSegment(baseText, furigana) {
        if (!furigana || furigana.trim().length === 0)
            return new UndecoratedSegment(baseText);
        return new FuriganaSegment(baseText, furigana);
    }

    function isLastCharacterInBlock(current, characters) {
        return !characters.length ||
            (isKanji(current) !== isKanji(characters[0]) && characters[0] !== '[');
    }

    function isKanji(character) {
        return character && character.charCodeAt(0) >= 0x4e00 && character.charCodeAt(0) <= 0x9faf;
    }

    return segments;
}

if (typeof module !== 'undefined' && module.exports)
    module.exports = Furigana;
if (typeof define === 'function' && define.amd)
    define(function() { return Furigana; });
else
    global.Furigana = Furigana;