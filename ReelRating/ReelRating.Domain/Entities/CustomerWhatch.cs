using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Domain.Entities
{
    public class CustomerWhatch
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int? QttAccess { get; set; }
        public int WhatchId { get; set; }
    }
}
