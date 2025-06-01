using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    [SerializeField] GameObject ThroneRoom;
    [SerializeField] GameObject TrainingGrounds;
    [SerializeField] GameObject Kitchen;
    [SerializeField] GameObject GreatHall;
    [SerializeField] GameObject Garden;
    [SerializeField] GameObject Study;
    [SerializeField] GameObject Courtyard;
    [SerializeField] GameObject Hallway;
    [SerializeField] GameObject BedChamber;

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
        ThroneRoom.SetActive(false);
        TrainingGrounds.SetActive(false);    
        Kitchen.SetActive(false);            
        GreatHall.SetActive(false);          
        Garden.SetActive(false);             
        Study.SetActive(false);              
        Courtyard.SetActive(false);
        Hallway.SetActive(false);
        BedChamber.SetActive(false); 

        //Load parsed room
        switch(levelIndex){
            case 1: ThroneRoom.SetActive(true);         break;
            case 2: TrainingGrounds.SetActive(true);    break;
            case 3: Kitchen.SetActive(true);            break;
            case 4: GreatHall.SetActive(true);          break;
            case 5: Garden.SetActive(true);             break;
            case 6: Study.SetActive(true);              break;
            case 7: Courtyard.SetActive(true);          break;
            case 8: Hallway.SetActive(true);            break;
            case 9: BedChamber.SetActive(true);         break;
        }
    }
}
