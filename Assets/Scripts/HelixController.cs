using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;

public class HelixController : MonoBehaviour

{


private Vector2 lastTapPosition;
private Vector3 startRotation;
public Transform topTransform;
public Transform goalTransform;
public GameObject helixLevelPrefab;
public List<Stage> allStages = new List<Stage>();
public float helixDistance;
private List<GameObject> spawnedLevels = new List<GameObject>();

    private void Awake()
    {
        startRotation = transform.localEulerAngles;
        helixDistance = topTransform.localPosition.y - (goalTransform.localPosition.y + 0.1f);
        LoadStage(0);
    }

     void Update()
    {
        if (Input.GetMouseButton(0))
        {
          Vector2 currentTapPosition = Input.mousePosition;
        if (lastTapPosition == Vector2.zero)
        {
            lastTapPosition = currentTapPosition;
        }
            float distance = lastTapPosition.x - currentTapPosition.x;
            lastTapPosition = currentTapPosition;
            transform.Rotate(Vector3.up * distance * 0.1f, Space.World);
        }
        if (Input.GetMouseButtonUp(0))
        {
            lastTapPosition = Vector2.zero;
        }
    }

    public void LoadStage(int stageNumbrer)
    {
        Stage stage = allStages[Mathf.Clamp(stageNumbrer, 0, allStages.Count - 1)];
        if(stage == null)
        {
           Debug.Log("sin niveles");
           return;
        }
        Camera.main.backgroundColor = allStages[stageNumbrer].stageBackgroundColor;
        Object.FindFirstObjectByType<BallController>().GetComponent<Renderer>().material.color = allStages[stageNumbrer].stageBallColor;
        transform.localEulerAngles = startRotation;
        foreach (GameObject go in spawnedLevels)
        {
            Destroy(go);
        }
        float levelDistance = helixDistance / stage.levels.Count;
        float spawnPosY = topTransform.localPosition.y;
      
           
        for (int i = 0; i < stage.levels.Count; i++)
              
        {
            spawnPosY -= levelDistance; 
            GameObject level = Instantiate(helixLevelPrefab, transform);
            level.transform.localPosition = new Vector3(0, spawnPosY, 0);
            spawnedLevels.Add(level);
            int partsToDisable = 12 - stage.levels[i].partCount;

            List<GameObject> disabledParts = new List<GameObject>();

            while (disabledParts.Count < partsToDisable)
            {
             GameObject randomPart = level.transform.GetChild(Random.Range(0, level.transform.childCount)).gameObject;
             if (!disabledParts.Contains(randomPart))
             {
                randomPart.SetActive(false);
                disabledParts.Add(randomPart);
             }
            }
            List<GameObject> leftparts = new List<GameObject>();
            foreach (Transform t in level.transform)
            {
                t.GetComponent<Renderer>().material.color = allStages[stageNumbrer].stageLevelPartColor;
                if (t.gameObject.activeInHierarchy)
                {
                    leftparts.Add(t.gameObject);
                }
            }
            List<GameObject>deathParts = new List<GameObject>();

            while (deathParts.Count < stage.levels[i].deathPartCount)
            {
              GameObject randomPart = leftparts[Random.Range(0, leftparts.Count)];

              if (!deathParts.Contains(randomPart))
              {
                randomPart.gameObject.AddComponent<DeathPart>();
                deathParts.Add(randomPart);
       
              }

            }
           }

    }
}
