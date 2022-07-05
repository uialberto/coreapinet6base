using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uibasoft.BaseLab.Domain.CustomEntities
{
    public class ApiResponse
    {
        public int Status { get; set; }
        public string Title { get; set; }
        public IEnumerable<BaseErrorMessage> Errors { get; set; } = new List<BaseErrorMessage>();
        public string Type { get; set; }
        public ApiResponse()
        {
        }
        public object Data { get; set; }
    }
}
