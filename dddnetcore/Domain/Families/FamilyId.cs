using System;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Families
{
    public class FamilyId : EntityId
    {
        public FamilyId(string value) : base(value) {}

        protected override object createFromString(string text)
        {
            return text;
        }

        public override string AsString()
        {
            return (string)base.Value;
        }
    }
}