using UnityEngine;

public class ScoreLoaderLast : MonoBehaviour
{
    [SerializeField] private StarRatingDisplay easyStars;
    [SerializeField] private StarRatingDisplay mediumStars;
    [SerializeField] private StarRatingDisplay hardStars;

    private void Start()
    {
        if (easyStars != null)
            easyStars.UpdateLastStarRatingFromPrefs("Easy");

        if (mediumStars != null)
            mediumStars.UpdateLastStarRatingFromPrefs("Medium");

        if (hardStars != null)
            hardStars.UpdateLastStarRatingFromPrefs("Hard");
    }
}
