using AnilShop.BusinessLayer.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnilShop.BusinessLayer.Result
{
    public class BusinessLayerResult<T> where T : class
    {
        public List<ErrorMessagesObj> Errors { get; set; }

        public T result { get; set; }

        public BusinessLayerResult()
        {
            Errors = new List<ErrorMessagesObj>();
        }

        public void AddError(ErrorMessageCode code, string message)
        {
            Errors.Add(new ErrorMessagesObj() { Code = code, Message = message });
        }
    }


}
