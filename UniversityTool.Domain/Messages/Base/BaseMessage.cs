﻿using UniversityTool.Domain.Codes;

namespace UniversityTool.Domain.Messages.Base
{
    public abstract record BaseMessage(OperationTypeCode OperationType);
}
