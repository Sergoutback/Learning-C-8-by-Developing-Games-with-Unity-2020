using UnityEngine;

public class LearningCurve : MonoBehaviour
{
    public GameObject directionLight;
    private Transform camTransform;
    private Transform lightTransform;

    private void Start()
    {
        camTransform = GetComponent<Transform>();

        Debug.Log(camTransform.localPosition);

        //lightTransform = GameObject.Find("Directional Light").GetComponent<Transform>();

        lightTransform = GetComponent<Transform>();

        Debug.Log(lightTransform.localPosition);

        var hero = new Character();

        var hero2 = new Character();

        var huntingBow = new Weapon("Hunting Bow", 105);

        var knight = new Paladin("Sir Arthur", huntingBow);

        hero2.name = "Sir Krane the Brave";

        hero.PrintStatsInfo();

        hero2.PrintStatsInfo();

        knight.PrintStatsInfo();

        //hero2.Reset();

        var heroine = new Character("Agatha");

        heroine.PrintStatsInfo();

        var warBow = huntingBow;

        warBow.name = "War Bow";

        warBow.damage = 155;

        huntingBow.PrintWeaponStats();

        warBow.PrintWeaponStats();
    }
}