using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Romb : MonoBehaviour
{
    private Animator _animator;

    public GameObject player;

    private bool _isHit = false;

    void OnTriggerEnter(Collider cd)
    {
        var tag = cd.tag;

        if ((tag == "Player") && (!_isHit))
        {
            _isHit = true;
            _animator.SetBool("hit", true);
            player.GetComponent<Score>().addPoints(1);
        }
    }

    public void DestroyRomb()
    {
        Destroy(gameObject);
    }

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {

    }
}
