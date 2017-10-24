using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AutoLotDAL.Models
{
    [Table("Inventory")]
    public partial class Inventory
    {
        [Key]
        public int CarId { get; set; }

        [StringLength(50)]
        public string Make { get; set; }

        [StringLength(50)]
        public string Color { get; set; }

        [StringLength(50)]
        public string PetName { get; set; }

        //c Add Timestamp property in each entity class(Inventory, Customer, Order, CreditRisk)
        //c Add explanation comment for Timestamp, related to concurrency. 
        //Timestamp is needed when the clients requested changes to the app such as value change, concurrency checking...
        //This is mapped to RowVersion data type in database because Timestamp is represented by byte[] data type.
        //Timestamp is added to WHERE clause in query in deleting record based on Id, preventing concurrency from being occured by examining order of request via Timestamp
        [Timestamp]
        public byte[] Timestamp { get; set; }

        //virtual for LazyLoading.
        //Get an Inventory object, within this object I get Orders property, returns ICollection<Order>, I access to Order table, and navigate Order table.
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}