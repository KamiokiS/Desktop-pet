using System.Collections.Generic;
using UnityEngine;

public class CreatureSpawner : MonoBehaviour
{
    public List<Creature> PrefabsCreatures;
    [SerializeField] private List<Creature> SpawnedCreatures = new();

    private Transform _spawnPoint;

    private void Awake()
    {
        _spawnPoint = GameObject.FindGameObjectWithTag("Spawn point").transform;
        SL.Instance.RegisterService(this);
    }


    public void SpawnCreature(string creatureName)
    {
        if (isCreatureAlreadySpawned(creatureName))
        {
            Debug.Log("Это существо уже живет у вас на экране");
            return;
        }


        foreach (Creature creature in PrefabsCreatures)
        {
            if (creature.name == creatureName)
            {
                Creature newCreature = Instantiate(creature, _spawnPoint.transform.position, Quaternion.identity);
                SpawnedCreatures.Add(newCreature);
                return;
            }
        }

        Debug.Log("Такого существа нет в книге");
    }

    private bool isCreatureAlreadySpawned(string creatureName)
    {
        foreach (var creature in SpawnedCreatures)
        {
            if (creature.CompareTag(creatureName))
            {
                return true;
            }
        }

        return false;
    }
    
}