using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    public int slotId = 0;

    public Text slotButtonText;

    private bool selected;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();  
    }

    // Update is called once per frame
    void Update()
    {
        //set button slot number
        slotButtonText.text = (slotId+1).ToString();

        //check whether this slot is selected
        Player player = GetComponentInParent<Player>();
        selected = (player.GetComponent<InventoryManager>().selected == slotId);


        anim.SetBool("selected", selected);

        bool reloadable = false;

        bool magazine;
        magazine = (player.GetComponent<InventoryManager>().items[slotId].magazineData.cartridges > 0);

        bool reloadMode = player.GetComponent<InventoryManager>().reloadMode;

        if(magazine && reloadMode)
        {
            reloadable = true;
        }

        anim.SetBool("reloadable", reloadable);
    }
}
