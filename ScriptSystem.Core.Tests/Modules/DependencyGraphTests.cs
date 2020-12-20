using System;
using Xunit;

using ScriptSystem.Core.Utilities;

namespace ScriptSystem.Core.Tests.Modules
{
    public class DependencyGraphTests
    {
        [Fact]
        public void TestDependents()
        {
            var graph = new DependencyGraph<Guid>();
            var item1 = Guid.NewGuid();
            var dep1 = Guid.NewGuid();

            graph.Add(item1, dep1);

            // we should contain all the dependencies now
            Assert.True(graph.Contains(item1));
            Assert.Contains(dep1, graph.GetDependencies(item1));
            Assert.True(graph.GetDependencies(dep1).IsEmpty);
            Assert.True(graph.GetDependents(item1).IsEmpty);
            Assert.Contains(item1, graph.GetDependents(dep1));

            // removing should fail with dependencies still in place
            Assert.False(graph.Remove(dep1));

            // but succeed after we remove all dependents
            Assert.True(graph.Remove(item1));
            Assert.True(graph.Remove(dep1));
        }
    }
}