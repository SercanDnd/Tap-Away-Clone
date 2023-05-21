using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Net;
using JetBrains.Annotations;

public class TapController : MonoBehaviour
{
    public int tapId;


    private void Update()
    {
        GetTapCube();
    }


    public void GetTapCube()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hits;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            List<GameObject> cubeList = new List<GameObject>();
            if (Physics.Raycast(ray, out hits, Mathf.Infinity))
            {
                if (hits.transform.CompareTag("Cube"))
                {
                    if (!cubeList.Contains(hits.transform.gameObject))
                    {
                        cubeList.Add(hits.transform.gameObject);
                        
                    }
                }
            }
            if (cubeList.Count > 0)
            {
                cubeList[0].GetComponent<CubeController>().Move();
            }
           
        }
        
    }
}
