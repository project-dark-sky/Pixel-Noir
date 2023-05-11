using System.Collections.Generic;
using System;

public class MinHeap<T>
    where T : IComparable<T>
{
    private List<T> heap;

    public MinHeap()
    {
        heap = new List<T>();
    }

    public void Add(T item)
    {
        heap.Add(item);
        int i = heap.Count - 1;
        while (i > 0)
        {
            int parentIndex = (i - 1) / 2;
            if (heap[parentIndex].CompareTo(heap[i]) <= 0)
            {
                break;
            }
            T temp = heap[parentIndex];
            heap[parentIndex] = heap[i];
            heap[i] = temp;
            i = parentIndex;
        }
    }

    public T ExtractMin()
    {
        if (heap.Count == 0)
        {
            throw new InvalidOperationException("Heap is empty");
        }
        T result = heap[0];
        heap[0] = heap[heap.Count - 1];
        heap.RemoveAt(heap.Count - 1);
        int i = 0;
        while (i < heap.Count)
        {
            int leftChild = 2 * i + 1;
            int rightChild = 2 * i + 2;
            if (leftChild >= heap.Count)
            {
                break;
            }
            int minChild = leftChild;
            if (rightChild < heap.Count && heap[rightChild].CompareTo(heap[leftChild]) < 0)
            {
                minChild = rightChild;
            }
            if (heap[minChild].CompareTo(heap[i]) >= 0)
            {
                break;
            }
            T temp = heap[i];
            heap[i] = heap[minChild];
            heap[minChild] = temp;
            i = minChild;
        }
        return result;
    }

    public int Count
    {
        get { return heap.Count; }
    }
}
