using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaBench : MonoBehaviour
{
    [HideInInspector] public List<Bench> Benches;

    void Start() {
        for (int i = 0; i < transform.childCount; i++)
            Benches.Add(transform.GetChild(i).GetComponent<Bench>()); ;
    }

    public Bench GetAvailableBench()
    {
        foreach (Bench bench in Benches) if (bench.Pokemon == null) return bench;
        return null;
    }
}
