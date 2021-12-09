using System;
using System.Collections.Generic;
using System.Text;

namespace BingoCard.Models
{
    public class BingoNumber
    {
        public int Value { get; set; }
        public bool IsNotChosen { get; set; } = true;

        public BingoNumber(int value)
        {
            Value = value;
        }
    }
}
