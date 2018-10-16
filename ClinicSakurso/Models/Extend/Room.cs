using System.ComponentModel.DataAnnotations;

namespace ClinicSakurso.Models
{
    [MetadataType(typeof(RoomMetaData))]
    public partial class Room { }

    public class RoomMetaData
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "გთხოვთ მიუთითოთ სართული")]
        public int Floor { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "გთხოვთ მიუთითოთ ბლოკი")]
        public int Block { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "გთხოვთ მიუთითოთ განყოფილება")]
        public string Department { get; set; }
    }
}