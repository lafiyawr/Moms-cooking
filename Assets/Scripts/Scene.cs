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
    

    // Start is called before the first frame update
    void Start()
    {
        if (_headers.Length > 0)
            Debug.Log(_headers[0].text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
