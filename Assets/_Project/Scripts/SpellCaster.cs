using UnityEngine;
using ObjectPooling;


public class SpellCaster : MonoBehaviour
{
    [SerializeField] private SpellPoolSettings slowingSpellSettings;
    [SerializeField] private SpellPoolSettings speedingPoolSettings;

    private Camera cam;

    private Vector3 spellOffset = new Vector3(0.3f, 0, 0);


    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        SpellPooled spell = null;

        if(Input.GetMouseButtonDown(0))
            spell = ObjectPooler.Spawn(slowingSpellSettings) as SpellPooled;


        if(Input.GetMouseButtonDown(1))
            spell = ObjectPooler.Spawn(speedingPoolSettings) as SpellPooled;


        if (spell != null)
        {
            var direction = GetDirection();
            spell.SetDirection(direction);
            spell.transform.position = transform.position + spellOffset;
            spell.Cast();
        }
    }

    private Vector3 GetDirection()
    {
        var mousePos = Input.mousePosition;
        Vector3 worldPosition = cam.ScreenToWorldPoint(mousePos);
        Vector3 direction = worldPosition - transform.position;
        direction.Normalize();

        return direction;
    }
}
