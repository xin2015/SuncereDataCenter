namespace SuncereDataCenter.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SuncereUser")]
    public partial class SuncereUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SuncereUser()
        {
            SuncereRole = new HashSet<SuncereRole>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string UserName { get; set; }

        [Required]
        [StringLength(255)]
        public string DisplayName { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [StringLength(255)]
        public string EmailAddress { get; set; }

        [StringLength(32)]
        public string PhoneNumber { get; set; }

        public bool Status { get; set; }

        public bool Static { get; set; }

        public DateTime? LastLoginTime { get; set; }

        [StringLength(255)]
        public string LastLoginIP { get; set; }

        [StringLength(255)]
        public string IP { get; set; }

        public bool EnableIPBinding { get; set; }

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
