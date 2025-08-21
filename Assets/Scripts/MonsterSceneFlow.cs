using UnityEngine;
using System.Collections;

public class MonsterSceneFlow : MonoBehaviour
{
    public MonsterWalk monsterWalk;
    public MonsterRequest monsterRequest;
    public UIManager manager;
    public Transform itemBesideMonster;

    public GameObject winPanel;
    public int targetScoreToWin = 2;
    private GameObject spawnedDeliveredItem;

    IEnumerator Start()
    {
        // hide request UI until the monster has finished all animations
        if (manager != null)
        {
            manager.requestBubble.SetActive(false);
        }

        // if we came from tracing scene, play a short "thank you" speech bubble and
        // display the word that player just traced
        if(GameSession.JustDelivered && GameSession.SelectedWordPrefab != null)
        {
            yield return StartCoroutine(PlayDeliveryThanks());
            GameSession.JustDelivered = false;
            GameSession.SelectedWordPrefab = null;

            // if the we win, stop everything
            if(winPanel != null && winPanel.activeSelf)
                yield break;
        }

        // normal loop, monster will talk back into frame and give another request
        yield return StartCoroutine(monsterWalk.WalkIn());
        monsterRequest.GiveRandomRequest();
    }
    
    IEnumerator PlayDeliveryThanks()
    {
        // ensure that the monster is present before we show the delivered item and dialogue 
        yield return StartCoroutine(monsterWalk.WalkIn());

        // replace any previous props so it doesnt stack images next to the monster
        if (spawnedDeliveredItem != null) Destroy(spawnedDeliveredItem);
        spawnedDeliveredItem = Instantiate(GameSession.SelectedWordPrefab, itemBesideMonster.position, Quaternion.identity, itemBesideMonster);

        manager.ShowSpeech("អរគុណ");
        yield return new WaitForSeconds(4f);

        if (spawnedDeliveredItem != null)
        {
            Destroy(spawnedDeliveredItem);
        }

        manager.HideSpeech();
        yield return StartCoroutine(monsterWalk.WalkOut());

        // after the monster walks off, check to see if the player has won 
        // if the player won, lock inputs so they do not accidentally interact with other parts
        // of the scene
        if (GameSession.Score >= targetScoreToWin)
        {
            if(winPanel) winPanel.SetActive(true);
            GameSession.InputLocked = true;
            yield break;
        }       

        if (spawnedDeliveredItem != null) Destroy(spawnedDeliveredItem);

    }
}
