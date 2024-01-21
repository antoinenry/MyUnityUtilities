public interface ISegment<T> where T : struct
{
    public T A { get; set; }
    public T B { get; set; }
    public bool IsNaN { get; set; }
    public float Length { get; }

    public ISegment<T> Clone();

    public bool Contains(T t, bool strict = false);
    public bool Contains(ISegment<T> s, bool strict = false);
    public bool Crosses(ISegment<T> s, bool strict = false);

    public ISegment<T> Invert();
    public ISegment<T> Intersect(ISegment<T> s);
    public ISegment<T>[] Join(ISegment<T> s);
    public ISegment<T>[] Exclude(ISegment<T> s);
}