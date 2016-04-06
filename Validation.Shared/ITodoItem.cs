namespace Validation.Shared
{
    public interface ITodoItem
    {
        bool Complete { get; set; }
        string Email { get; set; }
        string Text { get; set; }
    }
}