using System;
using static MagicVilla.Utility.SD;

namespace MagicVilla_Model
{
    public class APIRequest
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
    }
}
