using UnityEngine;
using System.Collections;

public class MonsterSceneFlow : MonoBehaviour
{
    public MonsterWalk monsterWalk;
    public MonsterRequest monsterRequest;
    public UIManager manager;
    public Transform itemBesideMonster;

    private GameObject spawnedDeliveredItem;

    IEnumerator Start()
    {
        if (manager != null) manager.requestBubble.SetActive(false);
        if(GameSession.JustDelivered && GameSession.SelectedWordPrefab != null)
        {
            yield return StartCoroutine(PlayDeliveryThanks());
            GameSession.JustDelivered = false;
            GameSession.SelectedWordPrefab = null;
        }

        yield return StartCoroutine(monsterWalk.WalkIn());
        monsterRequest.GiveRandomRequest();
    }
    
    IEnumerator PlayDeliveryThanks()
    {
        yield return StartCoroutine(monsterWalk.WalkIn());

        if (spawnedDeliveredItem != null) Destroy(spawnedDeliveredItem);
        spawnedDeliveredItem = Instantiate(GameSession.SelectedWordPrefab, itemBesideMonster.position, Quaternion.identity, itemBesideMonster);

        manager.ShowSpeech("អរគុណ");
        yield return new WaitForSeconds(1f);
        AttachItemToMonster(spawnedDeliveredItem.transform, monsterWalk.carryPoint);

        manager.requestBubble.SetActive(false);
        yield return StartCoroutine(monsterWalk.WalkOut());

        if (spawnedDeliveredItem != null) Destroy(spawnedDeliveredItem);

    }

    void AttachItemToMonster(Transform item, Transform carryPoint)
    {
        if (item == null || carryPoint == null) return;

        item.SetParent(carryPoint, worldPositionStays:true);
        item.position = carryPoint.position;
        item.rotation = carryPoint.rotation;
        item.localScale = Vector3.one;
        
    }
}
