using UnityEngine;

public class LetterSpawner : MonoBehaviour
{
    public GameObject[] letterPrefabs;
    public TraceManager TraceManager;
    public Transform spawnLocation;

    private GameObject currentLetter;


    public void LoadLetter(int index)
    {
        if (currentLetter != null)
        {
            Destroy(currentLetter);
        }

        currentLetter = Instantiate(letterPrefabs[index], spawnLocation.position, Quaternion.identity);
        TraceManager.SetupLetter(currentLetter);
    }

    void Start()
    {
        LoadLetter(0);  //  CHANGE LATER FOR THE WORD THING
    }
}
