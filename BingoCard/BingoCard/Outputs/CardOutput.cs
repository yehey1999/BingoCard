using BingoCard.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BingoCard.Outputs
{
    class CardOutput
    {
        public string playcard_token 
        { 
            get; 
            set; 
        }

        public Card card
        {
            get;
            set;
        }
    }
}
