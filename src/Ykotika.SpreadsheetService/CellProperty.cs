namespace Ykotika.SpreadsheetService
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CellProperty(bool isHyperLink = false) : Attribute
    {
        public bool IsHyperLink { get; } = isHyperLink;
    }
}
