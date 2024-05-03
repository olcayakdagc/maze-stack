using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;
using LevelDesign;
namespace Managers
{
    [DefaultExecutionOrder(-2)]
    public class LevelManager : MonoSingleton<LevelManager>
    {
        /// <summary>
        /// Number of the level currently played. This value is not modulated.
        /// </summary>
        public int LevelNo
        {
            get => PlayerPrefs.GetInt("Level", 1);
            set => PlayerPrefs.SetInt("Level", value);
        }

        [Tooltip("Randomizes levels after all levels are played.\nIf this is unchecked, levels will be played again in same order.")]
        [SerializeField] private bool randomizeAfterRotation = true;

        public Level[] Levels;
        [Tooltip("If you have tutorial levels add them here to extract them from rotation")]
        public Level[] TutorialLevels;
        public Level CurrentLevel { get; private set; }

        // Index of the level currently played
        private int currentLevelIndex;

        public static event UnityAction OnLevelLoad;
        public static event UnityAction OnLevelUnload;
        public static event UnityAction OnLevelStart;
        public static event UnityAction OnLevelWin;
        public static event UnityAction OnLevelLose;


        private void Start()
        {

            LoadCurrentLevel();
        }

        public void LoadCurrentLevel()
        {
            int tutorialCount = TutorialLevels.Length;
            int levelCount;
            int levelIndex = LevelNo;

            if (LevelNo <= tutorialCount)
                levelCount = tutorialCount;
            else
            {
                levelCount = Levels.Length;
                levelIndex -= tutorialCount;
            }

            if (levelIndex <= levelCount)
            {
                currentLevelIndex = levelIndex;
            }
            else if (randomizeAfterRotation)
            {
                currentLevelIndex = Random.Range(1, levelCount + 1);
            }
            else
            {
                levelIndex %= levelCount;
                currentLevelIndex = levelIndex.Equals(0) ? levelCount : levelIndex;
            }

            LoadLevel(currentLevelIndex);
            StartLevel();
        }

        private void LoadLevel(int index)
        {
            CurrentLevel = Instantiate(LevelNo <= TutorialLevels.Length ? TutorialLevels[index - 1] : Levels[index - 1]);
            OnLevelLoad?.Invoke();
            OnLevelStart?.Invoke();
        }

        public void StartLevel()
        {
            GameManager.instance.gameState = GameStates.gameplay;
            OnLevelStart?.Invoke();
        }

        public void RetryLevel()
        {
            UnloadLevel();

            LoadLevel(currentLevelIndex);
        }

        public void LoadNextLevel()
        {
            UnloadLevel();

            LevelNo++;
            LoadCurrentLevel();
        }

        private void UnloadLevel()
        {
            OnLevelUnload?.Invoke();
            Destroy(CurrentLevel.gameObject);
        }

        public void Win()
        {
            GameManager.instance.gameState = GameStates.win;

            Managers.HapticManager.instance.PlayPreset(Lofelt.NiceVibrations.HapticPatterns.PresetType.Success);

            OnLevelWin?.Invoke();
        }

        public void Lose()
        {
            GameManager.instance.gameState = GameStates.lose;
            Managers.HapticManager.instance.PlayPreset(Lofelt.NiceVibrations.HapticPatterns.PresetType.Failure);


            OnLevelLose?.Invoke();
        }
    }
}