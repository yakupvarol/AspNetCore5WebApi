using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebApi.Helper.ReturnMessage
{
    public class ReturnModel
    {
        public bool Success { get; set; }
        public int Code { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }
    }

    public class ReturnData : ReturnModel 
    {
        public ReturnData()
        {
            Success = true;
            Code = 200;
            Message = "Everthing is OK";
        }
        public dynamic Data { get; set; }
        public dynamic Count { get; set; }
    }


    public class ReturnData<T> : ReturnModel
    {
        public ReturnData()
        {
            Success = true;
            Code = 200;
            Message = "Everthing is OK";
        }
        public T Data { get; set; }
        public dynamic Count { get; set; }
    }

    public class ReturnList<T> : ReturnModel where T:class
    {
        public ReturnList()
        {
            Success = true;
            Code = 200;
            Message = "Everthing is OK";
        }
        public DataList<T> Data { get; set; }
    }

    public class DataList<T> where T : class
    {
        public int rowCount { get; set; }
        public int page { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string sort { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public dynamic itemCount { get; set; }
        public T items { get; set; }
    }


    public class ReturnNotFound : ReturnModel
    {
        public ReturnNotFound()
        {
            Success = false;
            Code = 404;
            Message = "Not Found";
        }
        public object Data { get; set; } = null;
    }


    public class ReturnError : ReturnModel
    {
        public ReturnError()
        {
            Success = false;
            Code = 500;
            Message = "An error has occurred in the system.";
        }
        public string InternalMessage { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<ReturnErrorModel> Errors { get; set; }
    }

    public class ReturnErrorModel
    {
        public string Key { get; set; }
        public string Message { get; set; }
        public string InternalMessage { get; set; }
    }
}
