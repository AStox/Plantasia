using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tweaks;


/*
    Needs:
        Needs to be different everytime
        Needs to just have time as an input (and a random seed?)
        Needs to alter the stalk, leaves, branches, and all attached deformers

    Each component has an animation curve attached to alter one aspect of that component over time.
    Each component will have a start and end state. The animation curve transitions between the two.
    The plant will be divided into smaller stalks, making up the main stalk and branches in segments.
    At the end of each segment there will be a node. Nodes will be where new growths can occur.
    Each new growth will be a stalk segment with two leaves and a node at the end. Two new growths will have a random chance of occuring, creating a new branch.
    The randomness comes from randomly setting the end states of all components and randomly setting the axial rotation of the nodes.

 */
// public struct 



/*
    
 */

public class PlantGrowth : MonoBehaviour
{
    float _time = 0;
    public float time;
    public Deform.Deformer[] allDeformers;

    public Deform.Deformer[] randomDeformers;
    public Deform.Deformer[] continuousDeformers;
    public Deform.Deformer[] dailyDeformers;

    Tweaks.DeformerTweaks deformerTweaks;
    Vector3 originalPos;
    public float maxTime;

    void Start()
    {
        time = 0;
        originalPos = transform.position;
        // originalPos = new Vector3(0,-0.5f,0);
        deformerTweaks = new Tweaks.DeformerTweaks();
        foreach(Deform.Deformer deformer in randomDeformers){
            deformerTweaks.Tweak(deformer, Random.Range(-1f, 1f));
        }
        transform.parent.rotation = Quaternion.Euler(new Vector3(transform.rotation.x,Random.Range(0f,360f),transform.rotation.z ));
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(_time);
        // GetComponent<Generator>().height = time * 0.01f;
        // transform.position += Vector3.up * _time * 0.0001f;
        if (maxTime > 0 && time > maxTime) {
            _time = 0;
        }
        UpdateTime();

        // Deform.Deformer.
    }

    void UpdateTime() {
        _time += 4f * Time.deltaTime;
        // time = Mathf.FloorToInt(_time);
        time = _time;
    }
}
