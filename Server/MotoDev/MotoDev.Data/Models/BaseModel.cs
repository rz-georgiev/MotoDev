namespace EngineExpert.Data.Models
{
    public class BaseModel
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? LastUpdatedAt { get; set; }

        public int CreatedByUserId { get; set; }

        public int? LastUpdatedByUserId { get; set; }
    }
}