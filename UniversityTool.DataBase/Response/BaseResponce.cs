using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityTool.Domain.Codes;
using UniversityTool.Domain.Response;

namespace UniversityTool.DataBase.Response
{
    internal class BaseResponce<T> : IBaseResponse<T>
    {
        public string Description { get; set; }
        public StatusCode StatusCode { get; set; }
        public T Data { get; set; }
    }
}
