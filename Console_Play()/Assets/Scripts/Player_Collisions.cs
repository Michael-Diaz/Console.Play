using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Collisions : MonoBehaviour
{
    private Player_Control flagSource;

    public int flagSelector;

    void Start()
    {
        flagSource = GameObject.Find("Player").GetComponent<Player_Control>();
    }

    void OnTriggerEnter2D(Collider2D bound)
    {
        if (bound.gameObject.tag == "Barrier")
        {
            switch (flagSelector)
            {
                case 0:
                    flagSource.wall = true;
                    break;
                case 1:
                    flagSource.floor = true;
                    break;
            }
        }
        
    }

    void OnTriggerExit2D(Collider2D bound)
    {
        if (bound.gameObject.tag == "Barrier")
        {
            switch (flagSelector)
            {
                case 0:
                    flagSource.wall = false;
                    break;
                case 1:
                    flagSource.floor = false;
                    break;
            } 
        }

    }
}
