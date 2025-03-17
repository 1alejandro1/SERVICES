using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.CROSS.MODELS.Generated
{
    public class ImageUploadResult
    {
        public ImageUploadResult(string url, string name)
        {
            this.Url = url;
            this.Name = name;
        }

        public string Url { get; }

        public string Name { get; }
    }
}
