using System.Collections.Generic;

namespace NvAPICodeGenerator.Parser.Models
{
    internal class CTree
    {
        private readonly List<CTree> _subTrees = new List<CTree>();

        public CTree(string name, CTree parent)
        {
            Name = name;
            Parent = parent;
        }

        public string Name { get; }

        public CTree Parent { get; }

        public CTree[] SubTrees
        {
            get => _subTrees.ToArray();
        }

        public virtual void AddSubTree(CTree subTree)
        {
            _subTrees.Add(subTree);
        }
    }
}