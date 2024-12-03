using UnityEngine;

public class CreatureBookView : MonoBehaviour
{
    private string creatureName;
    private CreatureSpawner CreatureSpawner;
    private void Awake()
    {
        creatureName = gameObject.name;
    }

    private void Start()
    {
        CreatureSpawner = SL.Instance.GetService<CreatureSpawner>();
    }


    private void OnMouseDown()
    {
        CreatureSpawner.SpawnCreature(creatureName);
    }
}
