using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaBench : MonoBehaviour
{
    [HideInInspector] public List<Bench> Benches;

    void Start() {
        for (int i = transform.childCount - 1; i >= 0; i--)
            Benches.Add(transform.GetChild(i).GetChild(0).GetComponent<Bench>()); ;
    }

    public Bench GetAvailableBench()
    {
        foreach (Bench bench in Benches) 
            if (bench.Pokemon == null) 
                return bench;
        return null;
    }
}
