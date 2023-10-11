using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameSection
{
    Lvl1,
    Lvl2,
    MainMenu

}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameSection currentSection = GameSection.MainMenu;

    public GameSection CurrentSection
    { get => currentSection; set => currentSection = value;}


    // Start is called before the first frame update
    private void Awake(){

        if(Instance == null)
        {
            Instance = this;
             DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadARScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMainMenuScene()
    {
        currentSection = GameSection.MainMenu;
        SceneManager.LoadScene(0);
    }

    
}
