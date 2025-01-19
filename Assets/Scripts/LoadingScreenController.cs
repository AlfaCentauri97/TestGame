using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenController : MonoBehaviour
{
    public Slider loadingSlider;
    public float loadingDuration = 3f;
    
    private bool isLoading = false;
    
    public void ToggleLoadingScreen(bool isActive)
    {
        gameObject.SetActive(isActive);

        if (isActive && !isLoading)
        {
            StartCoroutine(AnimateLoading());
        }
    }
    
    private IEnumerator AnimateLoading()
    {
        isLoading = true;
        loadingSlider.value = 0f;

        float elapsedTime = 0f;
        while (elapsedTime < loadingDuration)
        {
            elapsedTime += Time.deltaTime;
            loadingSlider.value = Mathf.Clamp01(elapsedTime / loadingDuration);
            yield return null;
        }

        loadingSlider.value = 1f;
        OnLoadingComplete();
        isLoading = false;
    }

    private void OnLoadingComplete()
    {
        Debug.Log("Loading DONE");
        ToggleLoadingScreen(false);
    }
}