using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Application.Common
{

    public class ApiResponse<TData>
    {
        public TData Data { get; set; }
        public bool Succeeded { get; set; } = false;
        public List<string> Errors { get; set; } = new List<string>();
        public List<string> Warnings { get; set; } = new List<string>();
        public ErrorType? ErrorType { get; set; }
        //public ErrorCode? ErrorCode { get; set; }

    }
    public enum ErrorType
    {
        LogicalError,
        SystemError,
        NotFound,
        Warning
    }
}
