using Microsoft.Azure.Mobile.Server;
using Validation.Shared;

namespace Validation.Server.DataObjects
{
    public class TodoItem : EntityData, ITodoItem
    {
        public bool Complete { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
    }
}