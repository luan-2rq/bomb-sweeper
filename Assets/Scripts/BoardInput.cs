using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Esse script lida com os toques no board
public class BoardInput : MonoBehaviour
{

    private static Camera cam;

    private static BoardInput instance;
    public static BoardInput Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        cam = Camera.main;
    }

    public static BreakableTile hasClickedTile()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider.CompareTag("Tile"))
                {
                    return hit.collider.gameObject.GetComponent<BreakableTile>();
                }
            }
        }
        return null;
    }

}
