using UnityEngine;

public class CircularLayout : MonoBehaviour
{
    // Zmienna kontrolująca promień okręgu
    public float radius = 5f;
    // Kąt startowy (w stopniach)
    public float startAngle = 0f;
    // Odległość między obiektami (w stopniach)
    public float angleStep = 30f;
    // Opcjonalny margines
    public float margin = 0.5f;

    void Start()
    {
        ArrangeChildrenInCircle();
    }

    void ArrangeChildrenInCircle()
    {
        int childCount = transform.childCount;
        float angle = startAngle;

        for (int i = 0; i < childCount; i++)
        {
            Transform child = transform.GetChild(i);
            
            // Oblicz pozycję x i y dla danego kąta
            float x = Mathf.Cos(angle * Mathf.Deg2Rad) * (radius + margin);
            float y = Mathf.Sin(angle * Mathf.Deg2Rad) * (radius + margin);

            // Ustaw nową pozycję obiektu dziecka
            child.localPosition = new Vector3(x, y, child.localPosition.z);

            // Przesuń kąt o wartość kroku
            angle += angleStep;
        }
    }

    void OnValidate()
    {
        // Aktualizuj układ dzieci podczas edycji w edytorze
        if (Application.isPlaying)
        {
            ArrangeChildrenInCircle();
        }
    }
}
