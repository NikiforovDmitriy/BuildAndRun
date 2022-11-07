using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathMover : MonoBehaviour
{

    private float _curveLength;

    private float _globalOffsetZ = 0;

    public float scaleX = 14f;

    public float scaleZ = 25f;

    public float offsetZ = -16f;

    public GameObject guys;
    void Start()
    {
        FindObjectOfType<LinesRender>().OnNewPathCreated += SetPoints;
    }

    public void setGlobalOffsetZ(float offset)
    {
        _globalOffsetZ = offset;
    }

    private void SetPoints(List<Vector3> points)
    {
        _curveLength = 0;
        for (int i = 0; i < points.Count - 1; i++)
        {
            Vector3 v0 = points[i];
            Vector3 v1 = points[i + 1];
            _curveLength += (v1 - v0).magnitude;
        }
        int countOfGuys = guys.transform.childCount;
        float distanceBeetwenGuys = _curveLength / countOfGuys;
        Vector3[] array = points.ToArray();
        for (int i = 0; i < countOfGuys; i++)
        {
            if (i == 0)
            {
                var point = FindPoint(array, _curveLength, 0.1f);
                Guy guy = guys.transform.GetChild(0).gameObject.GetComponent<Guy>();
                Vector3 newPos = new Vector3(point.x * scaleX, 0, point.z * scaleZ + offsetZ - (_globalOffsetZ * scaleZ));
                guy.Move(newPos);
            }
            else
            {
                var percentsOfDistanse = (i * distanceBeetwenGuys) / _curveLength;
                var point = FindPoint(array, _curveLength, percentsOfDistanse);
                Guy guy = guys.transform.GetChild(i).gameObject.GetComponent<Guy>();
                Vector3 newPos = new Vector3(point.x * scaleX, 0, point.z * scaleZ + offsetZ - (_globalOffsetZ * scaleZ));
                guy.Move(newPos);
            }
        }
    }

    private Vector3 FindPoint(Vector3[] vectors /* your vector array */, float length /* length of curve */, float p /* percentage along the line 0 to 1 */)
    {
        if (vectors.Length < 1)
        {
            return Vector3.zero; // if the list is empty
        }
        else if (vectors.Length < 2)
        {
            return vectors[0]; //if there is only one point in the list
        }

        float dist = length * Mathf.Clamp(p, 0, 1);
        Vector3 pos = vectors[0];

        for (int i = 0; i < vectors.Length - 1; i++)
        {
            Vector3 v0 = vectors[i];
            Vector3 v1 = vectors[i + 1];
            float this_dist = (v1 - v0).magnitude;

            if (this_dist < dist)
            {
                dist -= this_dist; //if the remaining distance is more than the distance between these vectors then minus the current distance from the remaining and go to the next vector
                continue;
            }

            return Vector3.Lerp(v0, v1, dist / this_dist); //if the distance between these vectors is more or equal to the remaining distance then find how far along the gap it is and return
        }
        return vectors[vectors.Length - 1];
    }
}
