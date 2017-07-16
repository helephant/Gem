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
        
        // need to preserve spaces if parsing HTML tags
        if (current === '<')
            parsingHtml = true;
        else if (current === '>')
            parsingHtml = false;

        if (current === '[') {
            parsingBaseSection = false;
        } else if (current === ']' || (parsingBaseSection && current === ' ' && !parsingHtml) || current == 'お') {
            if(currentBase && currentBase.trim() !== "")
                segments.push(getSegment(currentBase, currentFurigana));
            currentBase = "";
            currentFurigana = "";
            parsingBaseSection = true;

            if (current === 'お')
                segments.push(new UndecoratedSegment('お'));
            else if (current === ' ') {
                segments.push(new UndecoratedSegment(" "));
                while (!isLastSpaceInSegment(characters))
                    characters.shift();
            }
        }
        else if (!parsingBaseSection)
            currentFurigana += current;
        else
            currentBase += current;
    }

    if (currentBase && currentBase.trim() !== "")
        segments.push(getSegment(currentBase, currentFurigana));

    function getSegment(baseText, furigana) {
        if (!furigana || furigana.trim().length === 0)
            return new UndecoratedSegment(baseText);
        return new FuriganaSegment(baseText, furigana);
    }

    function isLastSpaceInSegment(segments) {
        return segments.length === 0 || segments[0] !== ' ';
    }

    return segments;
}