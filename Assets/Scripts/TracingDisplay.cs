using UnityEngine;
using TMPro;

public class TracingDisplay : MonoBehaviour
{
    public TextMeshPro wordText3D;
    public Transform imageSpawnPoint;

    private GameObject spawnedImage;

    void Start()
    {
        if (!string.IsNullOrEmpty(GameSession.SelectedWord))
        {
            wordText3D.text = GameSession.SelectedWord.ToUpper();
        }

        if(GameSession.SelectedWordPrefab  != null && imageSpawnPoint != null)
        {
            spawnedImage = Instantiate(GameSession.SelectedWordPrefab, imageSpawnPoint.position, Quaternion.identity);
            spawnedImage.transform.SetParent(imageSpawnPoint);
            spawnedImage.transform.localScale = Vector3.one;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
