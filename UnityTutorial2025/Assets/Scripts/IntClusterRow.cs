using System.Linq;
using UnityEngine;

/// <summary>
/// The integer cluster. Used with the <see cref="IntClusterRow"/> class.
/// </summary>
public struct IntCluster
{
    public int number;
    public int count;
    public int[] array;

    public IntCluster(int number, int count)
    {
        this.number = number;
        this.count = count > 0 ? count : 0;

        if (this.count == 0)
        {
            array = default;
        }
        else
        {
            array = new int[this.count];
            for (int i = 0; i < this.count; i++)
            {
                array[i] = this.number;
            }
        }
    }

    public static IntCluster FromTuple((int number, int count) tuple)
    {
        return new IntCluster(tuple.number, tuple.count);
    }

    public static IntCluster[] FromTuples((int number, int count)[] tuples)
    {
        return tuples.Select(t => FromTuple(t)).ToArray();
    }
}

/// <summary>
/// The integer cluster row. Contains integer clusters of various lengths
/// which can be used to set different odds for different numbers.
/// </summary>
/// <example>
/// The integer cluster row's flattened content could be [0, 0, 0, 0, 4, 4, 6, 8, 10, 10].
/// By using the <see cref="GetNumberAt(int)"/> method, you can get the number at the given index.
/// In this example, at index 2, you'll get 0. Because there are four zeroes and the other numbers aren't as numerous,
/// you're most likely to get a zero when randomizing an index for this row and getting the number at it.
/// </example>
public class IntClusterRow
{
    public IntCluster[] Content { get; }

    public int[] FlattenedContent { get; }

    public int ClusterCount => Content.Length;

    public int Length => FlattenedContent.Length;

    public int[] DistinctNumbers { get; }

    public IntClusterRow(params IntCluster[] intClusters)
    {
        if (intClusters == null)
        {
            return;
        }

        Content = intClusters.Where(c => c.count > 0).ToArray();
        FlattenedContent = Content.SelectMany(c => c.array).ToArray();
        DistinctNumbers = Content.Select(c => c.number).Distinct().ToArray();

        //Debug.Log($"IntClusterRow of length {Length} created. It has {DistinctNumbers.Length} distinct numbers.");
        //Debug.Log($"IntClusterRow's distinct numbers: {string.Join(", ", DistinctNumbers)}.");
        //Debug.Log($"IntClusterRow's content: {string.Join(", ", FlattenedContent)}.");
    }

    public IntClusterRow(params (int number, int count)[] intTuples) : this(IntCluster.FromTuples(intTuples))
    {
    }

    public bool HasNumber(int number)
    {
        return DistinctNumbers.Any(n => n == number);
    }

    public bool IsOutOfBounds(int index)
    {
        return index < 0 || index >= Length;
    }

    public int? GetNumberAt(int index, bool loop = false)
    {
        if (Length == 0 || (!loop && IsOutOfBounds(index)))
        {
            return null;
        }

        if (loop)
        {
            index = (int)Mathf.Repeat(index, Length);
        }

        return FlattenedContent[index];
    }

    public int? GetRandom()
    {
        if (Length == 0)
        {
            return null;
        }

        return FlattenedContent[Random.Range(0, Length)];
    }

    public int GetNumberCount(int number)
    {
        if (!HasNumber(number))
        {
            return 0;
        }

        return Content.Where(c => c.number == number).Sum(c => c.count);
    }
}
