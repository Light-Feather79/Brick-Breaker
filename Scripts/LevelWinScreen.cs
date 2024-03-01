using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelWinScreen : MonoBehaviour
{
    [SerializeField] private Sprite[] _imageOfStars;
    [SerializeField] private Image _imageStar;
    [SerializeField] private ParticleSystem _stars;

    private void OnEnable()
    {
        Level currentLevel = (Level)SceneManager.GetActiveScene().buildIndex;
        _imageStar.sprite = _imageOfStars[GameData.Instance.GetStarAmountOfLevel(currentLevel)];
        Instantiate(_stars);    
    }

    public void OpenNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
