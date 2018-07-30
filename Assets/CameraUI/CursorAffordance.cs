using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraRaycaster))]
public class CursorAffordance : MonoBehaviour {
    [SerializeField] Texture2D walkCursor = null;
    [SerializeField] Texture2D targetCursor = null;
    [SerializeField] Texture2D unknownCursor = null;
    [SerializeField] Vector2 cursorHotSpot = new Vector2(0, 0);

   const int  walkableLayer = 9;
   const int enemyLayer = 10;

    private CameraRaycaster cameraRaycaster;

	// Use this for initialization
	void Start () {
        //cameraRaycaster = GameObject.FindObjectOfType<CameraRaycaster>();
        cameraRaycaster = GetComponent<CameraRaycaster>();
        cameraRaycaster.notifyLayerChangeObservers += OnLayerChanged;
    }
	
	// Update is called once per frame
	void OnLayerChanged (int newLayer) {
       // switch (cameraRaycaster.layerHit)
         switch (newLayer)
        {           
            case walkableLayer:
                Cursor.SetCursor(walkCursor, cursorHotSpot, CursorMode.Auto);
                break;
            case enemyLayer:
                Cursor.SetCursor(targetCursor, cursorHotSpot, CursorMode.Auto);
                break;
            default:
                Cursor.SetCursor(unknownCursor, cursorHotSpot, CursorMode.Auto);
                return;
        }
    }
}
