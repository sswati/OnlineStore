using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineStore.Models
{
    public class PropertyAndValue
    {
        public int Id { get; set; }
        public virtual Property Property { get; set; }
        public string Value { get; set; }
    }
}