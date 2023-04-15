using UniversityTool.Domain.Codes;
using UniversityTool.Domain.Messages.Base;

namespace UniversityTool.Domain.Messages
{
    public record TabsPanelMessage(object Item, UIOperationTypeCode TypeCode) : BaseMessage(TypeCode);
}
