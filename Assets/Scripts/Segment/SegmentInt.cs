using UnityEngine;

[System.Serializable]
public struct SegmentInt : ISegment<int>
{
    [SerializeField] private int a;
    [SerializeField] private int b;
    [SerializeField] private bool isNan;

    public SegmentInt(int a, int b)
    {
        this.a = a;
        this.b = b;
        isNan = false;
    }
    public SegmentInt Nan => new SegmentInt(0, 0) { isNan = true };
    public ISegment<int> Clone() => new SegmentInt(a, b);

    public override string ToString() => SegmentOperations<SegmentInt,int>.ToString(this);
    public static implicit operator string(SegmentInt s) => s.ToString();

    public int A { get => isNan ? 0 : a; set => a = value; }
    public int B { get => isNan ? 0 : b; set => b = value; }
    public bool IsNaN { get => isNan; set => isNan = value; }
    public float Length => IsNaN ? -1 : Mathf.Abs(A - B);


    public bool Contains(int t, bool strict = false) => SegmentOperations<SegmentInt, int>.Contains(this, t, strict);
    public bool Contains(ISegment<int> s, bool strict = false) => SegmentOperations<SegmentInt, int>.Contains(this, s, strict);
    public bool Crosses(ISegment<int> s, bool strict = false) => SegmentOperations<SegmentInt, int>.Crosses(this, s, strict);

    public ISegment<int> Invert() => (SegmentInt)SegmentOperations<SegmentInt, int>.Invert(this);
    public ISegment<int> Intersect(ISegment<int> s) => SegmentOperations<SegmentInt, int>.Intersection(this, s);
    public ISegment<int>[] Join(ISegment<int> s) => SegmentOperations<SegmentInt, int>.Junction(this, s);
    public ISegment<int>[] Exclude(ISegment<int> s) => SegmentOperations<SegmentInt, int>.Exclusion(this, s);
}