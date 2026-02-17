using UnityEngine;
using UnityEngine.UI;

public class Cardmanager : MonoBehaviour
{
    public Image cardImage;
    public Text cardName;
    public Text cardDescription;
    public Card card; // object วัตถุ

    public void addCard(Card cardSO)
    {
        this.cardImage.sprite = cardSO.cardImage;
        this.cardName.text = cardSO.name;
        this.cardDescription.text = cardSO.description;
    }
}
