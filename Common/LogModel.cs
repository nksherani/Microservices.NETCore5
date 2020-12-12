using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class LogModel
    {
        public int Level { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }
        public DateTime Timestamp { get; set; }
        public int CreatedBy { get; set; }
    }
}
