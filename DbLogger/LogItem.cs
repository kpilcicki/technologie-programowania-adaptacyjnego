using System;
using System.ComponentModel.DataAnnotations;

namespace DbLogger
{
    public class LogItem
    {
        [Key]
        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime Time { get; set; }
    }
}
