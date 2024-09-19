using System.ComponentModel.DataAnnotations;

namespace MotoDev.Common.Enums
{
    public enum RepairStatusOption
    {
        [Display(Name = "To be started")]
        ToDo = 1,

        [Display(Name = "In Progress")]
        InProgress = 2,

        [Display(Name = "Done")]
        Done = 3,
    }
}