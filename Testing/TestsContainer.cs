using System;
using System.Collections.Generic;

namespace C_InANutShell.Testing
{
    public class TestsContainer : ITest
    {
        private List<ITest> _tests = new List<ITest>();
        public virtual void Execute()
        {
            foreach (var test in _tests)
            {
                test.Execute();
            }
        }

        public TestsContainer AddTest(ITest test)
        {
            _tests.Add(test);
            return this;
        }

        public TestsContainer RemoveTest(ITest test)
        {
            foreach (var t in _tests)
            {
                if (t.Equals(test))
                {
                    _tests.Remove(t);
                }
            }
            return this;
        }
    }
}