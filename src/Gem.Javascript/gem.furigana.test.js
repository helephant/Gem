const Furigana = require("./dist/gem.furigana").Furigana;

test("single gem that spans the entire word", () => {
    var reading = "動物[どうぶつ]";
    var furigana = new Furigana(reading);

    expect(furigana.Reading).toBe(reading);
});


test("single gem that spans the entire word", () => {
    var reading = "動物[どうぶつ]";
    var furigana = new Furigana(reading);

    expect(furigana.Reading).toBe(reading);
});

test("single gem in middle of a word", () => {
    var reading = "新[あたら]しい";
    var furigana = new Furigana(reading);

    expect(furigana.Reading).toBe(reading);
});

test("multiple gems inside word", () => {
    var reading = "黒[くろ]熊[くま]";
    var furigana = new Furigana(reading);

    expect(furigana.Reading).toBe(reading);
});

test("readings without gems do not change", () => {
    var reading = "ライオン";
    var furigana = new Furigana(reading);

    expect(furigana.Reading).toBe(reading);
});

test("honorific should not be included in gem", () => {
    var reading = "お茶[ちゃ]";
    var furigana = new Furigana(reading);

    expect(furigana.Reading).toBe(reading);
    expect(furigana.Hiragana).toBe("おちゃ");
    expect(furigana.Expression).toBe("お茶");
    expect(furigana.ReadingHtml).toBe("お<ruby><rb>茶</rb><rt>ちゃ</rt></ruby>");
});

test("honorific should not be included in gem2", () => {
    var reading = "起[お]きます";
    var furigana = new Furigana(reading);

    expect(furigana.Reading).toBe(reading); 
    expect(furigana.Hiragana).toBe("おきます");
    expect(furigana.Expression).toBe("起きます");
    expect(furigana.ReadingHtml).toBe("<ruby><rb>起</rb><rt>お</rt></ruby>きます");
});

test("number should not be included in gem", () => {
    var reading = "9時[じ]";
    var furigana = new Furigana(reading);

    expect(furigana.Reading).toBe(reading);
    expect(furigana.Hiragana).toBe("9じ");
    expect(furigana.Expression).toBe("9時");
    expect(furigana.ReadingHtml).toBe("9<ruby><rb>時</rb><rt>じ</rt></ruby>");
});

test("punctuation should not be included in gem", () => {
    var reading = "大[おお]きい。犬[いぬ]";
    var furigana = new Furigana(reading);

    expect(furigana.Reading).toBe(reading);
    expect(furigana.Hiragana).toBe("おおきい。いぬ");
    expect(furigana.Expression).toBe("大きい。犬");
    expect(furigana.ReadingHtml).toBe("<ruby><rb>大</rb><rt>おお</rt></ruby>きい。<ruby><rb>犬</rb><rt>いぬ</rt></ruby>");
});

test("romaji should not be included in gem", () => {
    var reading = "Big犬[いぬ]";
    var furigana = new Furigana(reading);

    expect(furigana.Reading).toBe(reading);
    expect(furigana.Hiragana).toBe("Bigいぬ");
    expect(furigana.Expression).toBe("Big犬");
    expect(furigana.ReadingHtml).toBe("Big<ruby><rb>犬</rb><rt>いぬ</rt></ruby>");
});

test("katana should not be included in gem", () => {
    var reading = "ローマ字[じ]";
    var furigana = new Furigana(reading);

    expect(furigana.Reading).toBe(reading);
    expect(furigana.Hiragana).toBe("ローマじ");
    expect(furigana.Expression).toBe("ローマ字");
    expect(furigana.ReadingHtml).toBe("ローマ<ruby><rb>字</rb><rt>じ</rt></ruby>");
});

test("katana should not be included in gem", () => {
    var reading = "ローマ字[じ]";
    var furigana = new Furigana(reading);

    expect(furigana.Reading).toBe(reading);
    expect(furigana.Hiragana).toBe("ローマじ");
    expect(furigana.Expression).toBe("ローマ字");
    expect(furigana.ReadingHtml).toBe("ローマ<ruby><rb>字</rb><rt>じ</rt></ruby>");
});

test("hiragana should not be included in gem", () => {
    var reading = "売[う]り場[ば]";
    var furigana = new Furigana(reading);

    expect(furigana.Reading).toBe(reading);
    expect(furigana.Hiragana).toBe("うりば");
    expect(furigana.Expression).toBe("売り場");
    expect(furigana.ReadingHtml).toBe("<ruby><rb>売</rb><rt>う</rt></ruby>り<ruby><rb>場</rb><rt>ば</rt></ruby>");
});

test("お in furigana is not treated as honorific", () => {
    var reading = "起[お]きます";
    var furigana = new Furigana(reading);

    expect(furigana.Reading).toBe(reading);
    expect(furigana.Hiragana).toBe("おきます");
    expect(furigana.Expression).toBe("起きます");
    expect(furigana.ReadingHtml).toBe("<ruby><rb>起</rb><rt>お</rt></ruby>きます");
});

test("honorific in middle of a phrase", () => {
    var reading = "東京[とうきょう] お急行[きゅうこう]";
    var furigana = new Furigana(reading);

    expect(furigana.Reading).toBe(reading);
});

test("honorific in middle of a word", () => {
    var reading = "東京[とうきょう]お急行[きゅうこう]";
    var furigana = new Furigana(reading);

    expect(furigana.Reading).toBe(reading);
});

test("honorific at end of a word", () => {
    var reading = "茶[ちゃ]お";
    var furigana = new Furigana(reading);

    expect(furigana.Reading).toBe(reading);
});

test("space can be a delimiter between hiragana sections", () => {
    var reading = "あの 人[ひと]";
    var furigana = new Furigana(reading);

    expect(furigana.Reading).toBe(reading);
});

test("preserve a space between segments", () => {
    var reading = "東京[とうきょう] 急行[きゅうこう]";
    var furigana = new Furigana(reading);

    expect(furigana.Reading).toBe(reading);
    expect(furigana.Expression).toBe("東京 急行");
    expect(furigana.Hiragana).toBe("とうきょう きゅうこう");
    expect(furigana.ReadingHtml).toBe("<ruby><rb>東京</rb><rt>とうきょう</rt></ruby> <ruby><rb>急行</rb><rt>きゅうこう</rt></ruby>");
});

test("preserve multiple spaces between segments", () => {
    var reading = "東京[とうきょう]    急行[きゅうこう]";
    var furigana = new Furigana(reading);

    expect(furigana.Reading).toBe("東京[とうきょう]    急行[きゅうこう]");
});

test("last character in reading is space", () => {
    var reading = "東京[とうきょう] 急行[きゅうこう]";
    var furigana = new Furigana(reading);

    expect(furigana.Reading).toBe(reading);
});

test("ignore empty furigana section", () => {
    var reading = "あの[]人[ひと]";
    var furigana = new Furigana(reading);

    expect(furigana.Reading).toBe("あの人[ひと]");
});

test("ignore furigana with only whitespace", () => {
    var reading = "あの[ ]人[ひと]";
    var furigana = new Furigana(reading);

    expect(furigana.Reading).toBe("あの人[ひと]");
});

test("furigana to expression", () => {
    var reading = "動物[どうぶつ]";
    var furigana = new Furigana(reading);

    expect(furigana.Expression).toBe("動物");
});

test("furigana to hiragana", () => {
    var reading = "新[あたら]しい";
    var furigana = new Furigana(reading);

    expect(furigana.Hiragana).toBe("あたらしい");
});

test("furigana to html ruby", () => {
    var reading = "新[あたら]しい";
    var furigana = new Furigana(reading);

    expect(furigana.ReadingHtml).toBe("<ruby><rb>新</rb><rt>あたら</rt></ruby>しい");
});

test("empty furigana", () => {
    var reading = "";
    var furigana = new Furigana(reading);

    expect(furigana.Reading).toBe(reading);
    expect(furigana.Expression).toBe("");
    expect(furigana.Hiragana).toBe("");
    expect(furigana.ReadingHtml).toBe("");
});

test("null furigana", () => {
    var reading = "";
    var furigana = new Furigana(undefined);

    expect(furigana.Reading).toBe(reading);
    expect(furigana.Expression).toBe("");
    expect(furigana.Hiragana).toBe("");
    expect(furigana.ReadingHtml).toBe("");
});

test("allow html inside string", () => {
    var reading = "学生です<span class='particle'>か</span>。";
    var furigana = new Furigana(reading);

    expect(furigana.ReadingHtml).toBe(reading);
});
