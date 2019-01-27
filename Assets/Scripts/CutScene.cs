using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class CutScene : MonoBehaviour
{
    private bool _isCutSceneComplete;

    public bool IsCutSceneComplete => _isCutSceneComplete;

    [SerializeField] private List<GameObject> _whoTalksList;
    [SerializeField] private List<string> _dialogueList;

    private bool _isTweening;
    private int _numWhoIsTalkingNow;
    
    // Start is called before the first frame update
    void Start()
    {
        _numWhoIsTalkingNow = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_numWhoIsTalkingNow < _whoTalksList.Count)
        {
            if (_whoTalksList.Count > 0 && !_isTweening && _whoTalksList.Count == _dialogueList.Count)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    DoSimpleTalkTween(_whoTalksList[_numWhoIsTalkingNow]);
                    UpdateSpeechBubble(_whoTalksList[_numWhoIsTalkingNow].GetComponentInChildren<TextMeshPro>());
                    ++_numWhoIsTalkingNow;
                }
            }
        }
        else
        {
            _isCutSceneComplete = true;
        }
    }

    public void DoSimpleTalkTween(GameObject actor)
    {
        _isTweening = true;
        Sequence a = DOTween.Sequence();
        a.Append(actor.transform.DOMove(Vector3.up, 0.25f)).SetEase(Ease.InBounce);
        a.Append(actor.transform.DOMove(Vector3.down, 0.1f)).SetEase(Ease.InCirc);
        Sequence b = DOTween.Sequence();
        b.Append(actor.transform.DORotate(new Vector3(30, actor.transform.eulerAngles.y, 0), 0.35f));
        b.OnComplete(() => _isTweening = false);
    }

    public void UpdateSpeechBubble(TextMeshPro textMeshPro)
    {
        textMeshPro.text = _dialogueList[_numWhoIsTalkingNow];
    }


}

