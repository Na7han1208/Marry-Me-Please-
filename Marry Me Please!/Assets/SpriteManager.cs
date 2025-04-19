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

    public Sprite changeSprite(int mood, String characterName){
        /*
            Character Mood Indexes
            1:  Neutral
            2:  Happy
            3:  Sad
            4:  Angry
            5:  Blushing
            6:  Surprised
            7:  Irritated
            8:  Terror/Panik
            9:  Smug
        */
        switch(characterName){
            case "Ming": 
            switch(mood){
                case 1: return mingNeutral;
                case 2: return mingHappy;
                case 3: return mingSad;
                case 4: return mingAngry;
                case 5: return mingBlushing;
                case 6: return mingSurprised;
                case 7: return mingIrritated;
                case 8: return mingTerror;
                case 9: return mingSmug;
                default: return null;
            }               
            case "Jinhui":   
            switch(mood){
                case 1: return jinhuiNeutral;
                case 2: return jinhuiHappy;
                case 3: return jinhuiSad;
                case 4: return jinhuiAngry;
                case 5: return jinhuiBlushing;
                case 6: return jinhuiSurprised;
                case 7: return jinhuiIrritated;
                case 8: return jinhuiTerror;
                case 9: return jinhuiSmug;
                default: return null;
            }          
            case "Yilin":             
            switch(mood){
                case 1: return yilinNeutral;
                case 2: return yilinHappy;
                case 3: return yilinSad;
                case 4: return yilinAngry;
                case 5: return yilinBlushing;
                case 6: return yilinSurprised;
                case 7: return yilinIrritated;
                case 8: return yilinTerror;
                case 9: return yilinSmug;
                default: return null;
            }     
            case "Fen":   
            switch(mood){
                case 1: return fenNeutral;
                case 2: return fenHappy;
                case 3: return fenSad;
                case 4: return fenAngry;
                case 5: return fenBlushing;
                case 6: return fenSurprised;
                case 7: return fenIrritated;
                case 8: return fenTerror;
                case 9: return fenSmug;
                default: return null;
            }                   
            case "Yuki":    
            switch(mood){
                case 1: return yukiNeutral;
                case 2: return yukiHappy;
                case 3: return yukiSad;
                case 4: return yukiAngry;
                case 5: return yukiBlushing;
                case 6: return yukiSurprised;
                case 7: return yukiIrritated;
                case 8: return yukiTerror;
                case 9: return yukiSmug;
                default: return null;
            }                              
            case "Theodore":
            switch(mood){
                case 1: return theodoreNeutral;
                case 2: return theodoreHappy;
                case 3: return theodoreSad;
                case 4: return theodoreAngry;
                case 5: return theodoreBlushing;
                case 6: return theodoreSurprised;
                case 7: return theodoreIrritated;
                case 8: return theodoreTerror;
                case 9: return theodoreSmug;
                default: return null;
            }                            
            case "Zihan":      
            switch(mood){
                case 1: return zihanNeutral;
                case 2: return zihanHappy;
                case 3: return zihanSad;
                case 4: return zihanAngry;
                case 5: return zihanBlushing;
                case 6: return zihanSurprised;
                case 7: return zihanIrritated;
                case 8: return zihanTerror;
                case 9: return zihanSmug;
                default: return null;
            }                           
            default: Debug.Log("Character not found"); break;
        }
        return null;
    }
}

/*
NOTE:
Is a big switch case good practice? no it isn't expandable, however this project calls for a fixed number of character sprites
and since we know how many character sprites exist before hand because of planning, we can use this much simpler system
*/