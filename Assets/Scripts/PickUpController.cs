using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpController : MonoBehaviour
{
    public Transform weaponContainer;

    public StarterAssets.StarterAssetsInputs inputs;

    private Transform currentItem;
    private Rigidbody itemRB;
    private BoxCollider itemCollider;

    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;

    [SerializeField]
    private bool interactPressed = false, dropPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        if (equipped)
        {
            itemRB = weaponContainer.GetComponentInChildren<Rigidbody>();
            itemCollider = weaponContainer.GetComponentInChildren<BoxCollider>();

            itemRB.isKinematic = true;
            itemCollider.isTrigger = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        interactPressed = inputs.interacting;
        dropPressed = inputs.dropping;

        if (equipped && dropPressed)
            Drop();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            if (!equipped && interactPressed && !slotFull)
            {
                currentItem = other.transform;
                itemRB = other.GetComponent<Rigidbody>();
                itemCollider = other.GetComponent<BoxCollider>();
                PickUp();
            }
                
        }
        
    }

    private void PickUp()
    {
        equipped = true;
        slotFull = true;

        currentItem.SetParent(weaponContainer);
        currentItem.localPosition = Vector3.zero;
        currentItem.localRotation = Quaternion.Euler(Vector3.zero);
        currentItem.localScale = Vector3.one;

        itemRB.isKinematic = true;

        inputs.dropping = false;
    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;
        currentItem.position += transform.forward * 1;
        currentItem.SetParent(null);

        itemRB.velocity = GetComponent<CharacterController>().velocity;

        itemRB.AddForce(transform.forward * dropForwardForce, ForceMode.Impulse);
        itemRB.AddForce(transform.up * dropUpwardForce, ForceMode.Impulse);

        float random = Random.Range(-1, 1);
        itemRB.AddTorque(new Vector3(random, random, random) * 10);

        itemRB.isKinematic = false;

        inputs.interacting = false;
    }

}
