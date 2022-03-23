using NHibernate.Mapping.Attributes;
using System;

namespace miauto.identity.api.Models.Entity
{
    [Serializable]
    [Class(0, Schema = "public", Table = "user", NameType = typeof(User))]
    public class User
    {
        [Id(Name ="id", Column ="id", Type ="int"), Generator(1,Class ="identity")]
        public virtual int id { get; set; }

        [Property(Column = "email",Type ="string",NotNull =true,Unique =true)]
        public virtual String email { get; set; }

        [Property(Column = "password", Type = "string", NotNull = true, Unique = true, Length =256)]
        public virtual String password { get; set; }

    }
}
