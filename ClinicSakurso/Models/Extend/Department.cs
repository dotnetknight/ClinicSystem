using System.ComponentModel.DataAnnotations;

namespace ClinicSakurso.Models
{
    [MetadataType(typeof(DepartmentMetaData))]
    public partial class Department { }

    public class DepartmentMetaData
    {
        [Required(AllowEmptyStrings =false,ErrorMessage ="დასახელება სავალდებულოა")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "თანამშრომელთა რაოდენობა სავალდებულოა")]
        public int Workers { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "ოთახების რაოდენობა სავალდებულოა")]
        public int Rooms { get; set; }
    }
}