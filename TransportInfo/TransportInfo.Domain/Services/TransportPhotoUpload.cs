using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace TransportInfo.Domain.Services
{
    public class TransportPhotoUpload
    {
        public IFormFile image { get; set; }
        
    }
}
