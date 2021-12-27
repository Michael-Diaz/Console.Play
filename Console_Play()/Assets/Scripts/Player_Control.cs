using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour
{
    Rigidbody2D rb;

    bool walkingTemp = false;
    bool walkingInf = false;
    float walkTimer = 0.0f;

    public bool wall = false;
    public bool floor = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float turnDir = transform.localScale.x;

        if ((walkingInf || (walkingTemp && walkTimer > Time.time)) && !wall)
            transform.Translate(new Vector2(2 * turnDir, 0) * Time.deltaTime);
        else
        {
            walkingTemp = false;
            walkingInf = false;
        }

    }

    public string Jump(string[] line)
    {
        Debug.Log("Called Walk()");

        int lineLen = line.Length;
        if (lineLen > 1)
            return "<color=#ff3333ff>  > ERR: Extra Args</color>\n";

        if (!floor)
            return "<color=#ffcc33ff>  > ERR: Airborne</color>\n";

        rb.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);

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
            if (!wall)
            {
                walkingInf = true;
                walkingTemp = false;
            }
            else
                return "<color=#ffcc33ff>  > ERR: Wall Ahead </color>\n";
        }
        else
        {
            if (int.TryParse(line[1], out int n))
            {
                if (!wall)
                {
                    walkTimer = Time.time + n;
                    walkingTemp = true;
                    walkingInf = false;
                }
                else
                    return "<color=#ffcc33ff>  > ERR: Wall Ahead </color>\n";
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
