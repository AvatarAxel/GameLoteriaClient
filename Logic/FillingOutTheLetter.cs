﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class FillingOutTheLetter
    {
        public  List<int> FillDeck()
        {
            List<int> DeckCards = new List<int>();
            var random = new Random();
            int idCard;
            while (DeckCards.Count < 16)
            {
                idCard = random.Next(1, 55);
                if (!DeckCards.Contains(idCard))
                {
                    DeckCards.Add(idCard);
                }
            }
            return DeckCards;
        }
    }
}
