using System;
using System.Text.Json.Serialization;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.AfetacaoPerfis
{
    public class AfetacaoPerfilId : EntityId
    {
        
        [JsonConstructor]
        public AfetacaoPerfilId(Guid value) : base(value)
        {
        }
        
        public AfetacaoPerfilId(String value):base(value)
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