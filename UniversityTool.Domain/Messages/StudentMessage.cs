using UniversityTool.Domain.Codes;
using UniversityTool.Domain.Messages.Base;
using UniversityTool.Domain.Models;

namespace UniversityTool.Domain.Messages
{
    public record StudentMessage(Student Student, UIOperationTypeCode OperationType) : BaseMessage(OperationType);
}
