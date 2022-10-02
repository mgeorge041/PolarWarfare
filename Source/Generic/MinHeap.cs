using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinHeap
{
    public int size { get; private set; }
    public GridArea[] gridAreas { get; private set; }

    public MinHeap(int size) 
    {
        gridAreas = new GridArea[size];
    }


    // Print heap
    public void Print()
    {
        for (int i = 0; i < size; i++)
        {
            Debug.Log(i + ": " + gridAreas[i].coords + ": " + gridAreas[i].hCost);
        }
    }


    // Get whether heap contains an item
    public bool Contains(GridArea gridArea)
    {
        for (int i = 0; i < size; i++)
        {
            if (gridAreas[i] == gridArea)
                return true;
        }
        return false;
    }


    // Add value to heap
    public void Insert(GridArea gridArea)
    {
        gridAreas[size] = gridArea;
        SortUp();
        size++;
    }


    // Get min 
    public T Pop<T>() where T : GridArea
    {
        if (size == 0)
            return null;

        GridArea minSquare = gridAreas[0];
        size--;
        SortDown();
        return (T)minSquare;
    }


    // Get parent index
    private int GetParentIndex(int index)
    {
        return (index - 1) / 2;
    }


    // Get left child index
    private int GetLeftChildIndex(int index)
    {
        return index * 2 + 1;
    }


    // Get right child index
    private int GetRightChildIndex(int index)
    {
        return index * 2 + 2;
    }


    // Get whether has left child
    private bool HasLeftChild(int index)
    {
        if (GetLeftChildIndex(index) <= size)
            return true;
        return false;
    }


    // Get whether has right child
    private bool HasRightChild(int index)
    {
        if (GetRightChildIndex(index) <= size)
            return true;
        return false;
    }


    // Sort up
    private void SortUp()
    {
        GridArea square = gridAreas[size];
        int index = size;
        int parentIndex = GetParentIndex(index);
        while (square.hCost < gridAreas[parentIndex].hCost)
        {
            gridAreas[index] = gridAreas[parentIndex];
            gridAreas[parentIndex] = square;
            index = parentIndex;
            parentIndex = GetParentIndex(index);
        }
    }


    // Sort down
    private void SortDown()
    {
        gridAreas[0] = gridAreas[size];
        GridArea square = gridAreas[0];
        int index = 0;
        
        while (index < size)
        {
            int leftChildIndex = GetLeftChildIndex(index);
            int rightChildIndex = GetRightChildIndex(index);
            if (HasLeftChild(index) && gridAreas[leftChildIndex].hCost < square.hCost)
            {
                gridAreas[index] = gridAreas[leftChildIndex];
                gridAreas[leftChildIndex] = square;
                index = leftChildIndex;
            }
            else if (HasRightChild(index) && gridAreas[rightChildIndex].hCost < square.hCost)
            {
                gridAreas[index] = gridAreas[rightChildIndex];
                gridAreas[rightChildIndex] = square;
                index = rightChildIndex;
            }
            else
            {
                break;
            }
        }
    }
}
