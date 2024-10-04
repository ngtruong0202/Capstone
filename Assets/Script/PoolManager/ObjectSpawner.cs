
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public string objectTag;
    public Transform objectTransform;
    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        GameObject obj = PoolManager.Instance.GetPooledObject(objectTag);
    //        if (obj != null)
    //        {
    //            obj.transform.position = objectTransform.position;
    //            obj.transform.rotation = objectTransform.rotation;
    //            obj.SetActive(true);
    //        }
    //    }
    //}

    public void Test()
    {
        GameObject obj = PoolManager.Instance.GetPooledObject(objectTag);
        if (obj != null)
        {

            obj.transform.position = objectTransform.position;
            obj.transform.rotation = objectTransform.rotation;
            obj.SetActive(true);
        }
    }
}
