
using UnityEngine;

public class Hacker : MonoBehaviour
{
    //Game configuration data
    const string menuHint = "You may type meny at any time.";
    string[] level1Passwords = {"books", "aisle", "self", "password", "font", "borrow"};
    string[] level2Passwords = {"prisoner", "handcuffs", "holster", "uniform", "arrest"};
    string[] level3Passwords = { "starfield", "telescope", "environment", "exploration", "astronauts" };

    //GameState
    int level;
    enum Screen   {MainMenu, Password, Win};
    Screen currentScreen = Screen.MainMenu;
    string password;

    // Start is called before the first frame update
    void Start()
    {
        ShowMainMenu();
    }//TODO HANDLE DIFFERENTLY DEPENDING ON SCREEN

    void ShowMainMenu()
    {
        Terminal.ClearScreen();
        print("Hello Console");
        Terminal.WriteLine("Hello Pablo");
        Terminal.WriteLine("  ");
        Terminal.WriteLine("Press 1 for the local library");
        Terminal.WriteLine("Press 2 for the police station");
        Terminal.WriteLine("Press 3 for NASA");
        Terminal.WriteLine("  ");
        Terminal.WriteLine("Choose your option now.");
    }

    //this should only decide who to handle input, not other things
    void OnUserInput(string input)
    {
        if (input == "menu")
        {
            ShowMainMenu();
        }
        else if (input == "exit" || input == "close" || input == "quit")
        {
            Terminal.WriteLine("This console is gona destroy itself");
            Application.Quit();
        }
        else if (currentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }
        else if (currentScreen == Screen.Password)
        {
            CheckPassword(input);
        }

    }

    void RunMainMenu(string input)
    {
        bool isValidLevelNumber = (input == "1" || input == "2" || input == "3");
        if (isValidLevelNumber)
        {
            level = int.Parse(input);
            AskForPassword();
        }
        else if (input == "007")
        {
            Terminal.WriteLine("what do you newed mr Pablo");
        }
        else
        {
            Terminal.WriteLine("Please choose a valir level");
            Terminal.WriteLine(menuHint);
        }
    }


    void AskForPassword()
    {
        currentScreen = Screen.Password;
        Terminal.ClearScreen();
        SetRandomPassword();

        Terminal.WriteLine("Please enter your password, hint:" + password.Anagram());
    }

    void SetRandomPassword()
    {
        switch (level)
        {
            case 1:
                password = level1Passwords[Random.Range(0, level1Passwords.Length)];
                break;
            case 2:
                password = level2Passwords[Random.Range(0, level2Passwords.Length)];
                break;
            case 3:
                password = level3Passwords[Random.Range(0, level3Passwords.Length)];
                break;
            default:
                Debug.LogError("invalid level number");
                break;
        }
    }

    void CheckPassword(string input)
    {
        if (input == password)
        {
            DisplayWinScreen();
        }
        else
        {
            AskForPassword();
        }
    }

    void DisplayWinScreen()
    {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        ShowLevelReward();
    }

    void ShowLevelReward()
    {
        switch (level)
        {
            case 1:
                Terminal.WriteLine("Have a book ....");
                Terminal.WriteLine(@"
        ___________
       /         //
      /         //
     /         //
    /________ //
   (_________(/
"  );
                break;
            case 2:
                Terminal.WriteLine("YOU GOT THE PRISON KEY");
                Terminal.WriteLine(@"
 __
/0 \__________
\__/-=' = ' = 

");

                Terminal.WriteLine("Play again for a greater challenge");
                break;
            case 3:
                Terminal.WriteLine(@"                                          
  __   __ _ ___  __ _ 
| '_ \ / _` / __|/ _` |
| | | | (_| \__ \ (_| |
|_| |_|\__,_|___/\__,_|
                       
");
                Terminal.WriteLine("Welcome to NASA's internal system!");
                break;
            //default:
            //    Debug.LogError("Invalid level reached");                
        }
    }
}