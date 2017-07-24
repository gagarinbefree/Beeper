namespace ContactListMvc.Models.Repository.EF.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class personattributes
    {
        public int id { get; set; }

        public int idperson { get; set; }

        public int idattribute { get; set; }

        [StringLength(255)]
        public string val { get; set; }

        public virtual attributes attributes { get; set; }

        public virtual persons persons { get; set; }
    }
}
