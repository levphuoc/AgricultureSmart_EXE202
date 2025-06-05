using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Models.TicketModels
{
    public class TicketStatusConstants
    {
        public const string Open = "open";
        public const string Assigned = "assigned";
        public const string InProgress = "in_progress";
        public const string Resolved = "resolved";
        public const string Closed = "closed";

        public static readonly List<string> ValidStatuses = new()
        {
            Open, Assigned, InProgress, Resolved, Closed
        };

        public static readonly Dictionary<string, List<string>> ValidTransitions = new()
        {
            { Open, new List<string> { Assigned, Closed } },
            { Assigned, new List<string> { InProgress, Open, Closed } },
            { InProgress, new List<string> { Resolved, Assigned, Closed } },
            { Resolved, new List<string> { Closed, InProgress } },
            { Closed, new List<string>() } // No transitions from closed
        };
    }
}
