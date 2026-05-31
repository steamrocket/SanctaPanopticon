using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/EventDatabase")]
public class EventDatabase : ScriptableObject
{
    public List<EventData> allEvents;

    private Dictionary<string, EventData> eventDictionary;

    public void Initialize()
    {
        eventDictionary = new Dictionary<string, EventData>();

        foreach (EventData data in allEvents)
        {
            if (!eventDictionary.ContainsKey(data.eventID))
            {
                eventDictionary.Add(data.eventID, data);
            }
        }
    }

    public EventData GetEvent(string eventID)
    {
        if (eventDictionary == null)
            Initialize();

        if (eventDictionary.TryGetValue(eventID, out EventData data))
            return data;

        Debug.LogError("EventID not found: " + eventID);
        return null;
    }
}
