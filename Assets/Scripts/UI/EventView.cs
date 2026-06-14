using UnityEngine;
using TMPro;
using UnityEngine.UI;


        if (EventManager.Instance != null)
        {
            EventManager.Instance.BindEventView(this);
        }
// イベントの表示と選択肢の管理を行うクラス
//EventView は ただの表示装置（EventPanel にアタッチする制御役）
// EventManager からイベントデータを受け取り、UIに表示し、選択肢が選ばれたら EventManager に通知する
public class EventView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI eventText;
    [SerializeField] private Transform choiceArea;
    [SerializeField] private Button choiceButtonPrefab;

    private EventManager eventManager;

    private void Awake()
    {
        eventManager = FindObjectOfType<EventManager>();
    }

    // イベントデータを受け取り、表示を更新する
    public void ShowEvent(EventData data)
    {       
        eventText.text = data.description;

        ClearChoices();

        foreach (var choice in data.choices)
        {
            CreateChoiceButton(choice);
        }

        gameObject.SetActive(true);
    }

    // 選択肢ボタンを生成し、クリックイベントを設定する
    private void CreateChoiceButton(ChoiceData choice)
    {
        Button btn = Instantiate(choiceButtonPrefab, choiceArea);
        btn.GetComponentInChildren<TextMeshProUGUI>().text = choice.choiceText;

        btn.onClick.AddListener(() =>
        {
            EventManager.Instance.OnChoiceSelected(choice);
            //gameObject.SetActive(false);
        });
    }

    // 既存の選択肢をクリアする
    private void ClearChoices()
    {
        foreach (Transform child in choiceArea)
        {
            Destroy(child.gameObject);
        }
    }
}