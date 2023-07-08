using System.ComponentModel.DataAnnotations;

namespace SAQAYA.EntityFramework.Common
{
    public class BaseEntity
    {
        [Key]
        public string ID { get; set; }
    }

}
