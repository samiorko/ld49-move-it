using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PackageSpawner : MonoBehaviour
{
    public List<PackageDefinition> definitions;
    public GameObject packagePrefab;
    public TextMeshProUGUI uiHint;
    public float range;

    private int _positionedForMission;
    private Vector3 _worldPos;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void SpawnMore(PackageDefinition def)
    {
        if (Mission.Instance.missionNumber > _positionedForMission)
        {
            _worldPos = Car.Instance.transform.position + Vector3.up * 4f;
            _positionedForMission = Mission.Instance.missionNumber;
            transform.position = _worldPos;
            transform.rotation = Car.Instance.transform.rotation;
        }
        
        var obj = Instantiate(packagePrefab, null);
        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;
        obj.transform.Rotate(def.rotation);

        var pack = obj.GetComponent<Package>();
        pack.definition = def;
        
        Mission.Instance.PackagePickedUp(pack);
    }

    // Update is called once per frame
    void Update()
    {
        var distance = Vector3.Distance(Car.Instance.transform.position, Mission.Instance.source.transform.position);
        var isInRange = distance < range;

        uiHint.text = isInRange
            ? "Press F to add one package"
            : "";
        
        if (!isInRange) return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            var randomDef = Random.Range(0, definitions.Count - 1);
            
            SpawnMore(definitions[randomDef]);
        }
    }
}
