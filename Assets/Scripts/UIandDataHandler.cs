using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIandDataHandler : MonoBehaviour
{
    [Tooltip("Place the Text Mesh Pro Input field game object here.")]
    public GameObject NameField;
    public GameObject BestScore;

    public string userName;
    public string enteredText;    

    private void Start()
    {
        DataManager.Instance.LoadHighscore();
        BestScore.GetComponent<Text>().text = "Best Score : " + DataManager.Instance.highscoreUsername + " : " + DataManager.Instance.highscoreUserScore.ToString();

        // if data manager already holding a username then
        //      overwite that name to username and text field        
        if (DataManager.Instance.currUserName.Length > 1)
        {
            NameField.GetComponent<TMP_InputField>().text = DataManager.Instance.currUserName;            
        }
        enteredText = NameField.GetComponent<TMP_InputField>().text;
        userName = enteredText;

        // IMPORTANT : if assigning a value to Unity inspector component then u must call the unity function to get that component
        // and then assign the new value to that, instead of assigning value to some reference of it
        // ex: a change in enteredText variable will have no effect over actual text field value        
    }

    private void Update()
    {        
        // if any changes occured to name textfield
        //      if not null and new name != old name
        //          save this new name in data manager
        enteredText = NameField.GetComponent<TMP_InputField>().text;
        if (userName != enteredText && enteredText.Length > 1)
        {
            userName = enteredText;
            DataManager.Instance.currUserName = enteredText;
        }
    }

    public void StartGame()
    {
        SaveUserName();
        SceneManager.LoadScene(1);        
    }

    public void SaveUserName()
    {
        DataManager.Instance.currUserName = userName;        
    }

    public void Exit()
    {        
#if UNITY_EDITOR
    // doing conditional compiling (via preprocessor directives) based on location of input
    EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
