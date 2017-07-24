namespace ContactListMvc.Models.Repository.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class persons
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public persons()
        {
            personattributes = new HashSet<personattributes>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string lastname { get; set; }

        [Required]
        [StringLength(255)]
        public string firstname { get; set; }

        [StringLength(255)]
        public string middlename { get; set; }

        public int? sex { get; set; }

        [Column(TypeName = "date")]
        public DateTime? birthday { get; set; }

        public int? idcity { get; set; }

        public int? idcategory { get; set; }

        public int isvalid { get; set; }

        public virtual categories categories { get; set; }

        public virtual cities cities { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<personattributes> personattributes { get; set; }
    }
}
