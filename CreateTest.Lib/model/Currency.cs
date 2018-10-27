namespace CreateTest.Lib.model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Currency")]
    public partial class Currency
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string CurrencyName { get; set; }

        public DateTime? Datess { get; set; }

        [StringLength(50)]
        public string Price { get; set; }
    }
}
