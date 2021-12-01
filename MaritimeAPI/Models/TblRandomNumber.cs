using System;
using System.Collections.Generic;

#nullable disable

namespace MaritimeAPI.Models
{
    public partial class TblRandomNumber
    {
        public int Id { get; set; }
        public decimal RndNumber { get; set; }

        public TblRandomNumber( decimal RndNumber)
        {
          //  this.Id= Id;
            this.RndNumber = RndNumber;
        }
        
    }
}
