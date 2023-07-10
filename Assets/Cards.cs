using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public int id;
    public string name;
    public string description;

    public Card(string _name, string _description)
    {
        name = _name; description = _description;

    }
}


public class Cards : MonoBehaviour
{
    public List<Card> cardsInHand;
    public int maxCardsInHand = 4;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < maxCardsInHand; i++)
        {
            cardsInHand.Add(new Card("card" + i, "description"));
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
