using System;

namespace CoderCamp.Models
{
    public class Event
    {
        string _date;

        /// <summary>
        /// A unique identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the event
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The date of the event
        /// </summary>
        public string Date {
            get { return _date; }
            set
            {
                DateTime dt;
                if (DateTime.TryParse(value, out dt))
                    _date = dt.ToUniversalTime().ToString("D");
                else
                    _date = value;
            }
        }

        /// <summary>
        /// The description of the event
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The URL for the event
        /// </summary>
        public string Link { get; set; }
    }
}
