using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour
{
    bool walkingTemp = false;
    bool walkingInf = false;
    float walkTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (walkingInf || (walkingTemp && walkTimer > Time.time))
            transform.Translate(new Vector2(2 * transform.localScale.x, 0) * Time.deltaTime);
        else
        {
            walkingTemp = false;
            walkingInf = false;
        }
        

    }

    public string Jump(string[] line)
    {
        return "No Error";
    }

    public string Walk(string[] line) // Turn into "Move" func to allow running?
    {
        Debug.Log("Called Walk()");

        int lineLen = line.Length;
        if (lineLen > 2)
            return "<color=#ff3333ff>  > ERR: Extra Args</color>\n";

        if (lineLen == 1)
        {
            walkingInf = true;
            walkingTemp = false;
        }
        else
        {
            if (int.TryParse(line[1], out int n))
            {
                walkTimer = Time.time + n;
                walkingTemp = true;
                walkingInf = false;
            }
            else
                return "<color=#ff3333ff>  > ERR: Arg 2, !Int </color>\n";
        }

        return "No Error";
    }

    public string Turn(string[] line)
    {
        Debug.Log("Called Turn()");

        int lineLen = line.Length;
        if (lineLen > 1)
            return "<color=#ff3333ff>  > ERR: Extra Args</color>\n";

        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);

        return "No Error";
    }
}
