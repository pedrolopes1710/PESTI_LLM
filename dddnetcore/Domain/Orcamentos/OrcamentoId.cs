using System;
using System.Text.Json.Serialization;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Orcamentos
{
    public class OrcamentoId : EntityId
    {
        
        [JsonConstructor]
        public OrcamentoId(Guid value) : base(value)
        {
        }
        
        public OrcamentoId(String value):base(value)
        {
        }

        override
        protected  Object createFromString(String text){
            return new Guid(text);
        }
        
        override
        public String AsString(){
            Guid obj = (Guid) base.ObjValue;
            return obj.ToString();
        }
        
        public Guid AsGuid(){
            return (Guid) base.ObjValue;
        }
    }
}