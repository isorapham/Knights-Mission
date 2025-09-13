using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;
    public Canvas gameCanvas;
    public GamePanel GamePanel;
 

    private void Awake()
    {
        gameCanvas = FindAnyObjectByType<Canvas>();
    
    }
    private void Start()
    {
        GamePanel.gameObject.SetActive(true);
    }
    private void OnEnable()
    {
        CharacterEvents.characterDamaged+=CharacterTookDamage;
        CharacterEvents.characterHealed += CharacterHealed;
    }
    private void OnDisable()
    {
        CharacterEvents.characterDamaged-=CharacterTookDamage;
        CharacterEvents.characterHealed-=CharacterHealed;
    }
    public void CharacterTookDamage(GameObject character, int damageReceived)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText= Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity,gameCanvas.transform).GetComponent<TMP_Text>();
        tmpText.text = damageReceived.ToString();
    }
    public void CharacterHealed(GameObject character, int healedRestored)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();
        tmpText.text = healedRestored.ToString();
    }
}
