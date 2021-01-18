using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemSO : ScriptableObject
{
    [TextArea(3,5)]
    public string text;

    public string question;
    public string correctAnswer;
    public string[] choices;

    public void ShuffleChoices()
    {
        choices = Shuffle(choices);
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
