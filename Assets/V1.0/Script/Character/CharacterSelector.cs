using System;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    [Serializable]
    public class CharacterSlot
    {
        public GameObject character;
        public int startHour;
        public int endHour;
    }

    [Header("Time Settings")]
    [SerializeField] bool useCompressedTime = true;
    [SerializeField] float cycleDuration = 30f;

    [Header("Characters")]
    [SerializeField] CharacterSlot[] slots;

    void Awake()
    {
        int hour = useCompressedTime ? GetCompressedHour() : GetRealHour();

        Debug.Log($"[CharacterSelector] Mode: {(useCompressedTime ? "Compressed" : "Real")} | Current hour: {hour}");

        ActivateCharacter(hour);
    }

    void Update()
    {
        if (!useCompressedTime) return;

        int hour = GetCompressedHour();
        ActivateCharacter(hour);
    }

    int GetRealHour()
    {
        int hour = DateTime.Now.Hour;
        Debug.Log($"[CharacterSelector] Real device time - Hour: {hour}");
        return hour;
    }

    int GetCompressedHour()
    {
        float progress = (Time.time % cycleDuration) / cycleDuration;
        int hour = Mathf.FloorToInt(progress * 24);
        return hour;
    }

    void ActivateCharacter(int hour)
    {
        bool anyActivated = false;

        for (int i = 0; i < slots.Length; i++)
        {
            CharacterSlot slot = slots[i];

            if (slot.character == null)
            {
                Debug.LogWarning($"[CharacterSelector] Slot {i} has no character assigned. Skipping.");
                continue;
            }

            bool inRange = hour >= slot.startHour && hour <= slot.endHour;
            slot.character.SetActive(inRange);

            if (inRange)
            {
                Debug.Log($"[CharacterSelector] Activated: {slot.character.name} | Slot: {slot.startHour}:00 - {slot.endHour}:59 | Hour: {hour}");
                anyActivated = true;
            }
        }

        if (!anyActivated)
        {
            Debug.LogWarning($"[CharacterSelector] No character matched hour {hour}. All characters are inactive.");
        }
    }
}