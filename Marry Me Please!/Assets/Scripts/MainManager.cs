using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    [SerializeField] GameObject BlackScreen;
    [SerializeField] GameObject ThroneRoom;
    [SerializeField] GameObject Study;
    [SerializeField] GameObject Garden;


    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            returnToMenu();
        }
    }

    public void returnToMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    public void loadBackground(int levelIndex){
        /*
            LEVEL INDEX
            1 - Throne Room
            2 - Training Grounds
            3 - Kitchen
            4 - Great Hall
            5 - Garden
            6 - Study
            7 - Courtyard
            8 - Hallway
            9 - Bedchamber
        */

        //Make sure all other rooms are inactive

        BlackScreen.SetActive(false);
        ThroneRoom.SetActive(false);               
        Garden.SetActive(false);             
        Study.SetActive(false);              

        //Load parsed room
        switch(levelIndex){
            case 0: BlackScreen.SetActive(true);        break;
            case 1: ThroneRoom.SetActive(true);         break;
            case 2: Study.SetActive(true);              break;
            case 3: Garden.SetActive(true);             break;
        }
    }
}
