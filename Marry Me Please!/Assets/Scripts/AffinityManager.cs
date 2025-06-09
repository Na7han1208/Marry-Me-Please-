using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class AffinityManager : MonoBehaviour
{
    [SerializeField] private Slider MingSlider;
    [SerializeField] private Slider TheoSlider;
    [SerializeField] private Slider FenSlider;
    [SerializeField] private Slider YukiSlider;
    [SerializeField] private Slider YilinSlider;
    [SerializeField] private Slider ZihanSlider;
    [SerializeField] private Slider JinhuiSlider;

    void Awake()
    {
        SaveData data = SaveLoadManager.Instance.LoadGame();

        //Ming
        int mingAffinity;
        if (data.mingAffinity > 100)
        {
            mingAffinity = 100;
        }
        else if (data.mingAffinity < 0)
        {
            mingAffinity = 0;
        }
        else
        {
            mingAffinity = data.mingAffinity;
        }
        MingSlider.value = mingAffinity;

        //Theo
        int theoAffinity;
        if (data.theodoreAffinity > 100)
        {
            theoAffinity = 100;
        }
        else if (data.theodoreAffinity < 0)
        {
            theoAffinity = 0;
        }
        else
        {
            theoAffinity = data.theodoreAffinity;
        }
        TheoSlider.value = theoAffinity;

        //Fen
        int fenAffinity;
        if (data.fenAffinity > 100)
        {
            fenAffinity = 100;
        }
        else if (data.fenAffinity < 0)
        {
            fenAffinity = 0;
        }
        else
        {
            fenAffinity = data.fenAffinity;
        }
        FenSlider.value = fenAffinity;

        //Yuki
        int yukiAffinity;
        if (data.yukiAffinity > 100)
        {
            yukiAffinity = 100;
        }
        else if (data.yukiAffinity < 0)
        {
            yukiAffinity = 0;
        }
        else
        {
            yukiAffinity = data.yukiAffinity;
        }
        YukiSlider.value = yukiAffinity;

        //Yilin
        int yilinAffinity;
        if (data.yilinAffinity > 100)
        {
            yilinAffinity = 100;
        }
        else if (data.yilinAffinity < 0)
        {
            yilinAffinity = 0;
        }
        else
        {
            yilinAffinity = data.yilinAffinity;
        }
        YilinSlider.value = yilinAffinity;

        //Zihan
        int zihanAffinity;
        if (data.zihanAffinity > 100)
        {
            zihanAffinity = 100;
        }
        else if (data.zihanAffinity < 0)
        {
            zihanAffinity = 0;
        }
        else
        {
            zihanAffinity = data.zihanAffinity;
        }
        ZihanSlider.value = zihanAffinity;

        //Jinhui
        int jinhuiAffinity;
        if (data.jinhuiAffinity > 100)
        {
            jinhuiAffinity = 100;
        }
        else if (data.jinhuiAffinity < 0)
        {
            jinhuiAffinity = 0;
        }
        else
        {
            jinhuiAffinity = data.jinhuiAffinity;
        }
        JinhuiSlider.value = jinhuiAffinity;
    }

    public void Continue()
    {
        SceneManager.LoadScene("Main");
    }
}
