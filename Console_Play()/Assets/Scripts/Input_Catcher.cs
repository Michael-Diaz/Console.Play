using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Input_Catcher : MonoBehaviour
{
    private InputField inputField;
    private Text inputText;
    private Text logs;

    private Player_Control commands;
    private Dictionary<string, System.Func<string[], string>> funcList = new Dictionary<string, System.Func<string[], string>>();

    // Start is called before the first frame update
    void Start()
    {
        // Initialize all the objects
        inputField = GameObject.Find("InputField").GetComponent<InputField>();
        inputText = inputField.transform.GetChild(1).GetComponent<Text>();
        logs = GameObject.Find("LogView").transform.GetChild(0).GetComponent<Text>();

        commands = GameObject.Find("Player").GetComponent<Player_Control>();

        // Add the proper command/function pairs from Player_Control.cs to the dictionary
        funcList.Add("walk", commands.Walk);
        funcList.Add("turn", commands.Turn);
    }

    // Update is called once per frame
    void Update()
    {
        string baseLine = "> ";
        string playerInput;

        // Player presses enter, input field isn't blank
        if(Input.GetKeyDown(KeyCode.Return) && !inputText.text.Equals(""))
		{
            // Grab the input from the 'console' and clear the field
            playerInput = inputText.text;
            inputText.text = "";

            // Run the the command given, store any error messages returned from respective functions & remove errorMsg if no error
            string errorMsg = RunCommands(playerInput);
            if (errorMsg.Equals("No Error"))
                errorMsg = "";
            
            // Generate the line to be printed to the command logs
            baseLine = baseLine + playerInput + "\n";
            logs.text = baseLine + errorMsg + logs.text;
        }
    }

    string RunCommands(string inputCommand)
    {
        // Remove whitespace from input string
        string[] commandParse = inputCommand.ToLower().Split(' ');

        // Check to see if the first word of the input pertains to an exisiting command, otherwise return an error
        System.Func<string[], string> temp = null;
        if (funcList.TryGetValue(commandParse[0], out temp))
        {
            return funcList[commandParse[0]](commandParse);
        } 
        else
            return "<color=#ff3333ff>  > ERR: ? Cmd</color>\n";
    }
}
