using System;
using System.Text.Json.Serialization;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.ConsumoPerfis
{
    public class ConsumoPerfilId : EntityId
    {
        
        [JsonConstructor]
        public ConsumoPerfilId(Guid value) : base(value)
        {
        }
        
        public ConsumoPerfilId(String value):base(value)
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