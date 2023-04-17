using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private TextMeshProUGUI _time;
    [SerializeField] private int _targetDestroyAsteroids;
    [SerializeField, Range(1, 5)] private int _targetTime;
    [SerializeField] private AudioSource audioSource;
    private int _currentDestroyAsteroids;
    private float _timer;
    void Start()
    {
        _score.text = _currentDestroyAsteroids + " / " + _targetDestroyAsteroids;
        _timer = _targetTime * 60;
    }

    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(_timer / 60);
        int seconds = Mathf.FloorToInt(_timer % 60);
        _time.text = string.Format("{0:d2}", minutes)  + ":" + string.Format("{0:d2}", seconds);

        if(_currentDestroyAsteroids == _targetDestroyAsteroids)
        {
            audioSource.PlayOneShot(audioSource.clip);
            _targetDestroyAsteroids += _targetDestroyAsteroids;
            _timer += _targetTime * 60;
        }

        if(Mathf.FloorToInt(_timer) == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void AddScore()
    {
        _currentDestroyAsteroids++;
        _score.text = _currentDestroyAsteroids + " / " + _targetDestroyAsteroids;
    }
}
