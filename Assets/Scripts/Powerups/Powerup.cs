using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : Item
{
    [SerializeField]
    private float lifeTime = 0;
    private float lifeTimeTimer = 0;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pickedUp && lifeTime > 0) {
            lifeTimeTimer += Time.deltaTime;
            if(lifeTimeTimer > lifeTime) {
                //remove our modifications and die
                unit.equipmentHolder.removeItem(this);
                Destroy(gameObject);
            }
        }
    }

    public override void equipped(Unit unit, CreatureAnimatorHelper animationHelper)
    {
        base.equipped(unit, animationHelper);
        spriteRenderer.enabled = false;
    }
}
