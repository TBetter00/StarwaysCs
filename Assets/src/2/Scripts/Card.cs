using UnityEngine.UI;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "card")]
public class Card : ScriptableObject
{
    public Sprite cardImage;
    public string name;
    public string description;
}
