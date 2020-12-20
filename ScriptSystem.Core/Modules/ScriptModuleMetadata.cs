using System.Collections.Generic;

namespace ScriptSystem.Core.Modules
{
    /// <summary>
    /// Structure that defines metadata about a script module.
    /// </summary>
    public class ScriptModuleMetadata
    {
        /// <summary>
        /// The pretty title of the module.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The description of the module.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The license of the module (MIT, GPL, etc).
        /// </summary>
        public string License { get; set; }

        /// <summary>
        /// List of people who created the module.
        /// </summary>
        public IList<string> Authors { get; set; }

        /// <summary>
        /// Fully qualified names of modules this module depends on.
        /// </summary>
        public IList<string> Dependencies { get; set; }
    }
}