using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour
{
    public float SweepingInTime = 0.5f;
    private float SweepingTimer;
    private bool isSweeping = false;
    private Vector3 onMouseDownDeltaToMouse = new Vector3();
    private Camera theOneAndOnlyCamera;
    private Vector3 mouseWorldPosition = new Vector3();
    private bool isOnAugment = false;
    private UnenmployedFans unenmployedFansScript;

    //
    public GameObject ProfileToMove;
    private Vector3 ProfileTargetPosition;
    private TransformHolder transformHolderReference;
    private SceneToSceneDataKeeper dataKeeper;

    // Drag and Drop visual feedbacks
    public GameObject DragDropFeedback;
    private bool isHovered;
    public float onMouseOverScaleDuration;
    public float onMouseOverScaleAmount;
    private float scaleTimer;


    private void Start()
    {
        theOneAndOnlyCamera = FindObjectOfType<Camera>();
        unenmployedFansScript = FindObjectOfType<UnenmployedFans>();
        dataKeeper = FindObjectOfType<SceneToSceneDataKeeper>();
    }

    private void OnMouseDown()
    {
        //transition towards mouse
        SweepingTimer = 0.0f;
        isSweeping = true;
        Vector3 mouse3DWorldPosition = theOneAndOnlyCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3 Mouse2DWorldPosition = new Vector3(mouse3DWorldPosition.x, mouse3DWorldPosition.y, transform.position.z);
        onMouseDownDeltaToMouse = Mouse2DWorldPosition - this.transform.position;

        // move user from augment to unemployed
        dataKeeper.MoveUserInCommunityList(this.GetComponent<AssociatedTreeNode>().associatedNode, 0);
    }



    private void OnMouseDrag()
    {
        mouseWorldPosition = theOneAndOnlyCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3 newObjectPosition = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, transform.position.z);

        if (isSweeping)
        {
            transform.position = Vector3.Lerp(newObjectPosition - onMouseDownDeltaToMouse, newObjectPosition, SweepingTimer / SweepingInTime);
        }
        else
        {
            transform.position = newObjectPosition;
            if (isOnAugment)
            {
                ProfileToMove.transform.position = ProfileTargetPosition;
                DragDropFeedback.SetActive(true);
            }
            else
            {
                DragDropFeedback.SetActive(false);
            }
        }
    }

    private void Update()
    {
        //moves the object under the mouse at start
        if (isSweeping)
        {
            SweepingTimer += Time.deltaTime;
            if (SweepingTimer >= SweepingInTime)
            {
                isSweeping = false;
            }
        }

        // on mouse Over : makes the item scale
        if (isHovered)
        {
            scaleTimer = Mathf.Clamp(scaleTimer + Time.deltaTime, 0, onMouseOverScaleDuration);
        }
        else
        {
            scaleTimer = Mathf.Clamp(scaleTimer - Time.deltaTime, 0, onMouseOverScaleDuration);
        }
        float timeRatio = scaleTimer / onMouseOverScaleDuration;
        float scaleRatio = Mathf.Lerp(1, onMouseOverScaleAmount, timeRatio);
        ProfileToMove.transform.localScale = new Vector3(scaleRatio, scaleRatio, 1);
    }

    private void OnTriggerStay2D(Collider2D augmentCollider)
    {
        isOnAugment = true;
        transformHolderReference = augmentCollider.GetComponentInChildren<TransformHolder>();
        int targetSlot = transformHolderReference.GetUsedSlots();
        Vector3 coordToDisplayFanIn = new();
        if (transformHolderReference.TransformList.Count > targetSlot)
        {
            coordToDisplayFanIn = transformHolderReference.TransformList[targetSlot].transform.position;
        }
        else
        {
            coordToDisplayFanIn = transform.position;
        }
        ProfileTargetPosition = new Vector3(coordToDisplayFanIn.x, coordToDisplayFanIn.y, transform.position.z);
        //Debug.Log("Is in trigger");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isOnAugment = false;
        ProfileToMove.transform.position = transform.position;
    }

    private void OnMouseUp()
    {
        DragDropFeedback.SetActive(false);
        if (isOnAugment && transformHolderReference.GetUsedSlots() < transformHolderReference.TransformList.Count)
        {
            transform.position = ProfileTargetPosition;
            //transformHolderReference.usedSlots += 1;
            ProfileToMove.transform.position = transform.position;
            dataKeeper.MoveUserInCommunityList(GetComponent<AssociatedTreeNode>().associatedNode, transformHolderReference.AugmentIndex);
            //dataKeeper.PrintCommunityListsAmounts();
            FindObjectOfType<EndFirstLoop>().EndTheFirstLoop();
        }
        else
        {
            isOnAugment = false;
            ProfileToMove.transform.position = transform.position;
            dataKeeper.MoveUserInCommunityList(GetComponent<AssociatedTreeNode>().associatedNode, 0);
        }

        //remettre à sa place dans les unemployed fans
        if (unenmployedFansScript != null)
        {
            unenmployedFansScript.ReorderFans();
        }
    }

    private void OnMouseOver()
    {
        isHovered = true;
    }

    private void OnMouseExit()
    {
        isHovered = false;
    }

}
