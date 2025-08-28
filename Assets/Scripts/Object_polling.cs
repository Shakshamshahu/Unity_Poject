using System.Collections.Generic;
using UnityEngine;

public class Object_polling : MonoBehaviour
{
    public GameObject pollingObject;
    public int polSize;
    public List<GameObject> pollingList;
    // Start is called before the first frame update
    void Start()
    {
        pollingList = new List<GameObject>();
        for (int i = 0; i < polSize; i++)
        {
            GameObject polObj = Instantiate(pollingObject);
            polObj.SetActive(false);
            pollingList.Add(polObj);
        }

    }

    public GameObject ObjectPol()
    {
        foreach (GameObject obj in pollingList)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        GameObject newBullet = Instantiate(pollingObject);
        newBullet.SetActive(true);
        pollingList.Add(newBullet);
        return newBullet;
    }

}
