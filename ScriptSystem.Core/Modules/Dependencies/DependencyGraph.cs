using System.Collections.Generic;
using System.Collections.Immutable;

namespace ScriptSystem.Core.Modules.Dependencies
{
    public class DependencyGraph<T>
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

        private IDictionary<T, DependencyNode> Nodes
            = new Dictionary<T, DependencyNode>();
    
        public void Add(T item)
        {
            if (!Nodes.ContainsKey(item))
                Nodes[item] = new DependencyNode();
        }

        public void Add(T item, T dependency)
        {
            Add(item);
            Add(dependency);
            Nodes[item].Dependencies.Add(dependency);
            Nodes[dependency].Dependents.Add(item);
        }

        /// <summary>
        /// Removes an object from the dependency graph.
        /// </summary>
        /// <returns>False if objects still depend on this object, otherwise True.</returns>
        public bool Remove(T item)
        {
            var node = Nodes[item];
            if (node.Dependents.Count != 0)
                return false;

            // remove dependent references from our dependencies
            foreach (var dependency in node.Dependencies)
                Nodes[dependency].Dependents.Remove(item);

            Nodes.Remove(item);
            return true;
        }

        /// <summary>
        /// Removes a dependency reference from an object.
        /// </summary>
        public void Remove(T item, T dependency)
        {
            Nodes[item].Dependencies.Remove(item);
            Nodes[dependency].Dependents.Remove(item);

            // if we have no more dependents, go ahead and GC ourselves
            if (Nodes[dependency].Dependents.Count == 0)
                Remove(dependency);
        }

        public bool Contains(T item)
        {
            return Nodes.ContainsKey(item);
        }

        public ImmutableHashSet<T> GetDependencies(T item)
        {
            return Nodes[item].Dependencies.ToImmutableHashSet();
        }

        public ImmutableHashSet<T> GetDependents(T item)
        {
            return Nodes[item].Dependents.ToImmutableHashSet();
        }
    }
}