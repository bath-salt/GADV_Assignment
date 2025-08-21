using UnityEngine;
using TMPro;

public class TracingDisplay : MonoBehaviour
{
    public TextMeshPro wordText3D;
    public Transform imageSpawnPoint;

    private GameObject spawnedImage;

    void Start()
    {
        // Populate the 3D word label from session state.
        // Uppercasing standardizes typography so the traced letterforms match the label.
        if (!string.IsNullOrEmpty(GameSession.SelectedWord))
        {
            wordText3D.text = GameSession.SelectedWord.ToUpper();
        }

        // If a request image is provided, instantiate it at the anchor
        // we spawn at the anchor;s world pos, then parent it to subsequent layout
        // can be driven by the anchor (e.g moving/clearing theb whole display together)
        if(GameSession.SelectedWordPrefab  != null && imageSpawnPoint != null)
        {
            spawnedImage = Instantiate(GameSession.SelectedWordPrefab, imageSpawnPoint.position, Quaternion.identity);

            // Parent after instantiation
            // using SetParent keeps world pos by default which avoids a visual snap
            // we then normalise local scale to 1 so the image is not distordted by any non-unit parent scaling
            spawnedImage.transform.SetParent(imageSpawnPoint);
            spawnedImage.transform.localScale = Vector3.one;
        }
    }
    public void ClearDisplay()
    {
        if (spawnedImage != null)
        {
            Destroy(spawnedImage);
            spawnedImage = null;
        }
        if (wordText3D != null)
        {
            wordText3D.gameObject.SetActive(false);
        }
            
    }


    
}
