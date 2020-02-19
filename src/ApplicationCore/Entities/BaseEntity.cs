using System;
using System.Collections.Generic;
using System.Text;

namespace CoreTemplate.ApplicationCore.Entities
{
    public abstract class BaseEntity<T>
    {
        public virtual T Id { get; protected set; }
    }
}
