using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "cardLibrary.asset", menuName = "cry/cardList", order = 1)]
public class CardLibrary : ScriptableObject {
    public List<Card> brightCardList1;
    // public List<CardBuild> darkCardList1;
    public Card card;
    // public CardBuild card2;
}