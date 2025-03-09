using UnityEngine;

public class ScoreLoader : MonoBehaviour
{
    [SerializeField] private StarRatingDisplay easyStars;
    [SerializeField] private StarRatingDisplay mediumStars;
    [SerializeField] private StarRatingDisplay hardStars;

    private void Start()
    {
        // Проверяем, есть ли сохранённые результаты и обновляем звезды
        if (easyStars != null)
            easyStars.UpdateStarRatingFromPrefs("Easy");

        if (mediumStars != null)
            mediumStars.UpdateStarRatingFromPrefs("Medium");

        if (hardStars != null)
            hardStars.UpdateStarRatingFromPrefs("Hard");
    }
}
