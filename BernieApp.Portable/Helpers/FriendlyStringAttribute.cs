using System;

namespace BernieApp.Portable.Helpers
{
    public class FriendlyStringAttribute : Attribute {
        public FriendlyStringAttribute(string value)
        {
            this.Value = value;
        }

        public string Value { get; set; }
    }
}