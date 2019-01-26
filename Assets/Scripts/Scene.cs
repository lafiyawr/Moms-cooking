using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Scene : MonoBehaviour
{   
    [SerializeField]private Text[] _headers;
    public Text[] Headers => _headers;
    
    [SerializeField] private Text[] _bodyText;
    public Text[] BodyText => _bodyText;
    
    [SerializeField]private SpriteRenderer _background;
    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
