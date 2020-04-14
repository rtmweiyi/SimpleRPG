using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager instance;
    void Awake()
    {
        instance = this;
    }
    Equipment[] currentEquipment;
    SkinnedMeshRenderer[] currentMeshes;

    public Equipment[] defaultItems;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged OnEquipmentChangedCallback;
    public SkinnedMeshRenderer targetMesh;
    void Start()
    {
        int numSlot = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlot];
        currentMeshes = new SkinnedMeshRenderer[numSlot];
        EquipDefaultItems();
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;
        Equipment oldItem = Unequip(slotIndex);
        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            Inventory.instance.Add(oldItem);
        }
        if (OnEquipmentChangedCallback != null)
        {
            OnEquipmentChangedCallback.Invoke(newItem, oldItem);
        }
        SetEquipmentBlendShapes(newItem,100);
        currentEquipment[slotIndex] = newItem;
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh);
        newMesh.transform.parent = targetMesh.transform;
        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;
        currentMeshes[slotIndex] = newMesh;
    }

    public Equipment Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            if(currentMeshes[slotIndex] != null){
                Destroy(currentMeshes[slotIndex].gameObject);
            }
            Equipment oldItem = currentEquipment[slotIndex];
            SetEquipmentBlendShapes(oldItem,0);
            Inventory.instance.Add(oldItem);
            if (OnEquipmentChangedCallback != null)
            {
                OnEquipmentChangedCallback.Invoke(null, oldItem);
            }
            currentEquipment[slotIndex] = null;
            return oldItem;
        }
        return null;
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
        EquipDefaultItems();
    }

void SetEquipmentBlendShapes(Equipment item,int weight){
    foreach (EquipmentMeshRegion blendShape in item.coveredMeshRegions)
    {
        targetMesh.SetBlendShapeWeight((int)blendShape,weight);
    }
}

void EquipDefaultItems(){
    foreach (Equipment item in defaultItems)
    {
        Equip(item);
    }
}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            UnequipAll();
        }
    }
}
