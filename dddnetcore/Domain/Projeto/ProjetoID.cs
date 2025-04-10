using System;
using DDDSample1.Domain.Shared;


namespace dddnetcore.Domain.Projetos
{
    public class ProjetoId : EntityId
    {
        public ProjetoId(Guid value) : base(value) { }

        public ProjetoId(string value) : base(value) { }

        protected override object createFromString(string text)
        {
            return new Guid(text);
        }

        public override string AsString()
        {
            return ((Guid)ObjValue).ToString();
        }

        public Guid AsGuid()
        {
            return (Guid)ObjValue;
        }
    }
}