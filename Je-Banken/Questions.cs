using System.Collections.Generic;

namespace Je_Banken
{
    public class Questions
    {
        public string Title { get; set; } = "";
        public string Subject { get; set; } = "";
        public string Type { get; set; } = "";
        public string Picture { get; set; } = "";
        public List<Option> Options { get; set; } = new List<Option>();
    }
}