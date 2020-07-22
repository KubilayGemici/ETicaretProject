using System;
using System.Collections.Generic;
using System.Text;

namespace BilgeAdamBitirmeProjesi.Common.Client.Models
{
    public class WebApiResponse<T>
    {
        public WebApiResponse()
        {

        }
        public WebApiResponse(string resultMassage, bool isSuccess)
        {
            ResultMessage = resultMassage;
            IsSuccess = isSuccess;
        }
        public WebApiResponse(string resultMassage, bool isSuccess,T resultData)
        {
            ResultMessage = resultMassage;
            IsSuccess = isSuccess;
            ResultData = resultData;
        }
        public long ElapsedMilliSecond { get; set; }
        public string ResultMessage { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string> ValidationErrors { get; set; }
        public T ResultData { get; set; }

    }
}
