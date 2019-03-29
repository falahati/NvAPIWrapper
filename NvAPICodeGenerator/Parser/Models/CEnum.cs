using System;

namespace NvAPICodeGenerator.Parser.Models
{
    internal class CEnum : CTree
    {
        public CEnum(string name, CTree parentTree) : base(name, parentTree)
        {
        }

        /// <inheritdoc />
        public override void AddSubTree(CTree subTree)
        {
            if (!(subTree is CEnumValue))
            {
                throw new ArgumentException("CEnum only accepts subtrees of type CEnumValue.", nameof(subTree));
            }

            base.AddSubTree(subTree);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"ENUM {Name}";
        }
    }
}