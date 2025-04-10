using System;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Indicadores
{
    public class IndicadorId : EntityId
    {
        public IndicadorId() : base() { }

        public IndicadorId(string value) : base(value) { }

        public IndicadorId(Guid value) : base(value) { }

        protected override object createFromString(string text)
        {
            return Guid.Parse(text); 
        }

        public override string AsString()
        {
            return Value.ToString();
        }

        public Guid AsGuid()
        {
            return Guid.Parse(Value.ToString()); 
        }
    }
}