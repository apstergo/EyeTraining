using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eyefit
{
    [Table("Answer")]
    public class Answers
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Answer_ID { get; set; }
        public int Proc { get; set; }
    }
}
