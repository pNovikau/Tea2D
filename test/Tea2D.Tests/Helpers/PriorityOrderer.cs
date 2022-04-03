using System.Collections.Generic;
using System.Linq;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Tea2D.Tests.Helpers
{
    public class PriorityOrderer : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
        {
            return testCases
                .Select(p => new {TestCase = p, Priority = GetTestCasePriority(p.TestMethod)})
                .OrderByDescending(p => p.Priority)
                .Select(p => p.TestCase);
        }

        private static int GetTestCasePriority(ITestMethod testMethod)
        {
            var attr = testMethod.Method.GetCustomAttributes(typeof(TestPriorityAttribute).AssemblyQualifiedName).ToArray();

            return attr.Length == 0 ? 0 : attr[0].GetNamedArgument<int>("Priority");
        }
    }
}