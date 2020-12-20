using System;
using ChaosInitiative.ScriptSystem.Core.Utilities;
using NUnit.Framework;

namespace ChaosInitiative.ScriptSystem.Core.Tests.Modules
{
    public class DependencyGraphTests
    {
        [Test]
        public void TestDependents()
        {
            var graph = new DependencyGraph<Guid>();
            var item1 = Guid.NewGuid();
            var dep1 = Guid.NewGuid();

            graph.Add(item1, dep1);

            // we should contain all the dependencies now
            Assert.That(graph, Has.One.EqualTo(item1));
            Assert.That(graph.GetDependencies(dep1), Is.Empty);
            Assert.That(graph.GetDependents(item1), Is.Empty);
            Assert.That(graph.GetDependencies(item1), Does.Contain(dep1));
            Assert.That(graph.GetDependents(dep1), Does.Contain(item1));

            // removing should fail with dependencies still in place
            Assert.That(graph.Remove(dep1), Is.False);

            // but succeed after we remove all dependents
            Assert.That(graph.Remove(item1), Is.True);
            Assert.That(graph.Remove(dep1), Is.True);
        }
    }
}