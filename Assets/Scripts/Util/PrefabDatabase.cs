using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Prefab Database")]
public class PrefabDatabase : ScriptableObject
{
    [SerializeField] private GameObject[] _actors;

    public GameObject[] Actors
    {
        get { return _actors; }
    }

    [SerializeField] private GameObject[] _ui;

    public GameObject[] Ui
    {
        get { return _ui; }
    }

    [SerializeField] private GameObject[] _scenes;

    public GameObject[] Scenes
    {
        get { return _scenes; }
    }
}
