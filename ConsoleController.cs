using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class ConsoleController : MonoBehaviour
{
    private bool showConsole;
    private string input;
    private Vector2 scroll;

    private Dictionary<string, MethodInfo> availableCommands;
    private List<string> log = new List<string>();

    private void Start()
    {
        showConsole = false;
        input = string.Empty;

        availableCommands = new Dictionary<string, MethodInfo>();

        // Zbieramy wszystkie publiczne metody z każdego MonoBehaviour w scenie
        var allMonoBehaviours = FindObjectsOfType<MonoBehaviour>();
        foreach (var mb in allMonoBehaviours)
        {
            var methods = mb.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            foreach (var method in methods)
            {
                availableCommands[method.Name.ToLower()] = method;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            ToggleConsole();
        }

        if (showConsole && Input.GetKeyDown(KeyCode.Tab))
        {
            Autocomplete();
        }

        if (showConsole && Input.GetKeyDown(KeyCode.Return))
        {
            ExecuteCommand();
        }
    }

    private void ToggleConsole()
    {
        showConsole = !showConsole;
        if (showConsole)
        {
            log.Add("Console opened.");
        }
        else
        {
            log.Add("Console closed.");
        }
    }

    private void Autocomplete()
    {
        string lowerInput = input.ToLower();
        var matchingCommands = availableCommands.Keys.Where(cmd => cmd.StartsWith(lowerInput)).ToList();

        if (matchingCommands.Count == 1)
        {
            input = matchingCommands[0];
        }
        else if (matchingCommands.Count > 1)
        {
            log.Add("Possible commands:\n" + string.Join("\n", matchingCommands));
        }
    }

    private void ExecuteCommand()
    {
        string lowerInput = input.ToLower();
        if (availableCommands.TryGetValue(lowerInput, out MethodInfo method))
        {
            var declaringType = method.DeclaringType;
            var instance = FindObjectsOfType<MonoBehaviour>().FirstOrDefault(mb => mb.GetType() == declaringType);
            method.Invoke(instance, null);
            log.Add($"Executed command: {lowerInput}");
        }
        else
        {
            log.Add($"Command not found: {lowerInput}");
        }

        input = string.Empty;
    }

    private void OnGUI()
    {
        if (!showConsole) return;

        float y = 0;
        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.SetNextControlName("ConsoleInput");
        input = GUI.TextField(new Rect(10, y + 5, Screen.width - 20, 20), input);
        GUI.FocusControl("ConsoleInput");

        y += 30;

        GUI.Box(new Rect(0, y, Screen.width, Screen.height - y), "");

        scroll = GUI.BeginScrollView(new Rect(0, y, Screen.width, Screen.height - y), scroll, new Rect(0, 0, Screen.width - 20, log.Count * 20));

        for (int i = 0; i < log.Count; i++)
        {
            GUI.Label(new Rect(10, i * 20, Screen.width - 30, 20), log[i]);
        }

        GUI.EndScrollView();
    }

    // Przykładowe publiczne metody, które można wywołać z konsoli
    public void TestCommand()
    {
        Debug.Log("TestCommand executed!");
        log.Add("TestCommand executed!");
    }

    public void AnotherCommand()
    {
        Debug.Log("AnotherCommand executed!");
        log.Add("AnotherCommand executed!");
    }
}
