using AutoMapper.Configuration.Annotations;

namespace MotoDev.Domain.Entities
{
    public class BaseModel
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastUpdatedAt { get; set; }

        public int? CreatedByUserId { get; set; }

        public int? LastUpdatedByUserId { get; set; }
    }
}