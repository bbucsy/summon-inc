using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RotateToClosestManager : MonoBehaviour
{
    private List<GameObject> _bosses;
    private SpriteRenderer _spriteRenderer;
    public float offsetX = 0;
    public float offsetY = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        var bossBehaviours = FindObjectsByType<BossBehaviour>(FindObjectsSortMode.None);
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _bosses = bossBehaviours
            .Where(b => b.hasMission)
            .Select(b => b.gameObject)
            .ToList();
        _spriteRenderer.enabled = _bosses.Count > 0;
        foreach (var boss in bossBehaviours)
        {
            Debug.Log("Subscribing to boss events!");
            boss.BossWantsToTalk += (sender, args) =>
            {
                Debug.Log("Boss wants to talk!");
                _bosses.Add(args.gameObject);
                _spriteRenderer.enabled = true;
            };
            boss.BossTalked += (sender, args) =>
            {
                Debug.Log("Boss talked!");
                _bosses.Remove(args.gameObject);
                _spriteRenderer.enabled = _bosses.Count > 0;
            };
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_bosses.Count == 0)
        {
            return;
        }
        var actualPosition = transform.position + new Vector3(offsetX, offsetY, 0);
        var closestBoss = _bosses
            .OrderBy(t => Vector3.Distance(t.transform.position, actualPosition))
            .First();
        var direction = closestBoss.transform.position - actualPosition;
        transform.eulerAngles = Vector3.forward * (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        // transform.LookAt(Vector3.forward, closestTask.transform.position - transform.position);
    }
}
