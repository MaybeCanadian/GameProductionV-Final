using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildingManagerScript : MonoBehaviour
{
    public static BuildingManagerScript instance;

    public Transform buildingParent;

    public Camera mainCamera;

    public LayerMask cameraRayCastLayer;

    public bool unPlaced = true;

    private void Awake()
    {
        if(instance != this && instance != null)
        {
            Destroy(this); 
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public void EnterBuildingMode(BuildingRecipes recipe)
    {
        GameStateMachine.GetInstance().ChangeState(GameStates.BUILDING);

        GameObject building = Instantiate(recipe.building.building, buildingParent);

        building.GetComponent<Rigidbody>().isKinematic = true;

        StartCoroutine("placeBuilding", building);
    }

    private IEnumerator placeBuilding(GameObject building)
    {
        unPlaced = true;

        while(unPlaced)
        {
            Ray MouseRay = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if(Physics.Raycast(MouseRay, out RaycastHit HitInfo, maxDistance: 300f, cameraRayCastLayer)) {
                Vector3 position = HitInfo.point;

                building.transform.position = position;
            }

            yield return null;
        }

        BuildingScript buildScript = building.GetComponent<BuildingScript>();

        //building.GetComponent<Rigidbody>().isKinematic = false;

        buildScript.Activate();

        GameStateMachine.GetInstance().ChangeState(GameStates.GAME);

        yield break;
    }

    public void OnMainButtonPressed()
    {
        if(unPlaced == true)
        {
            unPlaced = false;
        }
    }
}
