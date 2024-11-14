using System.ComponentModel.DataAnnotations;
using static TechStore.Infrastructure.Constants.DataConstant.VideoCardConstants;


namespace TechStore.Infrastructure.Data.Models.AttributesClasses
{
    public class VideoCard
    {
        public VideoCard()
        {
            VideoCardLaptops = new HashSet<Laptop>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(VideoCardNameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Laptop> VideoCardLaptops { get; set; }
    }
}