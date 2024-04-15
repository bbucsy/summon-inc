using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BossBehaviour))]
public class BossHintTimer : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]private float _innerTimer = 0;
    [SerializeField]private float _summonTimer = 0;
    [SerializeField] private float _hintTimer = 0;
    private BossBehaviour _bossBehaviour;
    
    public int TimerOffset = 0;

    public int SummonInterval = 15;

    public int HintInterval = 30;
    
    void Start()
    {
        _bossBehaviour = GetComponent<BossBehaviour>();
        _bossBehaviour.hasMission = false;
    }

    // Update is called once per frame
    void Update()
    {
        _innerTimer += Time.deltaTime;
        if (_innerTimer - TimerOffset > 0)
        {
            _summonTimer += Time.deltaTime;
            _hintTimer += Time.deltaTime;
        }
       

        if (_summonTimer > SummonInterval)
        {
            _summonTimer -= SummonInterval;
            _bossBehaviour.hasMission = true;
        }

        if (_hintTimer > HintInterval && _bossBehaviour.NextTaskHint == null)
        {
            _hintTimer = 0;
            getNextHint();
        }

    }

    private void getNextHint()
    {
        var task = FindFirstObjectByType<TasksManagerBehaviour>()
            .Tasks
            .FirstOrDefault(t => !t.HintReceived && !t.AssignedToBoss);

        if (task == null) return;
        
        task.AssignedToBoss = true;
        _bossBehaviour.NextTaskHint = task;

    }
}
