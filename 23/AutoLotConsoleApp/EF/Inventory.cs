namespace AutoLotConsoleApp.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    //This is a data annotation.
    //This tells EF how to build tables and properties when EF generates the DB.
    //In addtion to it, this also tells EF how to map the data from DB and entities.
    [Table("Inventory")]
    public partial class Inventory
    {
        //This tells static analyzers such as FXCop, Roslyn cod anlyzer to turn off the rules which are listed in the constructor.
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Inventory()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        public int CarId { get; set; }

        [StringLength(50)]
        public string Make { get; set; }

        [StringLength(50)]
        public string Color { get; set; }

        [StringLength(50)]
        public string PetName { get; set; }

        //This class(Inventory) can have many Order objects.
        //one(Inventory)-to-many(Order) relationship.
        //Order table will has FK corresponding to Inventory table.
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
