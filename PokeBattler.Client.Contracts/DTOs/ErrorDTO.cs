using System;
using System.Collections.Generic;
using System.Text;

namespace AutoChess.Contracts.DTOs
{
    public class ErrorDTO
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}
