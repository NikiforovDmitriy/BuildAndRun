using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guy : MonoBehaviour
{
    public bool finished = false;

    private Animator _animator;
    private Vector3 _movePoint;

    private float formationTime = 3.0f;

    Vector3 _startPos;

    private bool _setNewPos = false;

    private float _currentTime = 0.0f;
    private float _normalizedDelta;
    private SkinnedMeshRenderer _renderer;

    void Start()
    {
        _startPos = transform.position;
        _animator = GetComponent<Animator>();
        _renderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    public void WalkAnim()
    {
        _animator.SetBool("walking", true);
    }

    public void Move(Vector3 mp)
    {
        _movePoint = new Vector3(mp.x, _startPos.y, mp.z);
        _currentTime = 0.0f;
        _setNewPos = true;
    }

    public void Update()
    {
        if (_setNewPos)
        {
            Vector3 originalPosition = transform.position;
            Vector3 destinationPosition = _movePoint;
            if (_currentTime < formationTime)
            {
                _currentTime += Time.deltaTime;
                _normalizedDelta = _currentTime / formationTime;
                transform.position = Vector3.Lerp(originalPosition, destinationPosition, _normalizedDelta);
            }
            else
            {
                _currentTime = 0.0f;
                _setNewPos = false;
            }
        }
    }

    void OnTriggerEnter(Collider cd)
    {
        var tag = cd.tag;

        if (tag == "Finish")
        {
            _animator.SetBool("celebrating", true);
            finished = true;
        }
    }

    void OnCollisionEnter(Collision cd)
    {
        var go = cd.gameObject;
        var tag = go.tag;
        if (tag == "Enemy")
        {
            _animator.SetBool("die", true);
            Debug.Log("Enemy!");
        }
        else if (tag == "NewPlayer")
        {
            var materials = _renderer.materials;
            go.GetComponentInChildren<SkinnedMeshRenderer>().materials = materials;
            go.transform.SetParent(transform.parent);
            go.GetComponent<Guy>().WalkAnim();
            int layerName = LayerMask.NameToLayer("Player");
            go.layer = layerName;
        }
    }

    public void DestroyGuy()
    {
        Destroy(gameObject);
    }
}
