namespace StudioTG.Infrastructure.Common
{
    public class FieldOptions
    {
        public static string SectionName = nameof(FieldOptions);
        public required int MaxWidth { get; set; }
        public required int MaxHeight { get; set; }
    }
}
