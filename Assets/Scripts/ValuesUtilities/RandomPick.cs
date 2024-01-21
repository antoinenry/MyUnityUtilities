using UnityEngine;

static public class RandomPick
{
    static public T From<T>(params T[] choices)
    {
        int numChoices = choices != null ? choices.Length : 0;
        return numChoices > 0 ? choices[Random.Range(0, numChoices)] : default(T);
    }

    static public int From(IntRange range) => Random.Range(range.a, range.b);

    static public float From(FloatRange range) => Random.Range(range.a, range.b);
}