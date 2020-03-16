namespace SuncereDataCenter.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SuncerePermission")]
    public partial class SuncerePermission
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SuncerePermission()
        {
            SuncereRole = new HashSet<SuncereRole>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public int Type { get; set; }

        [StringLength(255)]
        public string Controller { get; set; }

        [StringLength(255)]
        public string Action { get; set; }

        public int Order { get; set; }

        public int ParentId { get; set; }

        [StringLength(255)]
        public string Icon { get; set; }

        public bool Status { get; set; }

        public bool Static { get; set; }

        public DateTime CreationTime { get; set; }

        public int? CreatorUserId { get; set; }

        public DateTime? LastModificationTime { get; set; }

        public int? LastModifierUserId { get; set; }

        [StringLength(255)]
        public string Remark { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SuncereRole> SuncereRole { get; set; }
    }
}
