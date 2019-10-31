using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tweaks;
using Deform;

public class Leaf : MonoBehaviour
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
        float time = GetComponent<PlantGrowth>().time;

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
