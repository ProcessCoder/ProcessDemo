using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProcessDemo.Commons.Models
{
    public class Farm
    {
        [Key]
        public int FarmId { get; set; }

        public string Name { get; set; }

        public ICollection<AppleTree> AppleTrees { get; set; }

        public Farm()
        {

        }
    }
}
