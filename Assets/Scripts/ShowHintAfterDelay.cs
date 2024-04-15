using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class ShowHintAfterDelay : MonoBehaviour
{
    public TextMeshProUGUI warningText;
    public TextMeshProUGUI hintText;

    public int hintAfterSeconds = 5;

    private List<BossBehaviour> bossBehaviours;
    // Start is called before the first frame update
    void Start()
    {
        if (!GetComponent<IMinigame>().Task.HintReceived)
        {
            Invoke(nameof(ShowUnsolvableHint), hintAfterSeconds);
            bossBehaviours = FindObjectsOfType<BossBehaviour>().ToList();
        }
    }
    
    private void ShowUnsolvableHint()
    {
        warningText.gameObject.SetActive(true);
        hintText.gameObject.SetActive(true);
        var bossWithHint = bossBehaviours.FirstOrDefault(predicate: b => b.NextTaskHint == GetComponent<IMinigame>().Task);
        if (bossWithHint == null)
        {
            hintText.text = "Wait for a manager to summon you!";
        }
        else
        {
            hintText.text = "Consult with " + bossWithHint.Name + "!";
        }
    }
}
