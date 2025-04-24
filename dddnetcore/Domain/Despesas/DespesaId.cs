using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Despesas
{
    public class DespesaId : EntityId
    {
        [JsonConstructor]
        public DespesaId(Guid value) : base(value)
        {
        }

        public DespesaId(String value):base(value)
        {
        }

        override
        protected Object createFromString(String text){
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