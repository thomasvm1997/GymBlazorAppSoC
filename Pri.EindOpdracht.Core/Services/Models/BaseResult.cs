namespace Pri.EindOpdracht.Core.Services.Models
{
    public abstract class BaseResult
    {
        public bool Success => Errors.Count == 0;
        public List<string> Errors { get; set; } = new List<string>();
    }
}
