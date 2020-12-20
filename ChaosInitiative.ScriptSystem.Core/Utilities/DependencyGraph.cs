using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace ChaosInitiative.ScriptSystem.Core.Utilities
{
    public class DependencyGraph<T> : IEnumerable<T>
    {
        class DependencyNode
        {
            public readonly HashSet<T> Dependents;
            public readonly HashSet<T> Dependencies;

            public DependencyNode()
            {
                Dependents = new HashSet<T>();
                Dependencies = new HashSet<T>();
            }
        }

        private readonly IDictionary<T, DependencyNode> _nodes
            = new Dictionary<T, DependencyNode>();
    
        public void Add(T item)
        {
            if (!_nodes.ContainsKey(item))
                _nodes[item] = new DependencyNode();
        }

        public void Add(T item, T dependency)
        {
            Add(item);
            Add(dependency);
            _nodes[item].Dependencies.Add(dependency);
            _nodes[dependency].Dependents.Add(item);
        }

        /// <summary>
        /// Removes an object from the dependency graph.
        /// </summary>
        /// <returns>False if objects still depend on this object, otherwise True.</returns>
        public bool Remove(T item)
        {
            var node = _nodes[item];
            if (node.Dependents.Count != 0)
                return false;

            // remove dependent references from our dependencies
            foreach (var dependency in node.Dependencies)
                _nodes[dependency].Dependents.Remove(item);

            _nodes.Remove(item);
            return true;
        }

        /// <summary>
        /// Removes a dependency reference from an object.
        /// </summary>
        public void Remove(T item, T dependency)
        {
            _nodes[item].Dependencies.Remove(item);
            _nodes[dependency].Dependents.Remove(item);

            // if we have no more dependents, go ahead and GC ourselves
            if (_nodes[dependency].Dependents.Count == 0)
                Remove(dependency);
        }

        public bool Contains(T item)
        {
            return _nodes.ContainsKey(item);
        }

        public ImmutableHashSet<T> GetDependencies(T item)
        {
            return _nodes[item].Dependencies.ToImmutableHashSet();
        }

        public ImmutableHashSet<T> GetDependents(T item)
        {
            return _nodes[item].Dependents.ToImmutableHashSet();
        }

        public IEnumerator<T> GetEnumerator() => _nodes.Keys.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}