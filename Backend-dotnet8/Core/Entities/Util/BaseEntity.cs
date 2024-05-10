namespace Backend_dotnet8.Core.Entities.Util
{
    public class BaseEntity<TID>
    {
        public TID Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; } = DateTime.Now;
        public DateTime DeleteAt { get; set; } = DateTime.Now;

        public bool Estate { get; set; } = true;

    }
}
