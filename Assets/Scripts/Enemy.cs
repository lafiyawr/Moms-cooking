using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(AudioSource))]

public class Enemy : MonoBehaviour
{
    public int MyId;

    [SerializeField]private float _speed;

    [SerializeField]private float _health;

    private SpriteRenderer _spriteRenderer;
    private AudioSource _audioSource;
    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider2D;

    private Camera _cam;
    
    // Start is called before the first frame update
    protected void Start()
    {
        Debug.Log("Hi i'm enemy " + MyId);
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _cam = Camera.main;
        transform.position = RandomSpawnLocation();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Move(_speed);
    }

    private void Move(float speed)
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
    }

    private Vector3 RandomSpawnLocation()
    {
        return _cam.ScreenToWorldPoint(new Vector3(-50, Random.Range(0, Screen.height-10), _cam.nearClipPlane));
    }
}
