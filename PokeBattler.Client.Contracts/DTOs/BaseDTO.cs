using System;
using System.Collections.Generic;
using System.Text;

namespace AutoChess.Contracts.DTOs
{
    public abstract class BaseDTO
    {
        public Guid Id { get; set; }
        public ErrorDTO? Error { get; set; }
    }
}
