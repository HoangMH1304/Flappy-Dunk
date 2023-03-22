using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public static ShopController Instance;

    [SerializeField] private Ball[] balls;
    [SerializeField] private Wing[] wings;
    [SerializeField] private Hoop[] hoops;
    [SerializeField] private Flame[] flames;

    [SerializeField] private Transform ballContent, wingContent, hoopContent, flameContent;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitBallItems();
        InitWingItems();
        InitHoopItems();
        InitFlameItems();
    }

    public void InitBallItems()
    {
        GameObject templateObject = ballContent.GetChild(0).gameObject;
        foreach (var item in balls)
        {
            GameObject itemObject = Instantiate(templateObject, ballContent);
            itemObject.GetComponent<BallDisplay>().Show(item);
        }
        Destroy(templateObject);
    }

    public void InitWingItems()
    {
        GameObject templateObject = wingContent.GetChild(0).gameObject;
        foreach (var item in wings)
        {
            GameObject itemObject = Instantiate(templateObject, wingContent);
            itemObject.GetComponent<WingDisplay>().Show(item);
        }
        Destroy(templateObject);
    }

    public void InitHoopItems()
    {
        GameObject templateObject = hoopContent.GetChild(0).gameObject;
        foreach (var item in hoops)
        {
            GameObject itemObject = Instantiate(templateObject, hoopContent);
            itemObject.GetComponent<HoopDisplay>().Show(item);
        }
        Destroy(templateObject);
    }

    public void InitFlameItems()
    {
        GameObject templateObject = flameContent.GetChild(0).gameObject;
        foreach (var item in flames)
        {
            GameObject itemObject = Instantiate(templateObject, flameContent);
            itemObject.GetComponent<FlameDisplay>().Show(item);
        }
        Destroy(templateObject);
    }




}
