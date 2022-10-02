using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using GameMapNS;

public class Tests_MinHeap
{
    private MinHeap heap;


    // Setup
    [SetUp]
    public void Setup()
    {
        heap = new MinHeap(10);
    }


    // Test create min heap
    [Test]
    public void CreatesMinHeap()
    {
        Assert.IsNotNull(heap);
    }


    // Test insert square
    [Test]
    public void InsertsSquare()
    {
        Square square = new Square(Vector2Int.zero);
        heap.Insert(square);
        heap.Print();
        Assert.AreEqual(square, heap.gridAreas[0]);
    }


    // Test inserts second square
    [Test]
    public void InsertsSecondSquare_Greater()
    {
        Square squareA = new Square(Vector2Int.zero);
        Square squareB = new Square(Vector2Int.one);
        squareB.gCost = 2;
        heap.Insert(squareA);
        heap.Insert(squareB);
        heap.Print();
        Assert.AreEqual(squareA, heap.gridAreas[0]);
        Assert.AreEqual(squareB, heap.gridAreas[1]);
    }


    // Test inserts second square
    [Test]
    public void InsertsSecondSquare_Lesser()
    {
        Square squareA = new Square(Vector2Int.zero);
        Square squareB = new Square(Vector2Int.one);
        squareA.gCost = 2;
        heap.Insert(squareA);
        heap.Insert(squareB);
        heap.Print();
        Assert.AreEqual(squareB, heap.gridAreas[0]);
        Assert.AreEqual(squareA, heap.gridAreas[1]);
    }


    // Test inserts second square
    [Test]
    public void InsertsThirdSquare_Greater()
    {
        Square squareA = new Square(Vector2Int.zero);
        Square squareB = new Square(Vector2Int.one);
        Square squareC = new Square(new Vector2Int(2, 2));
        squareB.gCost = 2;
        squareC.gCost = 4;
        heap.Insert(squareA);
        heap.Insert(squareB);
        heap.Insert(squareC);
        heap.Print();
        Assert.AreEqual(squareA, heap.gridAreas[0]);
        Assert.AreEqual(squareB, heap.gridAreas[1]);
        Assert.AreEqual(squareC, heap.gridAreas[2]);
    }


    // Test inserts second square
    [Test]
    public void InsertsThirdSquare_Lesser()
    {
        Square squareA = new Square(Vector2Int.zero);
        Square squareB = new Square(Vector2Int.one);
        Square squareC = new Square(new Vector2Int(2, 2));
        squareA.gCost = 2;
        squareB.gCost = 1;
        heap.Insert(squareA);
        heap.Insert(squareB);
        heap.Insert(squareC);
        heap.Print();
        Assert.AreEqual(squareC, heap.gridAreas[0]);
        Assert.AreEqual(squareA, heap.gridAreas[1]);
        Assert.AreEqual(squareB, heap.gridAreas[2]);
    }


    // Test get min
    [Test]
    public void Pops_1Square()
    {
        Square squareA = new Square(Vector2Int.zero);
        heap.Insert(squareA);
        Square popSquare = heap.Pop<Square>();
        Assert.AreEqual(squareA, popSquare);
    }


    // Test get min
    [Test]
    public void Pops_2Square_Greater()
    {
        Square squareA = new Square(Vector2Int.zero);
        Square squareB = new Square(Vector2Int.one);
        squareB.gCost = 2;
        heap.Insert(squareA);
        heap.Insert(squareB);
        Square popSquare = heap.Pop<Square>();
        heap.Print();
        Assert.AreEqual(squareA, popSquare);
    }


    // Test get min
    [Test]
    public void Pops_2Square_Lesser()
    {
        Square squareA = new Square(Vector2Int.zero);
        Square squareB = new Square(Vector2Int.one);
        squareA.gCost = 2;
        heap.Insert(squareA);
        heap.Insert(squareB);
        Square popSquare = heap.Pop<Square>();
        heap.Print();
        Assert.AreEqual(squareB, popSquare);
    }


    // Test get min
    [Test]
    public void Pops_3Square_Greater()
    {
        Square squareA = new Square(Vector2Int.zero);
        Square squareB = new Square(Vector2Int.one);
        Square squareC = new Square(new Vector2Int(2, 2));
        squareB.gCost = 2;
        squareC.gCost = 4;
        heap.Insert(squareA);
        heap.Insert(squareB);
        heap.Insert(squareC);
        Square popSquare = heap.Pop<Square>();
        heap.Print();
        Assert.AreEqual(squareA, popSquare);
    }


    // Test get min
    [Test]
    public void Pops_3Square_Lesser()
    {
        Square squareA = new Square(Vector2Int.zero);
        Square squareB = new Square(Vector2Int.one);
        Square squareC = new Square(new Vector2Int(2, 2));
        squareA.gCost = 2;
        squareB.gCost = 1;
        heap.Insert(squareA);
        heap.Insert(squareB);
        heap.Insert(squareC);
        Square popSquare = heap.Pop<Square>();
        heap.Print();
        Assert.AreEqual(squareC, popSquare);
    }
}
