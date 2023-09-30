
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Application.DTOs
{
    public class SuccessModelDTO
    {
        public int StatusCode { get; set; }
        public string SuccessMessage { get; set; }
        public object Data { get; set; }
    }
}
