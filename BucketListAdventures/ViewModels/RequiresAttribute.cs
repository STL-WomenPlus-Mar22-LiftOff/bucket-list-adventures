namespace BucketListAdventures.ViewModels
{
    internal class RequiresAttribute : Attribute
    {
        public string ErrorMessage { get; set; }
    }
}