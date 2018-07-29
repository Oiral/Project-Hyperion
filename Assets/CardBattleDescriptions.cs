using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card Description", menuName = "Cards/Create Card Description", order = 1)]
public class CardBattleDescriptions : ScriptableObject {
    
    [Multiline]
    public string Description;
}
