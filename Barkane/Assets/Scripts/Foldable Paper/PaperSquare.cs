using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[RequireComponent(typeof(EdgeParticles))]
public class PaperSquare : MonoBehaviour
{

    [SerializeField] private bool playerOccupied = false; //true if the player is on this square
    public bool PlayerOccupied { get => playerOccupied;}

    
    //Visuals
    public float paperLength = 2f;
    public float paperThickness = 0.001f;
    [SerializeField] private GameObject topHalf;
    public GameObject TopHalf => topHalf;
    [SerializeField] private GameObject bottomHalf;
    public GameObject BottomHalf => bottomHalf;
    [SerializeField] private EdgeParticles edgeParticles;

#if UNITY_EDITOR
    public Orientation orientation;
    public List<PaperJoint> adjacentJoints;
#endif

    private void Start() 
    {
        edgeParticles = GetComponent<EdgeParticles>();
    }

    public void SetPlayerOccupied(bool value)
    {
        playerOccupied = value;
    }

    //select is true when this region is selected and false when deselected
    public void OnFoldHighlight(bool select)
    {
        if(select)
            edgeParticles?.Emit();
        else
            edgeParticles?.Unemit();
    }

    //foldStart is true when starting a fold and false when ending a fold
    public void OnFold(bool foldStart)
    {
    }

#if UNITY_EDITOR
    public void AddJoint(PaperJoint joint)
    {
        adjacentJoints.Add(joint);
    }

    public void RemoveAdjacentJoints()
    {
        while(adjacentJoints.Count > 0)
        {
            adjacentJoints[0]?.Remove();
        }
    }
#endif

    private void OnValidate()
    {
        Vector3 offset = this.transform.rotation * new Vector3(0, paperThickness / 2, 0);
        if (topHalf) topHalf.transform.position = this.transform.position + offset;
        if (bottomHalf) bottomHalf.transform.position = this.transform.position - offset;
    }
}