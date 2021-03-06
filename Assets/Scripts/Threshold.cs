﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Threshold : MonoBehaviour
{

    private bool _hitReceived;

    public bool HitReceived => _hitReceived;

    private Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.Register<CritterCollisionEvent>(SetHitReceivedTrue);
        _rb = GetComponent<Rigidbody2D>();
        _rb.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHitReceivedTrue(GameEvent e)
    {
        CritterCollisionEvent _e = e as CritterCollisionEvent;
        _hitReceived = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Enemy>() != null)
        {
            _hitReceived = true;
        }
    }

    private void OnDestroy()
    {
        EventManager.Instance.Unregister<CritterCollisionEvent>(SetHitReceivedTrue);
    }

}

public class CritterCollisionEvent : GameEvent
{
    
}
