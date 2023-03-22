using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelHolder : MonoBehaviour
{
    [SerializeField] private int numberOfLevel;
    [SerializeField] private List<Level> levels;
    [SerializeField] private DescriptionLevel descriptionLevel;

    private void Awake()
    {
        GameObject levelTemplate = transform.GetChild(0).gameObject;
        for(int i = 0; i < numberOfLevel; i++)
        {
            GameObject newLevel = Instantiate(levelTemplate, transform);

            newLevel.GetComponent<LevelController>().Level = levels[i];

            int index = i;
            newLevel.GetComponent<Button>().onClick.AddListener(() => PopUpPanel(index));
        }
        Destroy(levelTemplate);
    }

    private void PopUpPanel(int index)
    {
        descriptionLevel.Level = levels[index];
        descriptionLevel.gameObject.SetActive(true);
    }
}
