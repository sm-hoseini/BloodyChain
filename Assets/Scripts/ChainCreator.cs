using UnityEngine;
using System.Collections;

public class ChainCreator : MonoBehaviour
{

    public Transform chainObj;
    public Transform hookObj;

    Transform[] chain;
    float chainRingHeight;

    Transform target;

    void Start()
    {
        chainRingHeight = chainObj.GetComponent<Renderer>().bounds.size.x;
        chain = new Transform[100];

        target = Instantiate(chainObj);
    }

    public void Create(Vector3 startPoint, Vector3 endPoint, float fixedY)
    {
        startPoint.z = 0;
        endPoint.z = 0;

        target.position = endPoint;
        if (chain != null && chain.Length > 0)
        {
            foreach (Transform tr in chain)
            {
                if (tr != null)
                    tr.gameObject.SetActive(false);
            }
        }
        float distance = Vector3.Distance(startPoint, endPoint);
        int numberOfRings = Mathf.CeilToInt(distance / chainRingHeight);
        for (int i = 0; i < numberOfRings; i++)
        {
            if (i == 0)
            {
                chain[0] = hookObj;
            }
            else
            {
                if (chain[i] == null)
                {
                    chain[i] = Instantiate(chainObj);
                    chain[i].parent = transform;
                }
                else
                {
                    chain[i].gameObject.SetActive(true);
                }
            }

            float xx = endPoint.x - startPoint.x;
            float zz = endPoint.y - startPoint.y;


            chain[i].position = startPoint + new Vector3(i * chainRingHeight * (xx / distance), i * chainRingHeight * (zz / distance),0 );

           // chain[i].LookAt(target);
            Quaternion rot = Quaternion.FromToRotation(chain[i].transform.right * -1, target.position - chain[i].position);
            chain[i].rotation = rot;

            chain[i].gameObject.SetActive(true);
        }

        /*Quaternion rot = Quaternion.LookRotation(endPoint - startPoint, Vector3.forward);
        rot.eulerAngles = new Vector3(90, rot.eulerAngles.y, rot.eulerAngles.z);
        transform.rotation = rot;*/

        /*chain[0].transform.position = startPoint;
        chain[numberOfRings - 1].transform.position = endPoint;

        for (int i = 1; i < (numberOfRings - 1); i++)
        {
            chain[i].position = chain[i - 1].rotation.eulerAngles;// * (i*chainRingHeight);
        }*/
      //  UnityEditor.EditorApplication.isPaused = true;
    }

    public void DisableChain()
    {
        if (chain != null && chain.Length > 0)
        {
            foreach (Transform tr in chain)
            {
                if (tr != null)
                    tr.gameObject.SetActive(false);
            }
        }
    }
}
