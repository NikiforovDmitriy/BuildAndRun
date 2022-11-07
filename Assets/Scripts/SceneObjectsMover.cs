using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObjectsMover : MonoBehaviour
{
    public float speed = 50.0f;
    public GameObject guys;

    public ParticleSystem particles;

    public bool isMoving = false;

    void Update()
    {
        if (isMoving)
        {
            transform.Translate(0, 0, -speed * Time.deltaTime);
            int countOfGuys = guys.transform.childCount;
            int countOfGuysFinished = 0;
            for (int i = 0; i < countOfGuys; i++)
            {
                Guy guy = guys.transform.GetChild(i).gameObject.GetComponent<Guy>();
                if (guy.finished) countOfGuysFinished++;
            }
            if (countOfGuysFinished == countOfGuys)
            {
                isMoving = false;
                particles.Play();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                LetsGo();
            }
        }
    }
    public void LetsGo()
    {
        isMoving = true;
        int countOfGuys = guys.transform.childCount;
        for (int i = 0; i < countOfGuys; i++)
        {
            Guy guy = guys.transform.GetChild(i).gameObject.GetComponent<Guy>();
            guy.WalkAnim();
        }

    }
}


