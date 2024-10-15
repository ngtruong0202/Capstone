using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Rarity")]

public class Rarity : ScriptableObject
{
    [SerializeField] private new string name;
    [SerializeField] private Color textColour = new Color(1, 1, 1, 1);

    public string Name  => name;
    public Color TextColour => textColour;
}
