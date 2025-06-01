using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

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

    [Header("Tile Setup")]
    public Tile[] tiles = new Tile[16]; 
    public Sprite[] tileSprites; 
    public Sprite hiddenSprite;

    private Tile firstTile = null;
    private Tile secondTile = null;
    private bool inputLocked = false;

    void Start()
    {
        SetupTiles();
    }

    void SetupTiles()
    {
        // Create shuffled list of paired sprites
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
            tiles[i].assignedSprite = spritePool[i];
            tiles[i].isMatched = false;
            tiles[i].Hide(hiddenSprite);

            tiles[i].button.onClick.RemoveAllListeners();
            tiles[i].button.onClick.AddListener(() => OnTileClicked(index));
        }
    }

    void OnTileClicked(int index)
    {
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
}
