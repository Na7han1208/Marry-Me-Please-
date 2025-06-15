using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MahjongManager : MonoBehaviour
{
    [System.Serializable]
    public class Tile
    {
        public Button button;
        public Image image;
        public Sprite assignedSprite;
        public bool isMatched = false;

        public void Show() => image.sprite = assignedSprite;
        public void Hide(Sprite hidden) { if (!isMatched) image.sprite = hidden; }
    }

    [Header("UI")]
    public TMP_Text timerText;
    public TMP_Text winPanel;

    private float timeRemaining = 45f;
    private bool gameWon = false;



    [Header("Tile Setup")]
    public Tile[] tiles = new Tile[16];
    public Sprite[] tileSprites;
    public Sprite hiddenSprite;

    private Tile firstTile = null;
    private Tile secondTile = null;
    private bool inputLocked = false;

    void Start()
    {
        AudioManager.Instance.StopAll();
        AudioManager.Instance.Play("3CMMusic");
        SetupTiles();
    }

    void Update()
    {
        if (!gameWon)
        {
            timeRemaining -= Time.deltaTime;
            if (timerText != null)
            {
                timerText.text = FormatTime(timeRemaining);
            }
            if (timeRemaining <= 0)
            {
                StartCoroutine(LoseGame());
            }
        }
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return $"{minutes:00}:{seconds:00}";
    }


    void SetupTiles()
    {
        List<Sprite> spritePool = new List<Sprite>();
        foreach (var s in tileSprites)
        {
            spritePool.Add(s);
            spritePool.Add(s);
        }

        // Shuffle
        for (int i = 0; i < spritePool.Count; i++)
        {
            Sprite temp = spritePool[i];
            int rand = Random.Range(i, spritePool.Count);
            spritePool[i] = spritePool[rand];
            spritePool[rand] = temp;
        }

        // Assign to tiles
        for (int i = 0; i < tiles.Length; i++)
        {
            int index = i;
            if (tiles[i].button != null && tiles[i].image == null)
            {
                tiles[i].image = tiles[i].button.GetComponent<Image>();
            }

            tiles[i].assignedSprite = spritePool[i];
            tiles[i].isMatched = false;
            tiles[i].Hide(hiddenSprite);

            tiles[i].button.onClick.RemoveAllListeners();
            tiles[i].button.onClick.AddListener(() => OnTileClicked(index));
        }
    }

    void OnTileClicked(int index)
    {
        AudioManager.Instance.Play("TileFlip");
        if (inputLocked || tiles[index].isMatched)
            return;

        Tile clickedTile = tiles[index];
        clickedTile.Show();

        if (firstTile == null)
        {
            firstTile = clickedTile;
        }
        else if (secondTile == null && clickedTile != firstTile)
        {
            secondTile = clickedTile;
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        inputLocked = true;
        yield return new WaitForSeconds(1f);

        if (firstTile.assignedSprite == secondTile.assignedSprite)
        {
            firstTile.isMatched = true;
            secondTile.isMatched = true;
            AudioManager.Instance.Play("Correct");
            CheckWin();
        }
        else
        {
            firstTile.Hide(hiddenSprite);
            secondTile.Hide(hiddenSprite);
        }

        firstTile = null;
        secondTile = null;
        inputLocked = false;
    }

    void CheckWin()
    {
        foreach (var tile in tiles)
        {
            if (!tile.isMatched)
                return;
        }
        gameWon = true;
        StartCoroutine(WinGame());
    }

    IEnumerator LoseGame()
    {
        inputLocked = true;
        winPanel.text = "You lose!";
        if (PlayerPrefs.GetInt("RouteFromMenu") == 0)
        {
            SaveData existingData = SaveLoadManager.Instance.LoadGame();
            SaveData saveData = new SaveData();

            saveData.currentLine = existingData.currentLine;
            saveData.playerName = existingData.playerName;

            saveData.mingAffinity = existingData.mingAffinity-8;
            saveData.jinhuiAffinity = existingData.jinhuiAffinity-8;
            saveData.yilinAffinity = existingData.yilinAffinity-8;
            saveData.fenAffinity = existingData.fenAffinity-8;
            saveData.yukiAffinity = existingData.yukiAffinity-8;
            saveData.theodoreAffinity = existingData.theodoreAffinity-8;
            saveData.zihanAffinity = existingData.zihanAffinity-8;
            SaveLoadManager.Instance.SaveGame(saveData);

            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene("Main");
        }
        else
        {
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene("MainMenu");
        }   
    }

    IEnumerator WinGame()
    {
        inputLocked = true;
        winPanel.text = "You Win!";
        AudioManager.Instance.Play("Correct");
        if (PlayerPrefs.GetInt("RouteFromMenu") == 0)
        {
            SaveData existingData = SaveLoadManager.Instance.LoadGame();
            SaveData saveData = new SaveData();

            saveData.currentLine = existingData.currentLine;
            saveData.playerName = existingData.playerName;

            saveData.mingAffinity = existingData.mingAffinity + 8;
            saveData.jinhuiAffinity = existingData.jinhuiAffinity + 8;
            saveData.yilinAffinity = existingData.yilinAffinity + 8;
            saveData.fenAffinity = existingData.fenAffinity + 8;
            saveData.yukiAffinity = existingData.yukiAffinity + 8;
            saveData.theodoreAffinity = existingData.theodoreAffinity + 8;
            saveData.zihanAffinity = existingData.zihanAffinity + 8;
            SaveLoadManager.Instance.SaveGame(saveData);

            yield return new WaitForSeconds(3f);
            AudioManager.Instance.StopAll();
            SceneManager.LoadScene("Main");
        }
        else
        {
            yield return new WaitForSeconds(3f);
            AudioManager.Instance.StopAll();
            SceneManager.LoadScene("MainMenu");
        }   
    }
}
