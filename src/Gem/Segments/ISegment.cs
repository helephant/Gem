namespace Gem.Segments
{
    internal interface ISegment
    {
        string Expression { get; }
        string Hiragana { get; }
        string Reading { get; }
        string ReadingHtml { get; }
    }
}