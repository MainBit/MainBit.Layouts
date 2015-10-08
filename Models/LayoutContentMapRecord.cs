using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainBit.Layouts.Models
{
    public class LayoutContentMapRecord
    {
        public virtual int Id { get; set; }
        public virtual int LayoutPartRecord_Id { get; set; }
        public virtual int ContentItemRecord_Id { get; set; }
    }
}