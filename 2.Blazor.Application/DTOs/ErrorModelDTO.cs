﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Application.DTOs
{
    public class ErrorModelDTO
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
