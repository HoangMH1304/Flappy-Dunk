using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopManager : MonoBehaviour
{
    public static HoopManager Instance;
    [SerializeField] private float distance;
    [SerializeField] private List<GameObject> hoops;

    private void Awake() 
    {
        if(Instance == null)
        {
            Instance = this;
        }    
        else
        {
            Destroy(gameObject);
        }
        InitialState();
    }

    public void InitialState()
    {
        for(int i = 0; i < hoops.Count; i++)
        {
            hoops[i].SetActive(false);
            hoops[i].GetComponent<HoopController>().ActiveColor(false);
        }
        hoops[0].SetActive(true);
        hoops[0].GetComponent<HoopController>().ActiveColor();
        hoops[0].transform.position = new Vector3(1.5f, 0, 0);


        hoops.Add(hoops[0]);
        hoops.RemoveAt(0);

        hoops[0].SetActive(true);
        hoops[0].GetComponent<HoopController>().DeactiveColor();
        hoops[0].transform.position = new Vector3(1.5f + distance, GetVerticalPosition(), 0);

        hoops.Add(hoops[0]);
        hoops.RemoveAt(0);
    }

    public float GetHorizontalPosition()
    {
        return hoops[hoops.Count - 2].transform.position.x + distance;
    }

    public float GetVerticalPosition()
    {
        return (float)Random.Range(-2f, 2f);
    }

    public GameObject GetReadyHoop()
    {
        // for (int i = 0; i < hoops.Count; i++)
        // {
        //     if (!hoops[i].activeInHierarchy)
        //     {
        //         hoops.Add(hoops[i]);
        //         hoops.RemoveAt(0);
        //         return hoops[hoops.Count - 1];
        //     }
        // }
        // return null;

        //test below code(may be similar with up)

        hoops.Add(hoops[0]);
        hoops.RemoveAt(0);
        hoops[hoops.Count - 2].GetComponent<HoopController>().ActiveColor();
        return hoops[hoops.Count - 1];
    }

    public void FadeAllHoops(float endValue, float time)
    {
        foreach(GameObject obj in hoops)
        {
            obj.GetComponent<HoopController>().Fade(endValue, time);
        }
    }

    public float GetRevivePosition()
    {
        for(int i = 0; i < hoops.Count; i++)
        {
            if(hoops[i].activeInHierarchy)
            {
                hoops[i].GetComponent<HoopController>().InitState();
                return hoops[i].transform.position.x - distance;
            }
        }
        return 0;
    }

    public void TurnOffCollider()
    {
        foreach(var hoop in hoops)
        {
            hoop.GetComponent<HoopController>().DeactiveColor(false);
        }
    }

    public void TurnOnCollider()
    {
        foreach(var hoop in hoops)
        {
            hoop.GetComponent<HoopController>().ActiveColor(false);
        }
    }
}
