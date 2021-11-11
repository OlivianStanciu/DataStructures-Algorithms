using System;
using C_InANutShell.Unions;

namespace C_InANutShell.Testing
{
    class UnionTest : ITest
    {
        public void Execute()
        {
            string[] set = new string[10];
            for (int i = 0; i < set.Length; i++)
            {
                set[i] = ((char)((int)'a' + i)).ToString();
            }

            UnionFind<string> unionFind = new UnionFind<string>(set, true);

            unionFind.Union("a", "b");
            unionFind.Union("c", "d");
            unionFind.Union("e", "f");
            unionFind.Union("g", "h");
            unionFind.Union("i", "j");

            unionFind.Union("j", "g");
            unionFind.Union("h", "f");
            unionFind.Union("a", "c");
            unionFind.Union("d", "e");
            unionFind.Union("g", "b");
            unionFind.Union("i", "j");

            var x = unionFind.Find("f", "g");
            x = unionFind.Find("a", "c");
            x = unionFind.Find("f", "h");
            x = unionFind.Find("f", "j");
        }
    }
}