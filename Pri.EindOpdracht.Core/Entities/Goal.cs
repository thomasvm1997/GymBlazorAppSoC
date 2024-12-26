namespace Pri.EindOpdracht.Core.Entities
{
    public class Goal
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime TargetDate { get; set; }
        public bool Achieved { get; set; }

        public string UserId { get; set; } // Foreign Key
        public ApplicationUser User { get; set; } //Vagigatie property om gemakkelijk queries uit te voeren
    }
}
