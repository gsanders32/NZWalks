namespace NZWalks.API.Models.DTO
{
    public class AddWalkRequest
    {
        public string Name { get; set; } = string.Empty;
        public double Length { get; set; }
        public Guid RegionId { get; set; }
        public Guid WalkDifficultyId { get; set; }
    }
}
