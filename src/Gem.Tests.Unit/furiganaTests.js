/// <reference path="../Gem/Javascript/furigana.js" />

QUnit.module("Gem.Tests.Javascript.FuriganaTests");


QUnit.test("single gem that spans the entire word", function (assert) {
    var reading = "動物[どうぶつ]";
    var furigana = new Furigana(reading);

    assert.equal(furigana.Reading, reading);
});

QUnit.test("single gem in middle of a word", function (assert) {
    var reading = "新[あたら]しい";
    var furigana = new Furigana(reading);

    assert.equal(furigana.Reading, reading);
});

QUnit.test("multiple gems inside word", function (assert) {
    var reading = "黒[くろ]熊[くま]";
    var furigana = new Furigana(reading);

    assert.equal(furigana.Reading, reading);
});

QUnit.test("readings without gems do not change", function (assert) {
    var reading = "ライオン";
    var furigana = new Furigana(reading);

    assert.equal(furigana.Reading, reading);
});

QUnit.test("honorific should not be included in gem", function (assert) {
    var reading = "お茶[ちゃ]";
    var furigana = new Furigana(reading);

    assert.equal(furigana.Reading, reading);
    assert.equal(furigana.Hiragana, "おちゃ");
    assert.equal(furigana.Expression, "お茶");
    assert.equal(furigana.ReadingHtml, "お<ruby><rb>茶</rb><rt>ちゃ</rt></ruby>");
});

QUnit.test("honorific in middle of a phrase", function (assert) {
    var reading = "東京[とうきょう] お急行[きゅうこう]";
    var furigana = new Furigana(reading);

    assert.equal(furigana.Reading, reading);
});

QUnit.test("honorific in middle of a word", function (assert) {
    var reading = "東京[とうきょう]お急行[きゅうこう]";
    var furigana = new Furigana(reading);

    assert.equal(furigana.Reading, reading);
});

QUnit.test("honorific at end of a word", function (assert) {
    var reading = "茶[ちゃ]お";
    var furigana = new Furigana(reading);

    assert.equal(furigana.Reading, reading);
});

QUnit.test("space can be a delimiter between hiragana sections", function (assert) {
    var reading = "あの 人[ひと]";
    var furigana = new Furigana(reading);

    assert.equal(furigana.Reading, reading);
});

QUnit.test("preserve a space between segments", function (assert) {
    var reading = "東京[とうきょう] 急行[きゅうこう]";
    var furigana = new Furigana(reading);

    assert.equal(furigana.Reading, reading);
    assert.equal(furigana.Expression, "東京 急行");
    assert.equal(furigana.Hiragana, "とうきょう きゅうこう");
    assert.equal(furigana.ReadingHtml, "<ruby><rb>東京</rb><rt>とうきょう</rt></ruby> <ruby><rb>急行</rb><rt>きゅうこう</rt></ruby>");
});

QUnit.test("ignore multiple spaces between segments", function (assert) {
    var reading = "東京[とうきょう]    急行[きゅうこう]";
    var furigana = new Furigana(reading);

    assert.equal(furigana.Reading, "東京[とうきょう] 急行[きゅうこう]");
});

QUnit.test("last character in reading is space", function (assert) {
    var reading = "東京[とうきょう] 急行[きゅうこう]";
    var furigana = new Furigana(reading);

    assert.equal(furigana.Reading, reading);
});

QUnit.test("ignore empty furigana section", function (assert) {
    var reading = "あの[]人[ひと]";
    var furigana = new Furigana(reading);

    assert.equal(furigana.Reading, "あの人[ひと]");
});

QUnit.test("ignore furigana with only whitespace", function (assert) {
    var reading = "あの[ ]人[ひと]";
    var furigana = new Furigana(reading);

    assert.equal(furigana.Reading, "あの人[ひと]");
});

QUnit.test("furigana to expression", function (assert) {
    var reading = "動物[どうぶつ]";
    var furigana = new Furigana(reading);

    assert.equal(furigana.Expression, "動物");
});

QUnit.test("furigana to hiragana", function (assert) {
    var reading = "新[あたら]しい";
    var furigana = new Furigana(reading);

    assert.equal(furigana.Hiragana, "あたらしい");
});

QUnit.test("furigana to html ruby", function (assert) {
    var reading = "新[あたら]しい";
    var furigana = new Furigana(reading);

    assert.equal(furigana.ReadingHtml, "<ruby><rb>新</rb><rt>あたら</rt></ruby>しい");
});

QUnit.test("empty furigana", function (assert) {
    var reading = "";
    var furigana = new Furigana(reading);

    assert.equal(furigana.Reading, reading);
    assert.equal(furigana.Expression, "");
    assert.equal(furigana.Hiragana, "");
    assert.equal(furigana.ReadingHtml, "");
});

QUnit.test("null furigana", function (assert) {
    var reading = "";
    var furigana = new Furigana(undefined);

    assert.equal(furigana.Reading, reading);
    assert.equal(furigana.Expression, "");
    assert.equal(furigana.Hiragana, "");
    assert.equal(furigana.ReadingHtml, "");
});

QUnit.test("allow html inside string", function (assert) {
    var reading = "学生です<span class='particle'>か</span>。";
    var furigana = new Furigana(reading);

    assert.equal(furigana.ReadingHtml, reading);
});
