namespace CreateTest.Lib.model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Questions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Questions()
        {
            Answers = new HashSet<Answers>();
        }

        [Key]
        public int QuestionId { get; set; }

        [Required]
        [StringLength(255)]
        public string QuestionContent { get; set; }

        public int SectionId { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreationDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Answers> Answers { get; set; }

        public virtual Sections Sections { get; set; }
    }
}
