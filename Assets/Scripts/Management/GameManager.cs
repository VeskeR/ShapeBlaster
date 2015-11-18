using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enemies;
using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Management
{
    public class GameManager : MonoBehaviour
    {
        public Texture2D CursorTexture;
        public GameObject ParticlesWallTemplate;
        public Text GameOverText;
        // Time after game manager starts sending enemies
        public float StartAfter;
        // Starting reverted enemy spawn chance
        public float StartRevertEnemySpawnChance;
        // Minimum reverted enemy spawn chance (maximum spawn chance)
        public float MinimumRevertEnemySpawnChance;
        // Decrease reverted enemy spawn chance every second by that value
        public float DecreaseValue;
        // List of enemies to spawn
        public List<Enemy> Enemies;

        private GameObject _player;
        private bool _gameOver;
        private float _timeSurvived;
        private float _currentRevertSpawnChance;

        private static GameManager _instance;

        public static GameManager Instance
        {
            get { return _instance; }
        }

        public static Camera Camera
        {
            get { return Camera.main; }
        }

        public static float XMin
        {
            get { return Camera.transform.position.x - Camera.orthographicSize*Camera.aspect; }
        }
        public static float XMax
        {
            get { return Camera.transform.position.x + Camera.orthographicSize*Camera.aspect; }
        }
        public static float YMin
        {
            get { return Camera.transform.position.y - Camera.orthographicSize; }
        }
        public static float YMax
        {
            get { return Camera.transform.position.y + Camera.orthographicSize; }
        }


        private void Awake()
        {
            if (_instance == null)
                _instance = this;
            else if (_instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);

            _player = GameObject.FindGameObjectWithTag("Player");
        }

        private void Start()
        {
            // Set game cursor
            Cursor.SetCursor(CursorTexture, new Vector2(0,0), CursorMode.Auto);

            // Create boundaries for particles bouncing
            InitParticleBoundaries();

            // Initialise starting values
            ResetValues();

            // Start spawning wawes
            StartCoroutine("SpawnWawes");
        }

        private void Update()
        {
            // Increase time survived
            _timeSurvived += Time.deltaTime;

            // If game is over and user pressed key 'R' ...
            if (_gameOver && Input.GetKey(KeyCode.R))
            {
                // ... then reload level
                Application.LoadLevel(Application.loadedLevel);
            }
        }


        public void GameOver()
        {
            // Stop spawning wawes
            StopCoroutine("SpawnWawes");

            // Set game over text
            string gameOver = string.Format("Game Over!\nYou have survived {0} seconds!\n", (int)_timeSurvived);
            gameOver += "Press 'R' to restart";

            GameOverText.text = gameOver;

            _gameOver = true;
        }

        private IEnumerator SpawnWawes()
        {
            // Wait before start spawning wawes
            yield return new WaitForSeconds(StartAfter);

            PlayerShip player = _player.GetComponent<PlayerShip>();

            while (true)
            {
                // If player is alive then attemp to spawn enemies
                if (player.IsAlive)
                {
                    // Attempt to create sekeer
                    if (Random.Range(0, (int)_currentRevertSpawnChance) == 0)
                        Instantiate(Enemies.Find(e => e.Name == "Sekeer"), GetEnemySpawnPosition(2f),
                            Quaternion.identity);

                    // Attempt to create wanderer
                    if (_timeSurvived > 30)
                        if (Random.Range(0, (int)_currentRevertSpawnChance * 3) == 0)
                            Instantiate(Enemies.Find(e => e.Name == "Wanderer"), GetEnemySpawnPosition(-1f),
                                Quaternion.identity);

                    // Attempt to create invisible sekeer
                    if (_timeSurvived > 45)
                        if (Random.Range(0, (int)_currentRevertSpawnChance * 5) == 0)
                            Instantiate(Enemies.Find(e => e.Name == "InvisibleSekeer"), GetEnemySpawnPosition(-1f),
                                Quaternion.identity);

                    // Attempt to create rectangular
                    if (_timeSurvived > 60)
                        if (Random.Range(0, (int)_currentRevertSpawnChance * 5) == 0)
                            Instantiate(Enemies.Find(e => e.Name == "Rectangular"), GetEnemySpawnPosition(-1f, false),
                                Quaternion.identity);

                    // Attempt to create black hole
                    if (_timeSurvived > 90)
                        if (Random.Range(0, (int)_currentRevertSpawnChance * 50) == 0)
                            Instantiate(Enemies.Find(e => e.Name == "BlackHole"), GetEnemySpawnPosition(-1f),
                                Quaternion.identity);

                    // If current reverted spawn chance isn't minimal ...
                    if (_currentRevertSpawnChance > MinimumRevertEnemySpawnChance)
                        // ... then decrease it
                        _currentRevertSpawnChance -= DecreaseValue * Time.deltaTime; 
                }

                yield return null;
            }
        }

        private Vector3 GetEnemySpawnPosition(float expand, bool awayFromPlayer = true)
        {
            // Get enemy possible spawn position
            Vector3 spawnPos = new Vector3(Random.Range(XMin - expand, XMax + expand), Random.Range(YMin - expand, YMax + expand), 0.0f);

            // If method must guarantee that distance between player and enemy is more than 2 units ...
            while (awayFromPlayer && Vector3.Distance(spawnPos, _player.transform.position) < 2)
                // ... then create new position for enemy while it's not enough distanced
                spawnPos = new Vector3(Random.Range(XMin - expand, XMax + expand), Random.Range(YMin - expand, YMax + expand), 0.0f);

            return spawnPos;
        }

        private void InitParticleBoundaries()
        {
            // Instantiate left wall for particles bouncing
            GameObject o = Instantiate(ParticlesWallTemplate, new Vector3(XMin - 0.5f, 0, 0), Quaternion.identity) as GameObject;
            o.transform.localScale = new Vector3(1, YMax - YMin, 1);

            // Instantiate right wall for particles bouncing
            o = Instantiate(ParticlesWallTemplate, new Vector3(XMax + 0.5f, 0, 0), Quaternion.identity) as GameObject;
            o.transform.localScale = new Vector3(1, YMax - YMin, 1);

            // Instantiate top wall for particles bouncing
            o = Instantiate(ParticlesWallTemplate, new Vector3(0, YMax + 0.5f, 0), Quaternion.identity) as GameObject;
            o.transform.localScale = new Vector3(XMax - XMin, 1, 1);

            // Instantiate bottom wall for particles bouncing
            o = Instantiate(ParticlesWallTemplate, new Vector3(0, YMin - 0.5f, 0), Quaternion.identity) as GameObject;
            o.transform.localScale = new Vector3(XMax - XMin, 1, 1);
        }

        private void ResetValues()
        {
            GameOverText.text = "";
            _gameOver = false;
            _timeSurvived = 0f;
            _currentRevertSpawnChance = StartRevertEnemySpawnChance;
        }
    }
}
