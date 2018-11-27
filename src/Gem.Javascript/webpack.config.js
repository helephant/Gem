var path = require("path");

module.exports = {
    entry: './gem.furigana.js',
    output: {
        path: path.resolve(__dirname, "dist"),
        filename: "gem.furigana.js",
        library: "gemFurigana",
        libraryTarget: 'umd'
    },
    devtool: 'source-map'
}