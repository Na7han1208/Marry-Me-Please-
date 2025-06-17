using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{



    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            returnToMenu();
        }
    }

    public void returnToMenu(){
        SceneManager.LoadScene("MainMenu");
    }


}
