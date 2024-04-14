using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RotateToClosestTask : MonoBehaviour
{
    private TasksManagerBehaviour _tasksManagerBehaviour;
    public float offsetX = 0;
    public float offsetY = 0;
    private SpriteRenderer _spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        _tasksManagerBehaviour = FindFirstObjectByType<TasksManagerBehaviour>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _spriteRenderer.enabled = _tasksManagerBehaviour.Tasks.Count(t => t.Completed) > 0;
        if (!_spriteRenderer.enabled)
        {
            return;
        }
        var computers = _tasksManagerBehaviour.TasksOfComputers
            .Where(kv => !kv.Value.Completed)
            .Select(kv => kv.Key)
            .ToList();
        var actualPosition = transform.position + new Vector3(offsetX, offsetY, 0);
        var closestTask = computers
            .OrderBy(t => Vector3.Distance(t.transform.position, actualPosition))
            .First();
        var direction = closestTask.transform.position - actualPosition;
        transform.eulerAngles = Vector3.forward * (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        // transform.LookAt(Vector3.forward, closestTask.transform.position - transform.position);
    }
}
