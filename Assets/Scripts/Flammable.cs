using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flammable : MonoBehaviour
{

    [SerializeField] GameObject flamePrefab;
    [SerializeField] bool CanDie = false;
    bool flameCreated = false;
    //Flammable myFlammable;
    [SerializeField] Vector3 offset;
    public bool IsOn = false;
    GameObject flame;
    public bool flameOnGameStart = false;



    private void Start()
    {
        if (flameOnGameStart)
            FlameOn();
    }

    public void FlameOn()
    {
        if (!IsOn && !flameCreated)
        {
            IsOn = true;
            flameCreated = true;
            flame = Instantiate(
                flamePrefab,
                transform.position + offset,
                Quaternion.identity, this.gameObject.transform);
            //Vector3 newSize = Flame.transform.localScale*2;
            flame.transform.localScale = flame.transform.localScale * 2;
            if (CanDie)
            {
                StartCoroutine(KillAfterTime(flame));
            }
            
        }
        else if (!IsOn && flameCreated)
        {
            if (flame != null)
            {
                FlameV2 flameComponenet = GetComponentInChildren<FlameV2>();
                //StartCoroutine(flameComponenet.FlameStart());
                flameComponenet.FlameStart();
            }
            else
                Debug.Log("myflame isnull");
        }
        
    }

    IEnumerator KillAfterTime(GameObject flame)
    {
        IsOn = false;
        yield return new WaitForSeconds(3);
        Destroy(flame);
        //Destroy(gameObject);
    }
}
