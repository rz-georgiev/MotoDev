namespace MotoDev.Common.Dtos
{
    public class MechanicDetailUpdateRequest
    {
        public int RepairDetailId { get; set; }

        public int NewStatusId { get; set; }

        public string NewNotes { get; set; }

    }
}