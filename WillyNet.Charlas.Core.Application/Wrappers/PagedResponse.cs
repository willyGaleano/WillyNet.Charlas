using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillyNet.Charlas.Core.Application.Wrappers
{
    public class PagedResponse<T> : Response<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }

        public PagedResponse(T data, int pageNumber, int pageSize, int total, string message = null)
        {
            this.Total = total;
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Message = message;
            this.Succeeded = true;
            this.Errors = null;
            this.Data = data;
        }
    }
}
