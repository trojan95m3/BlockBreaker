using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI script will handle showing the GameOver text, Restart button, and 
/// the console logs
/// </summary>
public class UI : MonoBehaviour
{
    public Text _TxtGameOver;
    public Button _BtnRestart;
    public Text _TxtConsole;

    [Tooltip("How many lines fit in the console")]
    public int _NumConsoleLines = 6;

    [Header("Log Strings")]
    public string _ProjectileShot = "A projectile was shot";
    public string _BlockHit = "A projectile has hit a block";
    public string _BlockDestroyed = "A block was destroyed";

    private List<string> mLogMessages = new List<string>();

    private void Start()
    {
        _TxtGameOver.gameObject.SetActive(false);
        _BtnRestart.gameObject.SetActive(false);

        _BtnRestart.onClick.AddListener(Restart);
    }

    private void Restart()
    {
        _TxtGameOver.gameObject.SetActive(false);
        _BtnRestart.gameObject.SetActive(false);

        GameManager.pInstance.Restart();

        mLogMessages.Clear();
        _TxtConsole.text = "";
    }

    public void GameOver()
    {
        _TxtGameOver.gameObject.SetActive(true);
        _BtnRestart.gameObject.SetActive(true);
    }

    /// <summary>
    /// add the new message to the list.  Check if too many messages and
    /// remove the oldest.  Display the updated text.
    /// </summary>
    /// <param name="message"></param>
    private void UpdateConsole(string message)
    {
        mLogMessages.Add(message);

        if (mLogMessages.Count > _NumConsoleLines)
            mLogMessages.RemoveAt(0);

        StringBuilder sb = new StringBuilder();
        foreach(string s in mLogMessages)
            sb.Append(s + "\n");

        _TxtConsole.text = sb.ToString();
    }

    public void ProjectileShot()
    { 
        UpdateConsole(_ProjectileShot);
    }

    public void BlockHit()
    {
        UpdateConsole(_BlockHit);
    }

    public void BlockDestroyed()
    {
        UpdateConsole(_BlockDestroyed);
    }
}
