using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using MyNewApp.Shared.Models.Helpers;

namespace MyNewApp.Shared.Models
{
    public class WochenTag
    {
        public string Name { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public TimeSpan Pause { get; set; }
    }
}
