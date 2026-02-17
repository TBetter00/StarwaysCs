using UnityEngine;

public class CardAdder : MonoBehaviour
{
    public Card card;

    public void addCard()
    {
        Cardmanager cardmanager = FindAnyObjectByType<Cardmanager>();
        cardmanager.addCard(card);
    }
}
