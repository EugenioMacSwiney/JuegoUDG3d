using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;

public class NewMonoBehaviourScript : MonoBehaviour

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
        FindFirstObjectByType<BallController>().GetComponent<Renderer>().material.color = allStages[stageNumbrer].stageBallColor;
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
        }


        }  
    }  
       