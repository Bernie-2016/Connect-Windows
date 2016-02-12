using System;

namespace BernieApp.Common.Helpers
{
    public class FriendlyStringAttribute : Attribute {
        public FriendlyStringAttribute(string value)
        {
            this.Value = value;
        }

        public string Value { get; set; }
    }
}