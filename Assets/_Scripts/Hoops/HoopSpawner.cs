using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HoopPooling
{
    public GameObject hoop;
    public int count;
    public bool expandable;
}

public class HoopSpawner : MonoSingleton<HoopSpawner>
{
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private HoopPooling hoopPooling;
    [SerializeField]
    private Transform holder;
    [SerializeField]
    private List<GameObject> pooledHoops;

    protected override void Awake()
    {
        base.Awake();
        InitHoopPoolList();
    }

    private void InitHoopPoolList()
    {

        for (int i = 0; i < hoopPooling.count; i++)
        {
            pooledHoops.Add(CreateGobject(hoopPooling.hoop));
        }
    }

    private GameObject CreateGobject(GameObject item)
    {
        GameObject gobject = Instantiate(item, holder);
        gobject.SetActive(false);
        return gobject;
    }

    public GameObject Spawn()
    {
        for (int i = 0; i < pooledHoops.Count; i++)
        {
            if (!pooledHoops[i].activeSelf)
            {
                pooledHoops[i].transform.position = mainCamera.transform.position + new Vector3(4f, Random.Range(-1, 1), 10);
                pooledHoops[i].SetActive(true);
                return pooledHoops[i];
            }
        }

        if(hoopPooling.expandable)
        {
            GameObject newHoop = CreateGobject(hoopPooling.hoop);
            pooledHoops.Add(newHoop);
            newHoop.SetActive(true);
            return newHoop;
        }
        return null;
    }

    public void DisableHoopStatus()
    {
        for(int i = 0; i < hoopPooling.count; i++)
        {
            if(pooledHoops[i].activeSelf)
            {
                pooledHoops[i].SetActive(false);
            }
        }
    }
}
