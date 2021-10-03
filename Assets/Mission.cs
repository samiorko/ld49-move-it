using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Mission : MonoBehaviour
{
    public static Mission Instance;

    public int missionNumber;
    public Transform targetArrow;
    
    public MissionTarget source;
    public MissionTarget target;

    public MissionTarget currentTarget;

    public List<Package> missionItems;

    public GameObject packagePrefab;

    private static bool IsInitialized;
    private static List<MissionTarget> Targets = new List<MissionTarget>();

    public int points;

    private Transform _smoothTarget;
    private Vector3 _smoothTargetVelocity;

    private int _messageShownForMission;


    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
        if (!IsInitialized)
        {
            Targets = FindObjectsOfType<MissionTarget>().ToList();
            IsInitialized = Targets.Any();
        }

        var obj = new GameObject("SmoothTarget");
        obj.transform.parent = null;
        
        _smoothTarget = obj.transform;

        NewMission();
    }

    private void NewMission()
    {
        if (missionItems != null)
        {
            foreach (var package in missionItems)
            {
                Destroy(package.gameObject);
            }
        }

        if (source != null)
        {
            source.activeTarget = false;
        }
        
        if (target != null)
        {
            target.activeTarget = false;
        }
  
        missionItems = new List<Package>();
        var sourceIndex = Random.Range(0, Targets.Count - 1);
        int targetIndex;
        do
        {
            targetIndex = Random.Range(0, Targets.Count - 1);
        } while (targetIndex == sourceIndex);

        source = Targets[sourceIndex];
        target = Targets[targetIndex];

        currentTarget = source;
        missionNumber++;
    }

    private void Update()
    {
        source.activeTarget = currentTarget == source;
        target.activeTarget = currentTarget == target;

        _smoothTarget.position = Vector3.SmoothDamp(
            _smoothTarget.position,
            new Vector3(currentTarget.transform.position.x, targetArrow.transform.position.y,
                currentTarget.transform.position.z),
            ref _smoothTargetVelocity,
            2f
        );
        
        targetArrow.LookAt(_smoothTarget);

        if (target.activeTarget == target)
        {
            var range = Vector3.Distance(target.transform.position, Car.Instance.transform.position);
            if(range < 10f)
            {
                var nearbyBoxes = Physics.OverlapSphere(target.transform.position, 20f, LayerMask.GetMask("package"));
                var boxPoints = nearbyBoxes.Any()
                    ? nearbyBoxes.Select(x => x.GetComponentInParent<Package>()).Sum(x => x.weight)
                    : 0;

                var gainedPoints = Mathf.RoundToInt(boxPoints);

                points += gainedPoints;
                
                MessageSystem.Instance.ShowMessage($"Packages delivered! {gainedPoints} points gained. Follow the arrow to your next pick-up.", 5);
                
                NewMission();
            }
        }
        
    }

    public void PackagePickedUp(Package package)
    {
        missionItems.Add(package);

        if (_messageShownForMission < missionNumber)
        {
            _messageShownForMission = missionNumber;
            var textDuration = 5;
            
            if (missionNumber > 1)
            {
                textDuration = 2;
            }
            
            MessageSystem.Instance.ShowMessage("Once you have picked enough packages, find the target pointed by the arrow", textDuration);
        }
        
        currentTarget = target;
    }
}
