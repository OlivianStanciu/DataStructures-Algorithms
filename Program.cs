﻿using System;
using C_InANutShell.Testing;

namespace C_InANutShell
{
    class Program
    {
        static void Main(string[] args)
        {
             new TestPerformer()
                .AddTest(
                    test: new DynamicArrayTest(), 
                    shouldBeExecuted: false)
                .AddTest(
                    test: new LinkedListTest(), 
                    shouldBeExecuted: false)
                .AddTest(
                    test: new StackTest(),
                    shouldBeExecuted: false
                )
                .AddTest(
                    test: new QueueTest(),
                    shouldBeExecuted: true
                )
                .AddTest(
                    test: new GraphTest(),
                    shouldBeExecuted: false
                )
                .AddTest(
                    test: new UnionTest(),
                    shouldBeExecuted: false
                )
                .AddTest(
                    test: new BinarySearchTree(),
                    shouldBeExecuted: false
                )
                .AddTest(
                    test: new HashTableTest(),
                    shouldBeExecuted: false
                )
                .AddTest(
                    test: new ArraySortingTest(),
                    shouldBeExecuted: false
                )
                .Execute();
        }
    }
}
