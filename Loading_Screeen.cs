using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Loading_Screen : MonoBehaviour
{
    public Slider loadingSlider;

    void Start()
    {
        // Rozpocznij ładowanie sceny w tle
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        // Pobierz indeks aktualnej sceny (możesz zmienić na nazwę sceny, jeśli wolisz)
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Rozpocznij asynchroniczne ładowanie nowej sceny
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex + 1);

        // Ustaw flagę, aby scena nie została załadowana automatycznie
        operation.allowSceneActivation = false;

        // Dopóki operacja ładowania nie została zakończona
        while (!operation.isDone)
        {
            // Aktualizuj wartość slidera na podstawie postępu ładowania sceny (zakładając, że 0.9 oznacza 90%)
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingSlider.value = progress;

            // Poczekaj na chwilę
            yield return null;

            // Jeśli ładowanie sceny zostało ukończone
            if (progress == 1f)
            {
                // Odczekaj krótki czas, aby gracze mogli zobaczyć 100% załadowania
                yield return new WaitForSeconds(1f);

                // Włącz scenę
                operation.allowSceneActivation = true;
            }
        }
    }
}
