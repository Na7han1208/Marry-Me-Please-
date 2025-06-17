using System;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    [Header("CHARACTER SPRITES")]
    [Header("Ming")]
    [SerializeField] private Sprite mingNeutral;
    [SerializeField] private Sprite mingHappy;
    [SerializeField] private Sprite mingSad;
    [SerializeField] private Sprite mingAngry;
    [SerializeField] private Sprite mingBlushing;
    [SerializeField] private Sprite mingSurprised;
    [SerializeField] private Sprite mingIrritated;
    [SerializeField] private Sprite mingTerror;
    [SerializeField] private Sprite mingSmug;

    [Header("Jinhui")]
    [SerializeField] private Sprite jinhuiNeutral;
    [SerializeField] private Sprite jinhuiHappy;
    [SerializeField] private Sprite jinhuiSad;
    [SerializeField] private Sprite jinhuiAngry;
    [SerializeField] private Sprite jinhuiBlushing;
    [SerializeField] private Sprite jinhuiSurprised;
    [SerializeField] private Sprite jinhuiIrritated;
    [SerializeField] private Sprite jinhuiTerror;
    [SerializeField] private Sprite jinhuiSmug;

    [Header("Yilin")]
    [SerializeField] private Sprite yilinNeutral;
    [SerializeField] private Sprite yilinHappy;
    [SerializeField] private Sprite yilinSad;
    [SerializeField] private Sprite yilinAngry;
    [SerializeField] private Sprite yilinBlushing;
    [SerializeField] private Sprite yilinSurprised;
    [SerializeField] private Sprite yilinIrritated;
    [SerializeField] private Sprite yilinTerror;
    [SerializeField] private Sprite yilinSmug;

    [Header("Fen")]
    [SerializeField] private Sprite fenNeutral;
    [SerializeField] private Sprite fenHappy;
    [SerializeField] private Sprite fenSad;
    [SerializeField] private Sprite fenAngry;
    [SerializeField] private Sprite fenBlushing;
    [SerializeField] private Sprite fenSurprised;
    [SerializeField] private Sprite fenIrritated;
    [SerializeField] private Sprite fenTerror;
    [SerializeField] private Sprite fenSmug;

    [Header("Yuki")]
    [SerializeField] private Sprite yukiNeutral;
    [SerializeField] private Sprite yukiHappy;
    [SerializeField] private Sprite yukiSad;
    [SerializeField] private Sprite yukiAngry;
    [SerializeField] private Sprite yukiBlushing;
    [SerializeField] private Sprite yukiSurprised;
    [SerializeField] private Sprite yukiIrritated;
    [SerializeField] private Sprite yukiTerror;
    [SerializeField] private Sprite yukiSmug;

    [Header("Theodore")]
    [SerializeField] private Sprite theodoreNeutral;
    [SerializeField] private Sprite theodoreHappy;
    [SerializeField] private Sprite theodoreSad;
    [SerializeField] private Sprite theodoreAngry;
    [SerializeField] private Sprite theodoreBlushing;
    [SerializeField] private Sprite theodoreSurprised;
    [SerializeField] private Sprite theodoreIrritated;
    [SerializeField] private Sprite theodoreTerror;
    [SerializeField] private Sprite theodoreSmug;

    [Header("Zihan")]
    [SerializeField] private Sprite zihanNeutral;
    [SerializeField] private Sprite zihanHappy;
    [SerializeField] private Sprite zihanSad;
    [SerializeField] private Sprite zihanAngry;
    [SerializeField] private Sprite zihanBlushing;
    [SerializeField] private Sprite zihanSurprised;
    [SerializeField] private Sprite zihanIrritated;
    [SerializeField] private Sprite zihanTerror;
    [SerializeField] private Sprite zihanSmug;

    [Header("Other")]
    [SerializeField] private Sprite wu;

    public Sprite changeSprite(int mood, String characterName){
        /*
            Character Mood Indexes
            0:  Neutral
            1:  Happy
            2:  Sad
            3:  Angry
            4:  Blushing
            5:  Surprised
            6:  Irritated
            7:  Terror/Panik
            8:  Smug
        */
        Debug.Log("SpriteManager Class" + mood + "\t" + characterName);
        try{
            switch(characterName){
                case "Ming": 
                switch(mood){
                    case 0: return mingNeutral;
                    case 1: return mingHappy;
                    case 2: return mingSad;
                    case 3: return mingAngry;
                    case 4: return mingBlushing;
                    case 5: return mingSurprised;
                    case 6: return mingIrritated;
                    case 7: return mingTerror;
                    case 8: return mingSmug;
                    default: return null;
                }               
                case "Jinhui":   
                switch(mood){
                    case 0: return jinhuiNeutral;
                    case 1: return jinhuiHappy;
                    case 2: return jinhuiSad;
                    case 3: return jinhuiAngry;
                    case 4: return jinhuiBlushing;
                    case 5: return jinhuiSurprised;
                    case 6: return jinhuiIrritated;
                    case 7: return jinhuiTerror;
                    case 8: return jinhuiSmug;
                    default: return null;
                }          
                case "Yilin":             
                switch(mood){
                    case 0: return yilinNeutral;
                    case 1: return yilinHappy;
                    case 2: return yilinSad;
                    case 3: return yilinAngry;
                    case 4: return yilinBlushing;
                    case 5: return yilinSurprised;
                    case 6: return yilinIrritated;
                    case 7: return yilinTerror;
                    case 8: return yilinSmug;
                    default: return null;
                }     
                case "Fen":   
                switch(mood){
                    case 0: return fenNeutral;
                    case 1: return fenHappy;
                    case 2: return fenSad;
                    case 3: return fenAngry;
                    case 4: return fenBlushing;
                    case 5: return fenSurprised;
                    case 6: return fenIrritated;
                    case 7: return fenTerror;
                    case 8: return fenSmug;
                    default: return null;
                }                   
                case "Yuki":    
                switch(mood){
                    case 0: return yukiNeutral;
                    case 1: return yukiHappy;
                    case 2: return yukiSad;
                    case 3: return yukiAngry;
                    case 4: return yukiBlushing;
                    case 5: return yukiSurprised;
                    case 6: return yukiIrritated;
                    case 7: return yukiTerror;
                    case 8: return yukiSmug;
                    default: return yukiHappy;
                }                              
                case "Theodore":
                switch(mood){
                    case 0: return theodoreNeutral;
                    case 1: return theodoreHappy;
                    case 2: return theodoreSad;
                    case 3: return theodoreAngry;
                    case 4: return theodoreBlushing;
                    case 5: return theodoreSurprised;
                    case 6: return theodoreIrritated;
                    case 7: return theodoreTerror;
                    case 8: return theodoreSmug;
                    default: return null;
                }                            
                case "Zihan":      
                switch(mood){
                    case 0: return zihanNeutral;
                    case 1: return zihanHappy;
                    case 2: return zihanSad;
                    case 3: return zihanAngry;
                    case 4: return zihanBlushing;
                    case 5: return zihanSurprised;
                    case 6: return zihanIrritated;
                    case 7: return zihanTerror;
                    case 8: return zihanSmug;
                    default: return null;
                }     
                case "Wu":    
                switch(mood){
                    case 0: return wu;
                    default: return null;
                }                 
                default: Debug.Log("Character not found"); break;
            }
            return null;
        }
        catch(Exception e){
            Debug.Log(e);
            return null;
        }
    }
}

/*
NOTE:
Is a big switch case good practice? no it isn't expandable, however this project calls for a fixed number of character sprites
and since we know how many character sprites exist before hand because of planning, we can use this much simpler system
*/