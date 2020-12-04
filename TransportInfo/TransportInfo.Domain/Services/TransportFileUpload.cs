using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace TransportInfo.Domain.Services
{
    public class TransportFileUpload
    {
        public IFormFile image { get; set; }

        public string transport { get; set; }
    }
}
