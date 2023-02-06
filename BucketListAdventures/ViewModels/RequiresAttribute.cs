namespace BucketListAdventures.ViewModels
{
    [AttributeUsage(AttributeTargets.All)]
    internal class RequiresAttribute : Attribute
    {
        public string ErrorMessage { get; set; }
    }
}