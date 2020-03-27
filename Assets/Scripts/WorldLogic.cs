using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldLogic : MonoBehaviour
{
    public GameObject Player;
    Text outputText;
    int items = 0;

    void Start()
    {
        Player.GetComponent<CharacterPlayerControl>().OnPick += this.OnPick;
        outputText = GameObject.FindWithTag("Output").GetComponent<Text>();
        outputText.text = "0 / 10";
    }

    void OnPick()
    {
        items++;
        outputText.text = $"{items.ToString()} / 10";
        if (items == 10) {
            outputText.text += "\nQuest completed.";
        }
    }
}
