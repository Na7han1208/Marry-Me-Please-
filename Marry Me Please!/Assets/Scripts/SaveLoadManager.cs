using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance;
    private string saveFilePath;

    [Header("Settings")]
    public float masterVolume;
    public float dialogueSpeed;
    public bool fullscreen;

    private void Awake()
    {
        masterVolume = 1f;
        dialogueSpeed = 1f;
        fullscreen = false;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            saveFilePath = Path.Combine(Application.persistentDataPath, "savefile.json");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveGame(SaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Game saved to: " + saveFilePath);
    }

    public SaveData LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            Debug.Log("Game loaded from: " + saveFilePath);
            return data;
        }
        else
        {
            Debug.LogWarning("No save file found at: " + saveFilePath);
            return null;
        }
    }

    public bool SaveExists()
    {
        return File.Exists(saveFilePath);
    }

    public void DeleteSave()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("Save file deleted.");
        }
    }
}
