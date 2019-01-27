using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class CutScene : MonoBehaviour
{
    private bool _isCutSceneComplete;

    public bool IsCutSceneComplete => _isCutSceneComplete;

    [SerializeField]private GameObject _narrationScreen; 
    [SerializeField] private List<GameObject> _whoTalksList;
    [SerializeField] private List<string> _dialogueList;

    private bool _isTweening;
    private int _numWhoIsTalkingNow;

    enum CutSceneSection
    {
        Narration,
        Dialogue
    }

    private CutSceneSection _cutSceneSection;

    // Start is called before the first frame update
    void Start()
    {
        _numWhoIsTalkingNow = 0;
        if (_narrationScreen != null)
        {
            _cutSceneSection = CutSceneSection.Narration;        
        }
        else
        {
            _cutSceneSection = CutSceneSection.Narration;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (_cutSceneSection)
        {
            case CutSceneSection.Narration:
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    _narrationScreen.SetActive(false);
                    _cutSceneSection = CutSceneSection.Dialogue;
                } 
                break;
            case CutSceneSection.Dialogue:
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
                    if(Input.GetKeyDown(KeyCode.Return))
                        _isCutSceneComplete = true;
                }
                break;
            default:
                break;
        }

    
    }

    public void DoSimpleTalkTween(GameObject actor)
    {
        _isTweening = true;
        actor.transform.GetChild(0).gameObject.SetActive(true);
        Sequence a = DOTween.Sequence();
        a.Append(actor.transform.DOMoveY(1f, 0.25f));
        a.Append(actor.transform.DOMoveY(-1f, 0.1f));
        a.OnComplete(() => _isTweening = false);
    }

    public void UpdateSpeechBubble(TextMeshPro textMeshPro)
    {
        textMeshPro.text = _dialogueList[_numWhoIsTalkingNow];
//        Debug.Log(textMeshPro.text);
    }


}

