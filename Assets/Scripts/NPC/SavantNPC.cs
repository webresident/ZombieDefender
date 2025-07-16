using System.Collections;
using TMPro;
using UnityEngine;

public class SavantNPC : NPC
{
    
    public override void Interact(Transform player)
    {
        base.Interact(player);
        ShowIteractions();
    }

    public void ShowIteractions()
    {
        container.SetActive(true);
        FillText(data.conversation[0].answer);
    }
}
