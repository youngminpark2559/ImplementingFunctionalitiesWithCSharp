using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AutoLotDAL.Models
{
    public partial class CreditRisk
    {
        [Key]
        public int CustId { get; set; }

        //c Add [Index] on FirstName, LastName in CreditRisk class, making and configuring composite key.
        //Make composite key by LastName+FirstName, and name it IDX_CreditRisk_Name, and set it unique.
        [StringLength(50)]
        [Index("IDX_CreditRisk_Name", IsUnique = true, Order = 2)]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Index("IDX_CreditRisk_Name", IsUnique = true, Order = 1)]
        public string LastName { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

    }


}