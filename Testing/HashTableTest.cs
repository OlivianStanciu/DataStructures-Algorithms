using System;
using C_InANutShell.HashTables;

namespace C_InANutShell.Testing
{
    class HashTableTest : ITest
    {
        public void Execute()
        {
            HashTable<string, int> ht = new HashTable<string, int>();
            
           ht.Add("key1", 1);
           ht.Add("key2", 2);
           ht.Add("key3", 3);
           ht.Add("key4", 4);
           
           ht.Remove("key2");
           ht.Remove("key3");
           ht.Remove("key4");
           
           
           var containsKey4 = ht.Contains("key4"); 

           System.Collections.Hashtable h = new System.Collections.Hashtable();
        }
    }
}