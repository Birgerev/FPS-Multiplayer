using UnityEngine;
using UnityEngine.UI;
using net.bigdog.game.ui;


namespace net.bigdog.game.player.ui
{
    public class InventorySlotUI : MonoBehaviour
    {
        public int slotId = 0;

        public Text slotButtonText;
        public GameObject magazineSection;
        public ProgressBar magazineFullnessBar;


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
            slotButtonText.text = (slotId + 1).ToString();

            //check whether this slot is selected
            Player player = GetComponentInParent<Player>();
            selected = (player.GetComponent<InventoryManager>().selected == slotId);


            anim.SetBool("selected", selected);

            bool reloadable = false;

            bool magazine;
            magazine = (player.GetComponent<InventoryManager>().items[slotId].magazineData.cartridgeCapacity > 0);
            magazineSection.SetActive(magazine);
            magazineFullnessBar.value =
(float)player.GetComponent<InventoryManager>().items[slotId].magazineData.cartridges /
(float)player.GetComponent<InventoryManager>().items[slotId].magazineData.cartridgeCapacity;


            /*bool reloadMode = player.GetComponent<InventoryManager>().reloadMode;

            if (magazine && reloadMode)
            {
                reloadable = true;
            }

            anim.SetBool("reloadable", reloadable);*/
        }
    }
}
