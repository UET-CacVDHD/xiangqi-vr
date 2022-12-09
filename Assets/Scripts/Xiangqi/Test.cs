using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Xiangqi.Parser;

public class Test : MonoBehaviour
{
    public Button testBtn;
    public Unity3DGameManager gameManager;
    public string input = "";

    private void Start()
    {
        gameManager = Unity3DGameManager.instance;
        PrintParse("std:M4.3");
        PrintParse("std:Mt4/2");
        PrintParse("std:M4.");
        PrintParse("std:Bs4");
        PrintParse("std:Ts4");
        PrintParse("std:Bt4.3");
        PrintParse("std:Bt4-");
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void PrintParse(string input)
    {
        var parser = CommandParser.Parser;
        var success = parser.TryParse(input, out var test);
        print($"Input: {input} Success: {success} Result: {test}");
    }

    public void TestButton()
    {
        var parser = CommandParser.Parser;
        var success = parser.TryParse(input, out var polyCommand);
        if (!success)
        {
            print($"Failed to parse '{input}'");
            return;
        }

        var stdCommand = polyCommand.GetStandardCommand();
        if (stdCommand == null)
        {
            print("Parsing failed");
            return;
        }

        print(stdCommand);

        var list = gameManager.gameSnapshot.ProcessStandardCommand(stdCommand);
        list.ForEach(
            cp => print($"{cp.ChessPiece.aCell} -> {cp.MovableCells.Aggregate("", (cur, cell) => cur + cell)}"));
    }
}