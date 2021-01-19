using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects/Item")]
public class ItemSO : ScriptableObject
{
    [TextArea(3,5)]
    public string hint;
    public int level;
    public GameObject prefab;
    [TextArea(3,5)]
    public string question;
    public bool isBoolean;
    public bool isTrue;
    public string correctAnswer;
    public string[] choices;

    public string[] ShuffleChoices()
    {
        return Shuffle(choices);

    }

    private string[] Shuffle(string[] choices)
    {
        Random rnd = new Random();

        for (int i = choices.Length - 1; i > 0; i--)
        {
            int randomIndex = rnd.Next(0, i + 1);

            string temp = choices[i];
            choices[i] = choices[randomIndex];
            choices[randomIndex] = temp;
        }
        return choices;
    }
}
