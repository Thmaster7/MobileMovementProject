using UnityEngine;

public class Item : MonoBehaviour
{
    private Camera mainCamera;
    private bool isDragging = false;
    private Transform selectedObject;
    private Vector3 offset;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = mainCamera.ScreenPointToRay(touch.position);
            RaycastHit hit;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.CompareTag("Draggable")) // Asegúrate de poner este tag en el objeto
                        {
                            selectedObject = hit.collider.transform;
                            offset = selectedObject.position - GetWorldPosition(touch.position);
                            isDragging = true;
                        }
                    }
                    break;

                case TouchPhase.Moved:
                    if (isDragging && selectedObject != null)
                    {
                        selectedObject.position = GetWorldPosition(touch.position) + offset;
                    }
                    break;

                case TouchPhase.Ended:
                    isDragging = false;
                    selectedObject = null;
                    break;
            }
        }
    }

    private Vector3 GetWorldPosition(Vector2 screenPosition)
    {
        Vector3 screenPoint = new Vector3(screenPosition.x, screenPosition.y, mainCamera.WorldToScreenPoint(transform.position).z);
        return mainCamera.ScreenToWorldPoint(screenPoint);
    }

}
