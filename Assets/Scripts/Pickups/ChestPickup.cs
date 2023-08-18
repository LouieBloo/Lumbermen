using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestPickup : Pickup
{
    [SerializeField]
    private ChestType type;

    public float gold;

    public RuntimeAnimatorController commonAnimationController;
    public RuntimeAnimatorController unCommonAnimationController;
    public enum ChestType { Common, Uncommon, Rare, Legendary}

    public GameObject goldText;

    // Start is called before the first frame update
    void Start()
    {
        switch (type)
        {
            case ChestType.Common:
                GetComponent<Animator>().runtimeAnimatorController= commonAnimationController;
                break;
            case ChestType.Uncommon:
                GetComponent<Animator>().runtimeAnimatorController = unCommonAnimationController;
                break;
        }
    }

    public override void pickedUp(GameObject pickerUpper)
    {

        GetComponent<Animator>().SetTrigger("Open");
        goldText.SetActive(true);
    }


    public void animationFinished()
    {
        if (gold > 0)
        {
            GameStats.Instance.modifyPlayerGold(gold);
        }
        Destroy(this.gameObject);
    }
}
