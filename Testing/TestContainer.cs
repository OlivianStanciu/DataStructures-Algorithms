using System;
using System.Collections.Generic;

namespace C_InANutShell.Testing
{
    class TestContainer
    {
        public ITest Test { get; set; }
        public bool ShouldBeExecuted { get; set; }
    }
    public class TestPerformer : ITest
    {
        private List<TestContainer> _tests = new List<TestContainer>();
        public virtual void Execute()
        {
            foreach (var testContainer in _tests)
            {
                if (testContainer.ShouldBeExecuted)
                {
                    testContainer.Test.Execute();
                }
            }
        }

        public TestPerformer AddTest(ITest test, bool shouldBeExecuted = true)
        {
            _tests.Add(new TestContainer()
            {
                Test = test,
                ShouldBeExecuted = shouldBeExecuted
            });
            return this;
        }

        public TestPerformer RemoveTest(ITest test)
        {
            foreach (var t in _tests)
            {
                if (t.Test.Equals(test))
                {
                    _tests.Remove(t);
                }
            }
            return this;
        }
    }
}