using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(AudioSource))]

public class Enemy : MonoBehaviour
{
    public int MyId;

    [SerializeField]private float _speed;

    [SerializeField]private int _health;

    private SpriteRenderer _spriteRenderer;
    private AudioSource _audioSource;
    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider2D;

    private Camera _cam;
    private bool _hasFiredCollisionEvent = false;
    
    
    // Start is called before the first frame update
    protected void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _rigidbody2D.isKinematic = true;
        _cam = Camera.main;
        transform.position = GetRandomSpawnLocation();
    }

    // Update is called once per frame
    protected void Update()
    {
        Move(_speed);
        CheckForCollisionWithThreshold();
    }

    private void CheckForCollisionWithThreshold()
    {
        if (transform.position.x >= 10 && !_hasFiredCollisionEvent)
        {
            EventManager.Instance.Fire(new CritterCollisionEvent());
            _hasFiredCollisionEvent = true;
        }

//        GameObject threshold = Services.GgjSceneManager.GameScene.thres
//        if(Vector3.Distance())
    }

    private void Move(float speed)
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
    }

    private Vector3 GetRandomSpawnLocation()
    {
        return _cam.ScreenToWorldPoint(new Vector3(-50, Random.Range(0, Screen.height-10), _cam.nearClipPlane));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<weaponManager>() != null){
            Debug.Log("collided with " + gameObject);
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();            
            }

            if (_health >= 1)
            {
                _health -= 1;            
            }

            if (_health < 1)
            {
                gameObject.SetActive(false);
            }
        }
    }
    
}
